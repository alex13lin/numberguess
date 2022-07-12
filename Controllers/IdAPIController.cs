using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace IdAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class IdAPIController : ControllerBase{
    [HttpGet("From/{id}")]
    public dynamic GetFrom(string id,[FromQuery] string id2,[FromHeader] string id4,[FromForm] string id5){
        List<dynamic> result = new List<dynamic> ();
        result.Add(id);
        result.Add(id2);
        //result.Add(id3);
        result.Add(id4);
        result.Add(id5);
        return result;
    }
    [HttpGet]
    public IEnumerable<IdAPI> Get(string getId)
    {
        return Enumerable.Range(1, 1).Select(index => new IdAPI
        {
            result = verify_ID(getId)
            
        })
        .ToArray();
    }

    
    
    public static string verify_ID(string Id){
        bool check_regex = Regex.IsMatch(Id, @"^[A-Z]{1}[1-2]{1}[0-9]{8}$");
        if(check_regex){
            //身分證驗證
            int count_area = Area(Id[0]);
            int count_num = Num(Id);   
            int count = count_area + count_num;//相加得到總驗證數值
            if(count%10!=0){
                return "false";
                        
            }
            return "true";
        }
        return "false";
    }
    static int Area(char Id_0){
            //區域碼計算
        String area_data = "ABCDEFGHJKLMNPQRSTUVXYWZIO";//依照所需依序排列出字串
        int area_i = 0;
        while(area_i<26){
            if(Id_0==area_data[area_i]){
                break;//當找到Id[0]在area_data裡的位置時，停止
            }else{
                area_i++;//紀錄所找到的字母位置(0,1,2,3...)
            }
        }
        area_i += 10;//+10以符合相對應數值
        int count_area = (area_i/10)*1 + (area_i%10)*9;//算出區域碼的驗證數值
        return count_area;
    }
    static int Num(string Id){
            //數字碼計算
        int weight,count_num = 0;
        for(int num_i = 1;num_i<=9;num_i++){
            bool check_num = int.TryParse(Id[num_i].ToString(),out int num);
            //將Id[num_i]從字元轉為字串，再從字串轉為整數，如果錯誤則回傳false，正確則可得到num
                        
            weight = 9 - num_i;
            if(weight == 0)weight = 1;
            count_num += num*weight;
        }
        return count_num;
    }
}