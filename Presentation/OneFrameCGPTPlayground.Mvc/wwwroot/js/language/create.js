"use strict";

var KTCreateLanguageSettingGeneral = function () {
    var createLanguageForm;
    var createLanguageSubmitButton;
    var createLanguageValidator;
    var createLanguageFlagReview;
    var createImage;
    

    var handleCreateLanguageForm = function (e) {
        createLanguageValidator = FormValidation.formValidation(
            createLanguageForm,
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

        createLanguageSubmitButton.addEventListener('click', async function (e) {
            e.preventDefault();
            createLanguageValidator.validate().then(async function (status) {
                if (status !== 'Valid') {
                    return;
                }
                var formLanguageCreate = $(createLanguageForm);
                var formArray = formLanguageCreate.serializeArray();
                var objActiveIndex = formArray.findIndex((obj => obj.name === "IsActive"));
                formArray[objActiveIndex].value = $("input[name='IsActive']").is(":checked") ? "true" : "false";

                var objDefaultIndex = formArray.findIndex((obj => obj.name === "IsDefault"));
                formArray[objDefaultIndex].value = $("input[name='IsDefault']").is(":checked") ? "true" : "false";

                let imageBase64 = await readFileAsDataURL(createImage.files[0]);

                formArray.push({ name: "Image", value: imageBase64 });

                Common.Ajax(formLanguageCreate.attr("method"), formLanguageCreate.attr("action"), formArray);
            });

        });

        createImage.addEventListener('change', async function (e) {
            let imageBase64 = await readFileAsDataURL(createImage.files[0]);
            createLanguageFlagReview.src = imageBase64;
            createLanguageFlagReview.alt = 'flag';
        });
    }

    return {
        init: function () {
            createLanguageForm = document.querySelector('#languageCreateForm');
            createLanguageSubmitButton = document.querySelector('#languageCreateButton');
            createLanguageFlagReview = document.querySelector('#createFlagPreview');
            createImage = document.querySelector('#createImage');

            handleCreateLanguageForm();
            cancelModalResetForm(createLanguageValidator);
        }
    };
}();


KTUtil.onDOMContentLoaded(function () {
    KTCreateLanguageSettingGeneral.init();
});
