"use strict";

var KTEditUserGeneral = function () {
    var editUserform;
    var editUserButton;
    var editUserValidator;

    var handleEditUserForm = function (e) {
        editUserValidator = FormValidation.formValidation(
            editUserform,
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
                                min: 11,
                                max: 11
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

        editUserButton.addEventListener('click', function (e) {
            e.preventDefault();
            editUserValidator.validate().then(function (status) {
                if (status !== 'Valid') {

                    return;
                }
                var formUserEdit = $(editUserform);
                var data = formUserEdit.serializeArray();
                var timeZone = document.getElementById("updateTimeZoneSelect").value;
                data.find(e => e.name === 'TimeZone').value = timeZone;
                Common.Ajax(formUserEdit.attr("method"), formUserEdit.attr("action"), data);
            });
        });
    }

    return {
        init: function () {
            editUserform = document.querySelector('#userEditForm');
            editUserButton = document.querySelector('#userEditButton');

            handleEditUserForm();
        }
    };
}();


function getTimeZones() {
    Common.Ajax("Get", "configurations/time-zones", null, null, function (response) {
        var selectedTimeZone = $("#selectedTimeZone").val();
        var timeZoneSelect = $("#updateTimeZoneSelect");
        timeZoneSelect.empty();
        response.map(function (item, index) {
            var option = '<option';
            if (item.id === selectedTimeZone) {
                option += ' selected'
            }
            option += '></option>';
            timeZoneSelect.append($(option).val(item.id).html(item.displayName))
        });
        timeZoneSelect.select2({
            dropdownParent: $("#updateSelect")
        });
    });
}

KTUtil.onDOMContentLoaded(function () {
    getTimeZones();
    KTEditUserGeneral.init();
});
