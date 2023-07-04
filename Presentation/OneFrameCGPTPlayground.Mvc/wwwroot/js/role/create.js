"use strict";

var KTCreateRoleGeneral = function () {
    var createRoleForm;
    var createRoleSubmitButton;
    var createRoleValidator;
    var handleForm = function (e) {
        createRoleValidator = FormValidation.formValidation(
            createRoleForm,
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

        submit(createRoleSubmitButton, createRoleForm, createRoleValidator);
    }

    return {
        init: function () {
            createRoleForm = document.querySelector('#roleCreateForm');
            createRoleSubmitButton = document.querySelector('#roleCreateButton');

            handleForm();
            cancelModalResetForm(createRoleValidator);
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTCreateRoleGeneral.init();
});
