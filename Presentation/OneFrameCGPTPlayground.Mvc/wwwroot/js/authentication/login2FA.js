"use strict";

var KT2FaGeneral = function () {
    var twoFaForm;
    var twoFaSubmitButton;
    var timerFunc;

    var timer = function () {
        var verificationTime = document.getElementById("VerificationTime").value;
        var startTime = new Date().getTime() + parseInt(verificationTime) * 1000;

        timerFunc = setInterval(function () {
            var now = new Date().getTime();
            var timeleft = startTime - now;

            var seconds = Math.floor((timeleft % (1000 * 60)) / 1000);

            document.getElementById("remainTime").innerHTML = seconds;

            if (seconds < 1) {
                clearInterval(timerFunc);
                document.getElementById("remainTime").innerHTML = "0";
                $("#twoFactorButton").css("visibility", "hidden");
                $("#resendButton").css("visibility", "visible");
            }
        }, 1000);
    }

    var handleTwoFaForm = function (e) {

        twoFaSubmitButton.addEventListener('click', function (e) {
            var inputs = $("#verificationCode").children();
            var verificationCode = "";
            for (var i = 0; i < inputs.length; i++) {
                verificationCode += inputs[i].value;
            }
            var formTwoFa = $(twoFaForm);
            var formArray = formTwoFa.serializeArray();
            var objIndex = formArray.findIndex((obj => obj.name === "VerificationCode"));
            formArray[objIndex].value = verificationCode;
            Common.Ajax(formTwoFa.attr("method"), formTwoFa.attr("action"), formArray);

        });
    }

    var resend = function () {
        $("#resendButton").click(function (e) {
            location.reload();
        });
    }

    var clearTimerHtml = function () {
        if (timerFunc) {
            clearInterval(timerFunc);
            timerFunc = null;
        }
        var timerElement = document.getElementById("divTimer");
        timerElement.parentNode.removeChild(timerElement);
    }

    var nextInput = function () {
        var container = $("#verificationCode").children();
        for (var i = 0; i < container.length; i++) {
            container[i].addEventListener('keyup', function (e) {
                var target = e.srcElement;
                var next = target;
                while (next = next.nextElementSibling) {
                    if (next == null)
                        break;
                    if (next.tagName.toLowerCase() === "input") {
                        next.focus();
                        break;
                    }
                }
            });
        }
    }

    return {
        init: function () {
            twoFaForm = document.querySelector('#twoFactorForm');
            twoFaSubmitButton = document.querySelector('#twoFactorButton');

            handleTwoFaForm();
            resend();
            nextInput();

            if (document.getElementById("VerificationType").value.toLowerCase().includes("auth")) {
                clearTimerHtml();
            }
            else {
                timer();
            }
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KT2FaGeneral.init();
});
