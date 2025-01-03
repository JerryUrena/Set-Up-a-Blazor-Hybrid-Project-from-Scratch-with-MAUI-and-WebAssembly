# How to add Bootstrap and SCSS Support to Your Blazor Hybrid App

Microsoft doesn't easily support SCSS, but we'll do it anyway. Watch out, Microsoft! 😄

## Requirement
- For the purpose of this tutorial let's clone this [repo](https://github.com/JerryUrena/Set-Up-a-Blazor-Hybrid-Project-from-Scratch-with-MAUI-and-WebAssembly/tree/npm) branch

## Dependencies

1. Navigate to `MyBlazorHybridApp`.
2. Open a new command prompt and `cd` to the `npm` folder.
3. Install the following development dependencies:

    ```bash
    npm install -D style-loader css-loader postcss-loader sass sass-loader autoprefixer @types/bootstrap
    ```

4. Install Bootstrap as a dependency:

    ```bash
    npm install bootstrap@latest
    ```

## Webpack Configuration

1. Open `webpack.config.js`.
2. Add the following rules to the file:

    ```js
    {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
    },
    {
        test: /\.(scss)$/,
        use: [
            { loader: 'style-loader' },
            { loader: 'css-loader' },
            {
                loader: 'postcss-loader',
                options: {
                    postcssOptions: {
                        plugins: [autoprefixer]
                    }
                }
            },
            { loader: 'sass-loader' }
        ]
    }
    ```

3. At the top of the file, add the `autoprefixer` import:

    ```js
    const autoprefixer = require('autoprefixer');
    ```

These rules add support for CSS and SCSS.

## Adding Bootstrap

1. Go to the `src` directory and open `index.ts`.
2. Add the following imports to include Bootstrap:

    ```ts
    import 'bootstrap';
    import 'bootstrap/dist/css/bootstrap.min.css';
    //import 'bootstrap/dist/js/bootstrap.bundle.min.js';
    ```

Now, Bootstrap is included in your project. Before building, let's add support for SCSS files.

## Adding SCSS Support

1. Create a new directory inside `src` named `scss`.
2. Within the `scss` directory, create a file named `custombootstrap.scss`.
3. Add the following code to modify the default Bootstrap colors:

    ```scss
    // Custom color variables
    $primary: #3498db;
    $secondary: #2ecc71;
    $danger: #e74c3c;
    $info: #8e44ad;

    $theme-colors: (
        "primary": $primary,
        "secondary": $secondary,
        "info": $info,
        "danger": $danger,
        "my-custom-color": #f1c40f,
        //add more colors here
    );

    $enable-subtle-buttons: true;

    @import "~bootstrap/scss/bootstrap";
    ```

4. Go back to `src/index.ts` and import the new SCSS file:

    ```ts
    // Import custom Bootstrap SCSS
    import './scss/custombootstrap.scss';
    ```

## Testing

1. Navigate to the `views` directory.
2. Open `Home.razor`.
3. Add the `btn` and `btn-success` classes to a button. Your button should now look like this:

    ```html
    <button @onclick="TriggerAlert" class="btn btn-success">Click Me</button>
    ```

## Final Step

Rebuild your app and run it to see the changes. 🎉
