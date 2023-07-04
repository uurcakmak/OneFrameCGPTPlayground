function cancelModalResetForm(validator) {
    $("[data-bs-dismiss='modal']").on("click", function () {
        validator.resetForm();
    });
}

function submit(button, form, validator) {
    button.addEventListener('click', function (e) {
        e.preventDefault();
        validator.validate().then(function (status) {
            if (status !== 'Valid') {

                return;
            }
            Common.Ajax($(form).attr("method"), $(form).attr("action"), $(form).serializeArray());
        });
    });
}

function submitWithReturnHref(button, form, validator, href) {
    button.addEventListener('click', function (e) {
        e.preventDefault();
        validator.validate().then(function (status) {
            if (status !== 'Valid') {

                return;
            }
            Common.Ajax($(form).attr("method"), $(form).attr("action"), $(form).serializeArray(), null, function () {
                window.location.href = href;
            });
        });
    });
}

if (typeof module !== 'undefined') {
    module.exports = {
        submit,
        submitWithReturnHref
    };
}
