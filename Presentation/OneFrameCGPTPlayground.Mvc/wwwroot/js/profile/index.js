var FileUploadSample = function () {
    var getOptions = function () {
        var url = "/configurations/file-uploader-options";
        Common.Ajax("GET", url, null, "json", function (data) {
            getOptionsData(data);
        });
    }

    return {
        init: function () {
            getOptions();
            closeModal();
        }
    };
}();

function getOptionsData(data) {
    var currentFile = null;
    var profilePhotoBase64 = '';
    var profilePhotoSize = parseInt(document.getElementById("jsProfileInfo").getAttribute("data-size"));

    data.Result.init = function () {

        this.on("addedfile", function (file) {
            if (currentFile) {
                this.removeFile(currentFile);
            }
            currentFile = file;

            let reader = new FileReader();
            reader.onload = function (event) {
                profilePhotoBase64 = event.target.result;
            };
            reader.readAsDataURL(file);
        });

        var component = this;

        $("#fileUploadButton").click(function (e) {
            if (component.files.length === 0) {
                showNotification(L("FileDoesNotExist"), "Warning");
            }
            if (component.files[0].size > profilePhotoSize) {
                showNotification(L("FileSize"), "Warning");
            }
            else {
                e.preventDefault();
                component.processQueue();
            }
        });

        this.on('sending', function (file, xhr, formData) {
            formData.append("profilePhotoBase64", profilePhotoBase64);
        });

        this.on("success", function (file, response) {
            if (response) {
                hideModal();
                clearModal();
                setProfilPhoto(profilePhotoBase64);
            }
        });
    }

    data.Result.accept = function (file, done) {
        if (!(file.type.includes('image'))) {
            done("Error! Files of this type are not accepted");
        }
        else { done(); }
    }

    FileUpload("#oneframe_file_upload", data.Result);
}

function clearModal() {
    RemoveFiles();
}

function hideModal() {
    $("#kt_modal_oneframe_file_upload").modal('hide');
}

function closeModal() {
    $("#kt_modal_oneframe_file_upload").on('hidden.bs.modal', function () {
        RemoveFiles();
    });
}

$('.deleteAvatar').click(function () {
    Common.Ajax("POST", "/profiles/delete-profile-photo");
    var defaultPhoto = document.getElementById("jsProfileInfo").getAttribute("data-default");
    setProfilPhoto(defaultPhoto);
})

function setProfilPhoto(profilePhotoBase64) {
    $('#profile_photo').attr('src', profilePhotoBase64)
    $('.profile_photo').css('background-image', 'url(' + profilePhotoBase64 + ')');
    localStorage.setItem('PROFILE_PHOTO', profilePhotoBase64);
    getLayoutProfilePhoto(profilePhotoBase64);
}

var KTProfileEditGeneral = function () {
    var profileEditForm;
    var profileEditSubmitButton;
    var profileEditValidator;

    var handleProfileEditForm = function (e) {
        profileEditValidator = FormValidation.formValidation(
            profileEditForm,
            {
                fields: {
                    'Name': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Name")),
                            stringLength: ValidationMessages.stringLength(2)
                        }
                    },
                    'Surname': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Surname")),
                            stringLength: ValidationMessages.stringLength(2)
                        }
                    },
                    'Email': {
                        validators: {
                            notEmpty: ValidationMessages.notEmpty(L("Email")),
                            emailAddress: ValidationMessages.emailValidation()
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

        profileEditSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();
            profileEditValidator.validate().then(function (status) {
                if (status !== 'Valid') {
                    return;
                }
                Common.Ajax($(profileEditForm).attr("method"), $(profileEditForm).attr("action"), $(profileEditForm).serializeArray(), null, function () {
                    window.location.reload();
                });
            });
        });
    }

    return {
        init: function () {
            profileEditForm = document.querySelector('#ProfileEditForm');
            profileEditSubmitButton = document.querySelector('#kt_profile_edit_submit');

            handleProfileEditForm();
        }
    }
}();

KTUtil.onDOMContentLoaded(function () {
    FileUploadSample.init();
    KTProfileEditGeneral.init();
});
