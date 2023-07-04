"use strict";

var KTEditApplicationSettingGeneral = function () {
    var editAppSettingForm;
    var editAppSettingSubmitButton;
    var editAppSettingValidator;

    var handleEditAppSettingForm = function (e) {
        editAppSettingValidator = FormValidation.formValidation(
            editAppSettingForm,
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

        editAppSettingSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();
            editAppSettingValidator.validate().then(function (status) {
                if (status !== 'Valid') {

                    return;
                }
                var formAppSettingEdit = $(editAppSettingForm);
                var formArray = formAppSettingEdit.serializeArray();
                var objIndex = formArray.findIndex((obj => obj.name === "IsStatic"));
                if ($("input[name='IsStatic']").is(":checked")) {
                    formArray[objIndex].value = "true";
                }
                else {
                    formArray[objIndex].value = "false";
                }

                Common.Ajax(formAppSettingEdit.attr("method"), formAppSettingEdit.attr("action"), formArray);
            });

        });
    }

    return {
        init: function () {
            editAppSettingForm = document.querySelector('#appSettingEditForm');
            editAppSettingSubmitButton = document.querySelector('#appSettingEditButton');


            handleEditAppSettingForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTEditApplicationSettingGeneral.init();
});
