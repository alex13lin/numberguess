var real_answer
function generator(){
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;
    document.getElementById("result").innerHTML="開始作答!!!";
    document.getElementById("answer").value="";
    xhr.addEventListener("readystatechange", function() {
        if(this.readyState === 4) {
            var text = JSON.parse(this.responseText);
            var topic = text[0].number1 + "+" + text[0].number2 + "=?";
            real_answer = text[0].answer
            document.getElementById("topic").innerText = topic;
        }
    });

    xhr.open("GET", "https://localhost:8080/GuessAPI/Generator");

    xhr.send();
}

async function the_submit(){
    console.log("開始處理");
    await check_result();
    console.log("格式確認完畢");    
    await regex_answer();
    console.log("答案確認完畢");

}

function regex_answer(){
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
      };
    var get_answer = document.getElementById("answer").value;
    var str = `https://localhost:8080/GuessAPI/Regex_result?get_answer=${get_answer}`
      
      fetch(str, requestOptions)
        .then(response => response.json())
        .then(result => alert(result[0].regex_result))
        .catch(error => console.log('error', error));
}

function check_result(){
    
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
      };
    var get_answer = document.getElementById("answer").value;
    var str = `https://localhost:8080/GuessAPI/ResultCheck?get_answer=${get_answer}&real_answer=${real_answer}`;
      fetch(str, requestOptions)
        .then(response => response.json())
        .then(result => {
            var the_str = document.getElementById("pname").value + " " + result[0].check_result;
            document.getElementById("result").innerText = the_str;
        })
        .catch(error => console.log('error', error));
}
