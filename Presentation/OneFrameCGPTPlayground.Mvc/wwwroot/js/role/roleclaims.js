"use strict";

var roleList = $("#roleList");

var KTRoleClaimGeneral = function () {

    var roleClaimStepper;
    var roleClaimForm;
    var roleClaimSubmitButton;
    var roleClaimContinueButton;

    var roleClaimStepperObj;
    var roleClaimValidations = [];

    var initRoleClaimStepper = function () {

        roleClaimStepperObj = new KTStepper(roleClaimStepper);

        roleClaimStepperObj.on('kt.stepper.changed', function (stepper) {
                roleClaimSubmitButton.classList.remove('d-inline-block');
                roleClaimSubmitButton.classList.remove('d-none');
                roleClaimContinueButton.classList.remove('d-none');
        });

        roleClaimStepperObj.on('kt.stepper.next', function (stepper) {

            var validator = roleClaimValidations[stepper.getCurrentStepIndex() - 1];

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

            $("#selectedRol").html($("#roleList").val());
        });

        roleClaimStepperObj.on('kt.stepper.previous', function (stepper) {
            stepper.goPrevious();
            KTUtil.scrollTop();
        });
    }

    var handleRoleClaimForm = function () {
        roleClaimSubmitButton.addEventListener('click', function (e) {

            var validator = roleClaimValidations[0];

            validator.validate().then(function (status) {
                if (status === 'Valid') {

                    e.preventDefault();

                    roleClaimSubmitButton.disabled = true;

                    roleClaimSubmitButton.setAttribute('data-kt-indicator', 'on');

                    setTimeout(function () {

                        roleClaimSubmitButton.removeAttribute('data-kt-indicator');

                        roleClaimSubmitButton.disabled = false;

                        roleClaimStepperObj.goNext();

                    }, 2000);

                    ConfirmBox(function () {
                        var selectedRoleClaimList = $("#tree").jstree(true).get_selected();
                        var model = { name: roleList.val(), selectedRoleClaimList: selectedRoleClaimList };
                        Common.Ajax("POST", "/roles/role-claims", model);
                    });

                    listChange(roleList, "/roles/role-claims/");
                } else {
                    InfoBox(L("WizardValidationMultipleError"));
                }
            });
        });
    }

    var initRoleClaimValidation = function () {
        roleClaimValidations.push(FormValidation.formValidation(
            roleClaimForm,
            {
                fields: {
                    roleList: {
                        validators: {
                            notEmpty: {
                                message: L("RoleRequired")
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

            roleClaimStepper = document.querySelector('#kt_create_account_stepper');
            roleClaimForm = roleClaimStepper.querySelector('#kt_create_account_form');
            roleClaimSubmitButton = roleClaimStepper.querySelector('[data-kt-stepper-action="submit"]');
            roleClaimContinueButton = roleClaimStepper.querySelector('[data-kt-stepper-action="next"]');

            initRoleClaimStepper();
            initRoleClaimValidation();
            handleRoleClaimForm();

        }
    };
}();
var tree = $("#tree");
setTree();
listChange(roleList, "/roles/role-claims/");

KTUtil.onDOMContentLoaded(function () {
    KTRoleClaimGeneral.init();
});
