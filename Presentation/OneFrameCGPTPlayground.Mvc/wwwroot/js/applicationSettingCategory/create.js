"use strict";

var KTCreateApplicationSettingCategoryGeneral = function () {
    var createAppSettingCategoryForm;
    var createAppSettingCategorySubmitButton;
    var createAppSettingCategoryValidator;

    var handleCreateAppSettingCategory = function (e) {
        createAppSettingCategoryValidator = FormValidation.formValidation(
            createAppSettingCategoryForm,
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

        submit(createAppSettingCategorySubmitButton, createAppSettingCategoryForm, createAppSettingCategoryValidator);

    }


    return {
        init: function () {
            createAppSettingCategoryForm = document.querySelector('#appSettingCategoryCreateForm');
            createAppSettingCategorySubmitButton = document.querySelector('#appSettingCategoryCreateButton');

            handleCreateAppSettingCategory();
            cancelModalResetForm(createAppSettingCategoryValidator);
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTCreateApplicationSettingCategoryGeneral.init();
});
