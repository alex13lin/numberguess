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
    await regex_answer();
    await check_result();
    
}

function regex_answer(){
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;
    var get_answer = document.getElementById("answer").value;

    xhr.addEventListener("readystatechange", function() {
        if(this.readyState === 4) {
            var text = JSON.parse(this.responseText);
            var regex_result = text[0].regex_result;
            alert(regex_result);
            
        }
    });
    var str = `https://localhost:8080/GuessAPI/Regex_result?get_answer=${get_answer}`
    xhr.open("GET", str);

    xhr.send();
}

function check_result(){
    
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;
    var get_answer = document.getElementById("answer").value;

    xhr.addEventListener("readystatechange", function() {
        if(this.readyState === 4) {
            var text = JSON.parse(this.responseText);
            var the_str = document.getElementById("pname").value + " " + text[0].check_result;
            document.getElementById("result").innerText = the_str;
            //console.log("收到");
        }
    });
    
    var str = `https://localhost:8080/GuessAPI/ResultCheck?get_answer=${get_answer}&real_answer=${real_answer}`
    xhr.open("GET", str);

    xhr.send();
}