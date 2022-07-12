using Microsoft.AspNetCore.Mvc;


namespace IdAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GuessAPIGeneratorController : ControllerBase{
    [HttpGet]
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
}

[ApiController]
[Route("[controller]")]
public class GuessAPIResultCheckController : ControllerBase{
    [HttpGet]
    public IEnumerable<GuessAPI> Get(int answer,int real_answer)
    {
        return Enumerable.Range(1, 1).Select(index => new GuessAPI
        {
            check_result = check_result(answer,real_answer)
        })
        .ToArray();
    }
    public string check_result(int answer,int real_answer){
        if(answer==real_answer){
            return "恭喜答對!";
        }
        else return "答錯了!";
    }
}