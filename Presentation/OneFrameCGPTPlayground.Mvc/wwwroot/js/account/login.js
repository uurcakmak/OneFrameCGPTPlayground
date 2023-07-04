"use strict";

var KTSigninGeneral = function () {
    var signinForm;
    var signinSubmitButton;
    var signinValidator;

    var handleSigninForm = function (e) {
        signinValidator = FormValidation.formValidation(
            signinForm,
            {
                fields: {
                    'email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
                        }
                    },
                    'password': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Password")),
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

        submit(signinSubmitButton, signinForm, signinValidator);
    }

    var removeProfilePhotoStorage = function () {
        localStorage.removeItem('PROFILE_PHOTO');
    }
    return {
        init: function () {
            removeProfilePhotoStorage();
            signinForm = document.querySelector('#kt_sign_in_form');
            signinSubmitButton = document.querySelector('#kt_sign_in_submit');
            handleSigninForm();
        }
    }
}();

KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});