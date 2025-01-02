const path = require('path');
const fs = require('fs');

function findFiles(dir, extensions, fileList = []) {
    const files = fs.readdirSync(dir);

    files.forEach((file) => {
        const fullPath = path.join(dir, file);
        const stat = fs.statSync(fullPath);

        if (stat.isDirectory()) {
            findFiles(fullPath, extensions, fileList);
        }
        else {
            const ext = path.extname(file).toLowerCase();

            if (extensions.includes(ext)) {
                fileList.push(fullPath);
            }
        }
    });

    return fileList;
}


module.exports = (env, argv) => {
    const mode = argv.mode || 'development';

    let entry = findFiles(
        path.resolve(__dirname, '../Views'),
        ['.ts', '.js', '.razor.ts', '.razor.js']);

    entry.push('./src/index.ts');

    return {
        mode: mode,
        devtool: mode === 'development' ? 'source-map' : false,
        entry: entry,
        output: {
            filename: 'index.bundle.js',
            path: path.resolve(__dirname, '../wwwroot/js'),
        },
        module: {
            rules: [
                {
                    test: /\.ts$/,
                    use: 'ts-loader',
                    exclude: /node_modules/,
                },
                {
                    test: /\.js$/,
                    use: {
                        loader: 'babel-loader',
                        options: {
                            presets: ['@babel/preset-env'],
                        },
                    },
                    exclude: /node_modules/,
                }
            ]
        },
        resolve: {
            extensions: ['.ts', '.js'],
        },
        devServer: {
            static: path.resolve(__dirname, '../wwwroot'),
            compress: true,
            port: 9000,
        },
    };
};
