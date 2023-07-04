"use strict";

var KTSignupGeneral = function () {
    var signupForm;
    var signupSubmitButton;
    var signupValidator;
    var signupPasswordMeter;

    var validatePassword = function () {
        return (signupPasswordMeter.getScore() === 100);
    }

    var getTimeZoneList = function () {
        Common.Ajax("Get", "/configurations/time-zones", null, null, function (response) {
            var timezoneSelect = $("#ddlTimeZone");
            timezoneSelect.empty();
            response.map(function (item, index) {
                var option = '<option';
                if (item.id === 'Europe/Istanbul') {
                    option += ' selected'
                }
                option += '></option>';
                timezoneSelect.append($(option).val(item.id).html(item.displayName))
            });
        });
    }

    var handleSignupForm = function (e) {
        signupValidator = FormValidation.formValidation(
            signupForm,
            {
                fields: {
                    'name': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Name"))
                        }
                    },
                    'surname': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Surname"))
                        }
                    },
                    'email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
                        }
                    },
                    'password': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Password")),
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
                    'confirmpassword': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(),
                            identical: {
                                compare: function () {
                                    return signupForm.querySelector('[name="password"]').value;
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

        signupSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();

            signupValidator.revalidateField('password');

            signupValidator.validate().then(function (status) {
                if (status !== 'Valid') {
                    return;
                }
                Common.Ajax($(signupForm).attr("method"), $(signupForm).attr("action"), $(signupForm).serializeArray(), null, function () {
                    window.location.href = "/accounts/register-email-sent";
                });
            });
        });

        signupForm.querySelector('input[name="password"]').addEventListener('input', function () {
            if (this.value.length > 0) {
                signupValidator.updateFieldStatus('password', 'NotValidated');
            }
        });
    }

    return {
        init: function () {
            signupForm = document.querySelector('#kt_sign_up_form');
            signupSubmitButton = document.querySelector('#kt_sign_up_submit');
            signupPasswordMeter = KTPasswordMeter.getInstance(signupForm.querySelector('[data-kt-password-meter="true"]'));

            handleSignupForm();
            getTimeZoneList();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTSignupGeneral.init();
});
