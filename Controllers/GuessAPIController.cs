using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Threading;

namespace IdAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GuessAPIController : ControllerBase{

    [HttpGet("Generator")]
    public IEnumerable<GuessAPI> Get()
    {
        return Enumerable.Range(1, 1).Select(index => new GuessAPI
        {
            number1 = number_random(),
            number2 = number_random(),
        })
        .ToArray();
    }
    
    public static int number_random(){
        Random myObject = new Random();
        int ranNum= myObject.Next(0, 100);
        return ranNum;
    }

    [HttpGet("ResultCheck")]
    public IEnumerable<GuessAPI> Get(int get_answer,int real_answer)
    {
        Thread.Sleep(2000);
        return Enumerable.Range(1, 1).Select(index => new GuessAPI
        {
            check_result = check_result(get_answer,real_answer)
            
        })
        .ToArray();
    }
    public string check_result(int get_answer,int real_answer){
        if(get_answer==real_answer){
            return "恭喜答對!";
        }
        else return "答錯了!";
    }

    [HttpGet("Regex_result")]
    public IEnumerable<GuessAPI> Get(string get_answer)
    {
        return Enumerable.Range(1, 1).Select(index => new GuessAPI
        {
            regex_result = regex_result(get_answer)
        })
        .ToArray();
    }
    public string regex_result(string get_answer){
        bool regex_result = Regex.IsMatch(get_answer, @"^[0-9]+$");
        if(regex_result) return "格式正確!!!";
        else return "格式錯誤!!!";
        
    }
}

