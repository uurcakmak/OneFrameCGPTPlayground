"use strict";

var KTForgotPasswordGeneral = function () {
    var forgotPasswordForm;
    var forgotPasswordSubmitButton;
    var forgotPasswordValidator;

    var handleForgotPasswordForm = function (e) {
        forgotPasswordValidator = FormValidation.formValidation(
            forgotPasswordForm,
            {
                fields: {
                    'Email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
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

        submitWithReturnHref(forgotPasswordSubmitButton, forgotPasswordForm, forgotPasswordValidator, "/accounts/password-email-sent");
    }

    return {
        init: function () {
            forgotPasswordForm = document.querySelector('#forgotPasswordForm');
            forgotPasswordSubmitButton = document.querySelector('#forgotPasswordButton');

            handleForgotPasswordForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTForgotPasswordGeneral.init();
});
