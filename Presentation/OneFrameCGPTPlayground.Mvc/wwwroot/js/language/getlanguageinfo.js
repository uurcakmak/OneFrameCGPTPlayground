"use strict";

var KTEditLanguageGeneral = function () {
    var editLanguageForm;
    var editLanguageSubmitButton;
    var editLanguageValidator;
    var editLanguageFlagReview;
    var editImage;

    var handleEditLanguageForm = function (e) {
        editLanguageValidator = FormValidation.formValidation(
            editLanguageForm,
            {
                fields: {
                    'Name': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Name"))
                        }
                    },
                    'Code': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Code"))
                        }
                    },
                    'Image': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Flag"))
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

        editLanguageSubmitButton.addEventListener('click', async function (e) {
            e.preventDefault();

            editLanguageValidator.validate().then(async function (status) {
                if (status !== 'Valid') {
                    return;
                }
                var formLanguageEdit = $(editLanguageForm);
                var formArray = formLanguageEdit.serializeArray();

                var objActiveIndex = formArray.findIndex((obj => obj.name === "IsActive"));
                formArray[objActiveIndex].value = $("input[name='IsActive']").is(":checked") ? "true" : "false";

                var objDefaultIndex = formArray.findIndex((obj => obj.name === "IsDefault"));
                formArray[objDefaultIndex].value = $("input[name='IsDefault']").is(":checked") ? "true" : "false";

                let imageBase64 = await readFileAsDataURL(editImage.files[0]);

                formArray.push({ name: "Image", value: imageBase64 ?? editLanguageFlagReview.src });

                Common.Ajax(formLanguageEdit.attr("method"), formLanguageEdit.attr("action"), formArray);
            });

        });

        editImage.addEventListener('change', async function (e) {
            let imageBase64 = await readFileAsDataURL(editImage.files[0]);
            editLanguageFlagReview.src = imageBase64;
            editLanguageFlagReview.alt = 'flag';
        });
    }

    return {
        init: function () {
            editLanguageForm = document.querySelector('#languageEditForm');
            editLanguageSubmitButton = document.querySelector('#languageEditButton');
            editLanguageFlagReview = document.querySelector('#editFlagPreview');
            editImage = document.querySelector('#editImage');

            handleEditLanguageForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTEditLanguageGeneral.init();
});
