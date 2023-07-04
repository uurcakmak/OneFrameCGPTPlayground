"use strict";

var KTResetPasswordGeneral = function () {
    var resetPasswordForm;
    var resetPasswordSubmitButton;
    var resetPasswordValidator;

    var handleResetPasswordForm = function (e) {
        resetPasswordValidator = FormValidation.formValidation(
            resetPasswordForm,
            {
                fields: {
                    'Email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
                        }
                    },
                    'Password': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Password")),
                        }
                    },

                    'ConfirmPassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(),
                            identical: {
                                compare: function () {
                                    return resetPasswordForm.querySelector('[name="Password"]').value;
                                },
                                message: L("PasswordConfirmValidationError")
                            }
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

        submitWithReturnHref(resetPasswordSubmitButton, resetPasswordForm, resetPasswordValidator,"/accounts/password-changed")
    }

    return {
        init: function () {
            resetPasswordForm = document.querySelector('#resetPasswordForm');
            resetPasswordSubmitButton = document.querySelector('#resetButton');

            handleResetPasswordForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTResetPasswordGeneral.init();
});
