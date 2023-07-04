"use strict";

var KTPasswordExpiredGeneral = function () {
    var passwordExpiredForm;
    var passwordExpiredSubmitButton;
    var passwordExpiredValidator;
    var passwordMeter;

    var validatePassword = function () {
        return (passwordMeter.getScore() === 100);
    }

    var handlePasswordExpiredForm = function (e) {
        passwordExpiredValidator = FormValidation.formValidation(
            passwordExpiredForm,
            {
                fields: {
                    'UserName': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
                        }
                    },
                    'CurrentPassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("CurrentPassword"))
                        }
                    },
                    'NewPassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("NewPassword")),
                            callback: {
                                message: L("PleaseEnterValidPassword"),
                                callback: function (input) {
                                    if (input.value.length > 0) {
                                        return validatePassword();
                                    }
                                }
                            }
                        }
                    },
                    'NewPasswordConfirmation': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(),
                            identical: {
                                compare: function () {
                                    return form.querySelector('[name="NewPassword"]').value;
                                },
                                message: L("PasswordConfirmValidationError")
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: false
                        }
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        passwordExpiredSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();

            passwordExpiredValidator.revalidateField('NewPassword');

            passwordExpiredValidator.validate().then(function (status) {
                if (status !== 'Valid') {
                    return;
                }
                Common.Ajax($(passwordExpiredForm).attr("method"), $(passwordExpiredForm).attr("action"), $(passwordExpiredForm).serializeArray());
            });
        });

        passwordExpiredForm.querySelector('input[name="NewPassword"]').addEventListener('input', function () {
            if (this.value.length > 0) {
                passwordExpiredValidator.updateFieldStatus('NewPassword', 'NotValidated');
            }
        });
    }


    return {
        init: function () {
            passwordExpiredForm = document.querySelector('#passwordExpiredForm');
            passwordExpiredSubmitButton = document.querySelector('#changePasswordButton');
            passwordMeter = KTPasswordMeter.getInstance(form.querySelector('[data-kt-password-meter="true"]'));

            handlePasswordExpiredForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTPasswordExpiredGeneral.init();
});
