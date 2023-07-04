"use strict";

var KTEditRoleGeneral= function () {
    var editRoleForm;
    var editRoleSubmitButton;
    var editRoleValidator;

    var handleEditRoleForm = function (e) {
        editRoleValidator = FormValidation.formValidation(
            editRoleForm,
            {
                fields: {
                    'Name': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Name")),
                        }
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row'
                    })
                }
            }
        );

        submit(editRoleSubmitButton, editRoleForm, editRoleValidator);
    }

    return {
        init: function () {
            editRoleForm = document.querySelector('#roleEditForm');
            editRoleSubmitButton = document.querySelector('#roleEditButton');

            handleEditRoleForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTEditRoleGeneral.init();
});

