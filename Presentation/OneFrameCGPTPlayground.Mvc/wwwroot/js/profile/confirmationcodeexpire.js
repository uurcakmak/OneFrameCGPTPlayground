var confirmationCodeSubmitButton;

function TakeNewCode() {
    window.location.reload();
}

function timer() {
    var countDownDate = document.getElementById("ExpiredDate").value;
    var takeNewCodeButton = document.getElementById("btnTakeNewCode");
    takeNewCodeButton.addEventListener('click', TakeNewCode);
    var remainTimeElement = document.getElementById("kt_remainTime");

    var countDownInterval = setInterval(function () {

        var now = Math.floor(new Date().getTime() / 1000);
        var distance = countDownDate - now;
        remainTimeElement.innerHTML = distance < 0 ? 0 : distance;
        if (distance < 1) {
            clearInterval(countDownInterval);
            confirmationCodeSubmitButton.style.display = "none";
            takeNewCodeButton.style.display = "block";
        }

    }, 1000);
}

var KTConfirmationCodeGeneral = function () {
    var confirmationCodeForm;
    var confirmationCodeValidator;

    var handleConfirmationCodeForm = function (e) {
        confirmationCodeValidator = FormValidation.formValidation(
            confirmationCodeForm,
            {
                fields: {
                    'Code': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Code"))
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row'
                    })
                }
            }
        );

        submit(confirmationCodeSubmitButton, confirmationCodeForm, confirmationCodeValidator);
    }

    return {
        init: function () {
            confirmationCodeForm = document.querySelector('#ConfirmPhoneNumberForm');
            confirmationCodeSubmitButton = document.querySelector('#kt_confirmation_submit');

            handleConfirmationCodeForm();
        }
    }
}();

KTUtil.onDOMContentLoaded(function () {
    KTConfirmationCodeGeneral.init();

    timer();
});
