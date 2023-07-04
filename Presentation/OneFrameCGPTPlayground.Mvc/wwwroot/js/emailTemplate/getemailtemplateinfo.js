"use strict";

var KTEditEmailTemplateGeneral = function () {
    var editEmailTemplateForm;
    var editEmailTemplateSubmitButton;
    var defaultLanguageIsoTwoLetterName = document.getElementById("jsEmailTemplateInfo").getAttribute("data-default-language")

    var convertTextareaToTiny = function () {
        tinymce.remove();
        tinymce.init({
            selector: '.editor',
            mode: "exact",
            plugins: [
                'advlist autolink lists link image charmap print preview anchor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime media table image paste imagetools wordcount'
            ],
            paste_data_images: true,
            file_picker_types: 'file image media',
            toolbar: 'language insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
            content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
            images_upload_handler: function (blobInfo, success, failure) {
                success("data:" + blobInfo.blob().type + ";base64," + blobInfo.base64());
            },
            language: defaultLanguageIsoTwoLetterName,
        });
    }

    var emailFormatCheck = function () {
        var result = false;
        var array = $.find(".multipleEmail");
        for (var i = 0; i < array.length; i++) {
            var item = array[i];
            if (NullControl(item.value)) {
                result = true;
            }
            else if (MultipleEmailRegexWithComma(item.value) === false) {
                result = false;
                break;
            }
        }

        return result;
    }

    var emailInputsChange = function () {
        $(".multipleEmail").on("change", function () {
            if (!emailFormatCheck()) {
                InfoBox(L("MultipleEmailCheck"));
            }
        });
    }


    var sendExampleEmail = function () {
        $('#emailTemplateSendEmailButton').click(function (e) {
            e.preventDefault();
            tinyMCE.triggerSave();
            DialogBox(L('SendTryEmailTitle'), 'email', L('EmailPlaceHolder'), L('EmailValidationMessage'), L('SendEmail'), function (input) {
                var to = input;
                var url = "/email-templates/send-email";
                var index = $('.tab-content .active').find('input[name^="Translations"]')[0]["name"].split('.')[0];
                var subject = $('input[name="' + index + '.Subject"]').val();
                var content = $('textarea[name="' + index + '.EmailContent"]').val();
                var data = {
                    "To": to,
                    "Subject": subject,
                    "Content": content
                };
                Common.Ajax("POST", url, data);
            });
        });
    }

    var handleEmailTemplateForm = function (e) {
        editEmailTemplateSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();

            if (!emailFormatCheck()) {
                InfoBox(L("MultipleEmailCheck"));
                return;
            }

            tinymce.triggerSave();
            Common.Ajax($(editEmailTemplateForm).attr("method"), $(editEmailTemplateForm).attr("action"), $(editEmailTemplateForm).serializeArray());
        });
    }


    return {
        init: function () {
            editEmailTemplateForm = document.querySelector('#emailTemplateEditForm');
            editEmailTemplateSubmitButton = document.querySelector('#emailTemplateSaveButton');

            convertTextareaToTiny();
            emailInputsChange();
            sendExampleEmail();
            handleEmailTemplateForm();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTEditEmailTemplateGeneral.init();
});
