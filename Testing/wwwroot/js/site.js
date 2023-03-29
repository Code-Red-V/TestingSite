function saveResult() {
    var result = document.getElementById('percent').innerHTML;
    var test = document.querySelector("input[name='test']").value;

    $.post('/Home/SaveResult', { percent: "" + result + "", testId: "" + test + "" },
        function (res) {
            if (res.isValid) {
                alert(res.message);
            } else {
                alert(res.message);
            }
        });
}
function check() {
    let radioChecked = [], checkboxesChecked = [], rightAnswers = 0, rightAnswersPercent = 0, rightCheckBoxesChecked = 0, checkBoxCheckedResult, trueCheckBoxes = 0, checkBox;

    var allInputs = document.getElementsByClassName('answer');
    var questionCount = parseInt(document.querySelector('input[name="questionCount"]').value) - 1;

    for (let i = 0; i < allInputs.length; i++) {
        if (allInputs[i].checked && allInputs[i].type == 'radio') {
            radioChecked.push(allInputs[i]);
        } else if (allInputs[i].checked && allInputs[i].type == 'checkbox') {
            checkboxesChecked.push(allInputs[i]);
        }
    }
    for (let i = 0; i < radioChecked.length; i++) {
        var resDiv = document.createElement('div');
        resDiv.setAttribute("id", "resultIcon");

        var parElem = radioChecked[i].parentElement;
        var questionItem = parElem.parentElement;

        if (radioChecked[i].value == 'true') {              

            parElem.classList.add('rightAnswer');
            questionItem.classList.add('rightAnswerBackground');

            resDiv.classList.add('trueIcon');
            questionItem.appendChild(resDiv);       

            rightAnswers++;
        } else if (radioChecked[i].value == 'false') {
            parElem.classList.add('wrongAnswer');
            questionItem.classList.add('wrongAnswerBackground');

            resDiv.classList.add('falseIcon');
            questionItem.appendChild(resDiv);
        }
    }

    for (let i = 0; i < questionCount; i++) {
        var name = 'Q' + (i + 1);
        var inputs = document.getElementsByName(`${name}`);
        
        for (let j = 0; j < inputs.length; j++) {
            var parElem = inputs[j].parentElement;

            if (inputs[j].type == 'checkbox' && inputs[j].value == 'true') {
                checkBox = inputs[j].parentElement;
                trueCheckBoxes++;
            }
        }

        var oneMark = 1 / trueCheckBoxes;

        for (let j = 0; j < inputs.length; j++) {
            var parElem = inputs[j].parentElement;

            if (inputs[j].type == 'checkbox' && inputs[j].checked && inputs[j].value == 'true') {               
                parElem.classList.add('rightAnswer');
                rightCheckBoxesChecked += oneMark;
            }
            if (inputs[j].type == 'checkbox' && inputs[j].checked && inputs[j].value == 'false') {
                parElem.classList.add('wrongAnswer');
                    rightCheckBoxesChecked -= oneMark;
            }
        }
        if (rightCheckBoxesChecked < 0) { rightCheckBoxesChecked = 0; }
        if (isNaN(checkBoxCheckedResult)) checkBoxCheckedResult = 0;

        checkBoxCheckedResult += rightCheckBoxesChecked;   

        if (typeof checkBox !== 'undefined') {
            let questionItem = checkBox.parentElement;

            var resDiv = document.createElement('div');
            resDiv.setAttribute("id", "resultIcon");

            if (rightCheckBoxesChecked == 1) {
                questionItem.classList.add('rightAnswerBackground');
              
                resDiv.classList.add('trueIcon');
                questionItem.appendChild(resDiv);
            }
            if (rightCheckBoxesChecked == 0) {
                questionItem.classList.add('wrongAnswerBackground');

                resDiv.classList.add('falseIcon');
                questionItem.appendChild(resDiv);
            }
            if (rightCheckBoxesChecked > 0 & rightCheckBoxesChecked < 1) {
                questionItem.classList.add('warningBackground');  

                resDiv.classList.add('warningIcon');
                questionItem.appendChild(resDiv);
            }
        }
   
            
        rightCheckBoxesChecked = 0;
        trueCheckBoxes = 0;
    }
    if (!isNaN(checkBoxCheckedResult)) {
        rightAnswers += checkBoxCheckedResult;
    }

    rightAnswersPercent=Math.trunc(rightAnswers/questionCount*100)+'%';

    var result = document.getElementById('result');
    result.innerHTML = "";
    result.innerHTML = "<p></p>Ваш результат:<br> Баллы: <b>" + rightAnswers.toFixed(2) + "</b> из <b>" + questionCount + "</b><br> Тест пройден на ";

    var descB=document.createElement('b');
    descB.setAttribute("id","percent");
    descB.innerHTML = ""+rightAnswersPercent+"";

    result.appendChild(descB);

    var submitButton = document.getElementById('submitButton');
    submitButton.classList.add('disabled');

     var resultButton = document.getElementById('saveResult');
    resultButton.classList.remove('resultButton');

    
    //var icons = document.getElementsByClassName('icon');

    //for (var i = 0; i < icons.length; i++) {
    //    icons[i].classList.remove('icon');
    //    icons[i].classList.add('visIcon');
    //}
}
window.onload = function () {
    $('#testList').load("/Home/ShowTests");
}

function chooseCategory(el) {
          $('#testList').load("/Home/ShowTests/?id="+el.id);      
}

function allTests() {
    $('#testList').load("/Home/ShowTests");
}

function getChecked(question, answer) {
    var inputs = document.getElementsByName('Q' + question);
    if (inputs[answer].checked) {
        inputs[answer].checked = false;
    } else {
        inputs[answer].checked = true;
    }
}
