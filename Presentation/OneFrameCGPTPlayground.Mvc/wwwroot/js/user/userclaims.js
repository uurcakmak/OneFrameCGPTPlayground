"use strict";

var userList = $("#userList");

var KTUserClaimGeneral = function () {

    var userClaimStepper;
    var userClaimForm;
    var userClaimSubmitButton;
    var userClaimContinueButton;

    var userClaimStepperObj;
    var userClaimValidations = [];

    var initUserClaimStepper = function () {

        userClaimStepperObj = new KTStepper(userClaimStepper);

        userClaimStepperObj.on('kt.stepper.changed', function (stepper) {
            userClaimSubmitButton.classList.remove('d-inline-block');
            userClaimSubmitButton.classList.remove('d-none');
            userClaimContinueButton.classList.remove('d-none');
        });

        userClaimStepperObj.on('kt.stepper.next', function (stepper) {

            var validator = userClaimValidations[stepper.getCurrentStepIndex() - 1];

            if (validator) {
                validator.validate().then(function (status) {
                    if (status === 'Valid') {
                        stepper.goNext();

                        KTUtil.scrollTop();
                    }
                });
            } else {
                stepper.goNext();

                KTUtil.scrollTop();
            }

            $("#selectedUser").html($("#userList").val());
        });

        userClaimStepperObj.on('kt.stepper.previous', function (stepper) {
            stepper.goPrevious();
            KTUtil.scrollTop();
        });
    }

    var handleUserClaimForm = function () {
        userClaimSubmitButton.addEventListener('click', function (e) {

            var validator = userClaimValidations[0];

            validator.validate().then(function (status) {
                if (status === 'Valid') {

                    e.preventDefault();

                    userClaimSubmitButton.disabled = true;

                    userClaimSubmitButton.setAttribute('data-kt-indicator', 'on');

                    setTimeout(function () {

                        userClaimSubmitButton.removeAttribute('data-kt-indicator');

                        userClaimSubmitButton.disabled = false;

                        userClaimStepperObj.goNext();

                    }, 2000);

                    ConfirmBox(function () {
                        var selectedUserClaimList = $("#tree").jstree(true).get_selected();
                        var model = { name: userList.val(), selectedUserClaimList: selectedUserClaimList };
                        Common.Ajax("POST", "/users/user-claims", model);
                    });

                    listChange(userList, "/users/user-claims/");
                } else {
                    InfoBox(L("WizardValidationMultipleError"));
                }
            });
        });
    }

    var initUserClaimValidation = function () {
        userClaimValidations.push(FormValidation.formValidation(
            userClaimForm,
            {
                fields: {
                    userList: {
                        validators: {
                            notEmpty: {
                                message: L("UserRequired")
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        ));
    }

    return {

        init: function () {

            userClaimStepper = document.querySelector('#kt_create_account_stepper');
            userClaimForm = userClaimStepper.querySelector('#kt_create_account_form');
            userClaimSubmitButton = userClaimStepper.querySelector('[data-kt-stepper-action="submit"]');
            userClaimContinueButton = userClaimStepper.querySelector('[data-kt-stepper-action="next"]');

            initUserClaimStepper();
            initUserClaimValidation();
            handleUserClaimForm();

        }
    };
}();

var tree = $("#tree");
setTree();
listChange(userList, "/users/user-claims/");

KTUtil.onDOMContentLoaded(function () {
    KTUserClaimGeneral.init();
});
