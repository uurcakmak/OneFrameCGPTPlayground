"use strict";

var KTCreateApplicationSettingGeneral = function () {
    var createAppSettingForm;
    var createAppSettingSubmitButton;
    var createAppSettingValidator;

    var handleCreateAppSettingForm = function (e) {
        createAppSettingValidator = FormValidation.formValidation(
            createAppSettingForm,
            {
                fields: {
                    'Key': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Key"))
                        }
                    },
                    'Value': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Value"))
                        }
                    },
                    'ValueType': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("ValueType"))
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

        createAppSettingSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();
            createAppSettingValidator.validate().then(function (status) {
                if (status !== 'Valid') {
                    return;
                }
                var formAppSettingCreate = $(createAppSettingForm);
                var formArray = formAppSettingCreate.serializeArray();
                var objIndex = formArray.findIndex((obj => obj.name === "IsStatic"));
                if ($("input[name='IsStatic']").is(":checked")) {
                    formArray[objIndex].value = "true";
                }
                else {
                    formArray[objIndex].value = "false";
                }

                Common.Ajax(formAppSettingCreate.attr("method"), formAppSettingCreate.attr("action"), formArray);
            });

        });
    }

    return {
        init: function () {
            createAppSettingForm = document.querySelector('#appSettingCreateForm');
            createAppSettingSubmitButton = document.querySelector('#appSettingCreateButton');

            handleCreateAppSettingForm();
            cancelModalResetForm(createAppSettingValidator);
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTCreateApplicationSettingGeneral.init();
});
