function saveResult() {
    var percent =document.getElementById('percent').innerHTML;
    var testId =  parseInt(document.querySelector("input[name='test']").value);
  
    var newPercent ='';
    for (let i = 0; i < percent.length; i++)
    {
        if (percent[i] != '%')
        {
            newPercent += percent[i];
        }
    }
    newPercent = parseInt(newPercent);
    var value = { 'percentPar': newPercent, 'testPar': testId }
    $.ajax({
        type: "Post",
        url: "/api/Values/Process",
        data: JSON.stringify(value),  
        dataType: "json",
        contentType: "application/json",
        success: function (res) {
            if (res.isValid) {
                alert(res.message);
            } else {
                alert(res.message);
            }
        }
    });
    //$.post('/api/Values/Process',
    //    {
    //        percentPar: JSON.stringify(newPercent),
    //        testPar: JSON.stringify(testId),
    //    },
    //    function (res) {
    //        if (res.isValid) {
    //            alert(res.message);
    //        } else {
    //            alert(res.message);
    //        }
    //    });       
}
function check() {
    var rightAnswers = 0,
        checkBoxCheckedResult = 0;

    var questionCount = parseInt(document.querySelector('input[name="questionCount"]').value) - 1;

    for (let i = 0; i < questionCount; i++) {
        var name = 'Q' + (i + 1);
        var inputs = document.getElementsByName(`${name}`);
        
        if (inputs[0].type == 'radio') {
            var checkedRadios = document.querySelector("input[type='radio'][name=" + name + "]:checked");
            if (checkedRadios == null) {
                decoratedNotice(inputs[0], "false");
            } else {
                if (checkedRadios.value == 'true') {
                    highlightAnswer(checkedRadios, "true");
                    decoratedNotice(checkedRadios, "true");
                    rightAnswers++;
                } else {
                    highlightAnswer(checkedRadios, "false");
                    decoratedNotice(checkedRadios, "false");
                }
            }          
        }
        if (inputs[0].type == 'checkbox') {
            var trueCheckBoxesCount = document.querySelectorAll("input[type='checkbox'][name=" + name + "][value='true']").length;
            var checkedCheckBoxes = document.querySelectorAll("input[type='checkbox'][name=" + name + "]:checked");

            var oneMark = 1 / trueCheckBoxesCount;

            if (checkedCheckBoxes.length == 0) {
                decoratedNotice(inputs[0], 'false');
            } else {
                for (let j = 0; j < checkedCheckBoxes.length; j++) {
                    if (checkedCheckBoxes[j].value == 'true') {
                        highlightAnswer(checkedCheckBoxes[j], 'true');
                        checkBoxCheckedResult += oneMark;
                    } else {
                        highlightAnswer(checkedCheckBoxes[j], 'false');
                        checkBoxCheckedResult -= oneMark;
                    }
                }               
                if (checkBoxCheckedResult < 0) {
                    checkBoxCheckedResult = 0;
                }
                if (checkBoxCheckedResult == 1) {
                    decoratedNotice(inputs[0], 'true');
                }
                if (checkBoxCheckedResult == 0) {
                    decoratedNotice(inputs[0], 'false');
                }
                if (checkBoxCheckedResult > 0 & checkBoxCheckedResult < 1) {
                    decoratedNotice(inputs[0], 'warning');
                }
                if (!isNaN(checkBoxCheckedResult)) {
                    rightAnswers += checkBoxCheckedResult;
                    checkBoxCheckedResult = 0;
                }
            }
           
        }
           
    }    
    resOutput(rightAnswers, questionCount);
}

function resOutput(res, questCount) {
    var rightAnswersPercent = Math.trunc(res / questCount * 100) + '%';

    var result = document.getElementById('result');
    result.innerHTML = "";
    result.innerHTML = "<p></p>Ваш результат:<br> Баллы: <b>" + res.toFixed(2) + "</b> из <b>" + questCount + "</b><br> Тест пройден на ";

    var descB = document.createElement('b');
    descB.setAttribute("id", "percent");
    descB.innerHTML = "" + rightAnswersPercent + "";

    result.appendChild(descB);

    var submitButton = document.getElementById('submitButton');
    submitButton.classList.add('disabled');

    var resultButton = document.getElementById('saveResult');
    resultButton.classList.remove('resultButton');
    }

function highlightAnswer(answer, value) {
    var parent = answer.parentElement;
    if (value == 'true') {
        parent.classList.add('rightAnswer');
    } else if (value == 'false') {
        parent.classList.add('wrongAnswer');
    }
}
function decoratedNotice(child, value) {
    
    var parent = child.parentElement;
    let questionDiv = parent.parentElement;

    var resDiv = document.createElement('div');
    resDiv.setAttribute("id", "resultIcon");

    if (value == 'true') {
        questionDiv.classList.add('rightAnswerBackground');

        resDiv.classList.add('trueIcon');
        questionDiv.appendChild(resDiv);
    } else if (value == 'false') {
        questionDiv.classList.add('wrongAnswerBackground');

        resDiv.classList.add('falseIcon');
        questionDiv.appendChild(resDiv);
    } else if (value == 'warning') {
        questionDiv.classList.add('warningBackground');

        resDiv.classList.add('warningIcon');
        questionDiv.appendChild(resDiv);
    }
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


//$('#pagination-container').pagination({
//    dataSource: [1, 2, 3, 4, 5, 6, 7, 195],
//    callback: function (data, pagination) {
//        var html = simpleTemplating(data);
//        $('#data-container').html(html);
//    }
//})

//function simpleTemplating(data) {
//    var html = '<ul>';
//    $.each(data, function (index, item) {
//        html += '<li>' + item + '</li>';
//    });
//    html += '</ul>';
//    return html;
//}
