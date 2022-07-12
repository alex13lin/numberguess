namespace IdAPI;

public class GuessAPI
{
    public int number1 {get;set;}
    public int number2 {get;set;}
    public string? check_result {get;set;}
    public int answer => (int)(number1) + (int)(number2);
    
}
