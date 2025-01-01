using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorHybridApp.Services;

public interface IMyFirstService
{

}
public class MyFirstService(IMySecondService mySecondService) : IMyFirstService
{

}


////or you can do it the classic way
//public class MyFirstService : IMyFirstService
//{
//    private readonly IMySecondService _mySecondService;
//    public MyFirstService(IMySecondService mySecondService)
//    {
//        _mySecondService = mySecondService;
//    }
//}
