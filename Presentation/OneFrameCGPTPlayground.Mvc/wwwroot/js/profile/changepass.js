"use strict";

var KTChangePasswordGeneral = function () {
    var changePasswordForm;
    var changePasswordSubmitButton;
    var changePasswordValidator;

    var handleChangePasswordForm = function (e) {
        changePasswordValidator = FormValidation.formValidation(
            changePasswordForm,
            {
                fields: {

                    'currentPassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Password"))
                        }
                    },
                    'newPassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("NewPassword")),
                        }
                    },

                    'newPasswordConfirmation': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(),
                            identical: {
                                compare: function () {
                                    return changePasswordForm.querySelector('[name="newPassword"]').value;
                                },
                                message: L("PasswordConfirmValidationError")
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            currentPassword: false
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

        submit(changePasswordSubmitButton, changePasswordForm, changePasswordValidator);

        changePasswordForm.querySelector('input[name="newPassword"]').addEventListener('input', function () {
            if (this.value.length > 0) {
                changePasswordValidator.updateFieldStatus('newPassword', 'NotValidated');
            }
        });
    }

    var cancelChangePassword = function () {
        $("#cancelButton").on("click", function () {
            changePasswordValidator.resetForm();
            var array = $("[data-kt-password-meter-control='highlight']").children()

            for (var i = 0; i < array.length; i++) {
                array[i].classList.remove("active");
            }
        });
    }

    return {
        init: function () {
            changePasswordForm = document.querySelector('#changePasswordForm');
            changePasswordSubmitButton = document.querySelector('#changePasswordButton');

            handleChangePasswordForm();
            cancelChangePassword();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTChangePasswordGeneral.init();
});
