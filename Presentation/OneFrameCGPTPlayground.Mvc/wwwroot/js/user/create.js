"use strict";

var KTCreateUserGeneral = function () {
    var createUserForm;
    var createUserButton;
    var createUserValidator;

    var handleUserForm = function (e) {
        createUserValidator = FormValidation.formValidation(
            createUserForm,
            {
                fields: {
                    'Email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
                        }
                    },
                    'PhoneNumber': {
                        validators: {
                            stringLength: {
                                message: L("PhoneNumberValidationMessage"),
                                min:11,
                                max:11
                            }
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

        submit(createUserButton, createUserForm, createUserValidator);
    }

    return {
        init: function () {
            createUserForm = document.querySelector('#userCreateForm');
            createUserButton = document.querySelector('#userCreateButton');

            handleUserForm();
            cancelModalResetForm(createUserValidator);
        }
    };
}();

function getTimeZones() {
    Common.Ajax("Get", "configurations/time-zones", null, null, function (response) {
        var timezoneSelect = $("#createTimeZoneSelect");
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

KTUtil.onDOMContentLoaded(function () {
    getTimeZones();
    KTCreateUserGeneral.init();

    $('#createTimeZoneSelect').select2({
        dropdownParent: $('#modalNewUser')
    });
});
