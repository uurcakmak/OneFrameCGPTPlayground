"use strict";

var KTEditApplicationSettingCategoryGeneral = function () {
    var editAppSettingCategoryForm;
    var editAppSettingCategorySubmitButton;
    var editAppSettingCategoryValidator;

    var handleEditAppSettingCategory = function (e) {
        editAppSettingCategoryValidator = FormValidation.formValidation(
            editAppSettingCategoryForm,
            {
                fields: {
                    'Name': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Name")),
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

        submit(editAppSettingCategorySubmitButton, editAppSettingCategoryForm, editAppSettingCategoryValidator);
    }

    return {
        init: function () {
            editAppSettingCategoryForm = document.querySelector('#appSettingCategoryEditForm');
            editAppSettingCategorySubmitButton = document.querySelector('#appSettingCategoryEditButton');

            handleEditAppSettingCategory();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTEditApplicationSettingCategoryGeneral.init();
});
