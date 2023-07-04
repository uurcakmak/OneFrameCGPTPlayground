function showNotification(message, type) {
    switch (type) {
        case "Info":
            toastr.info(message);
            break;
        case "Success":
            toastr.success(message);
            break;
        case "Warning":
            toastr.warning(message);
            break;
        case "Error":
            toastr.error(message);
            break;
        default:
            toastr.info(message);
    }
}

var Common = {
    Ajax: function (httpMethod, url, data, type, successCallBack, errorCallBack, async, cache, contentType) {
        if (typeof type === "undefined" || type === null) {
            type = "json";
        }

        if (typeof async === "undefined" || async === null) {
            async = true;
        }

        if (typeof cache === "undefined" || cache === null) {
            cache = false;
        }

        if (typeof contentType === "undefined" || contentType === null) {
            contentType = "application/x-www-form-urlencoded; charset=UTF-8";
        }

        if (event !== undefined) {
            var target = (event.target || event.srcElement);
            if (target !== undefined) {
                if (!$(target).is("button")) {
                    target = $(target).parent("button");
                }
                $(target).addClass("disabled").attr("disabled", true);
                $(target).attr('data-kt-indicator', 'on');
            }
        }

        return $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            beforeSend: function (xhr) { xhr.setRequestHeader('Accept-Language', window.currentCulture); },
            data: data,
            dataType: type,
            contentType: contentType,
            async: async,
            cache: cache,
            success: function (data) {
                if (successCallBack && (typeof successCallBack == "function")) {
                    successCallBack(data);
                } else {
                    Common.DisplaySuccess(data);
                }
            },
            error: function (xhr, status, error) {
                if (errorCallBack && (typeof errorCallBack == "function")) {
                    errorCallBack(error);
                } else {
                    Common.AjaxFailureCallback(xhr, status, error);
                }
            }
        });
    },
    AjaxFailureCallback: function (xhr, status, error) {
        var text = error;

        if (xhr.responseJSON !== undefined && xhr.responseJSON !== null) {
            var data = xhr.responseJSON.error;

            text = data.message;
            if (data.details !== undefined && data.details !== null) {
                text += "<br/>" + data.details;
            }

            if (data.validationErrors !== undefined && data.validationErrors !== null) {
                text += "<br/>";

                for (var errorMessage in data.validationErrors) {
                    text += "<br/>• " + errorMessage.ErrorMessage;
                }
            }
        }
        Common.DisplaySuccess({ redirectUrl: null, message: text, type: "Error" });
    },
    DisplaySuccess: function (data) {
        var element = $("*[data-kt-indicator]");
        if (element !== undefined) {
            $(element).removeAttr('data-kt-indicator');
        }
        if (data.redirectUrl) {
            if (element !== undefined) {
                $(element).text(L("Redirecting"));
            }
            window.location.href = data.redirectUrl;
        } else {
            if (element !== undefined) {
                $(element).removeClass("disabled").attr("disabled", false);
            }
            if (data.message !== undefined && data.message !== null) {
                showNotification(data.message, data.type);
            }
        }
    },
    FindValueInArray: function (array, key) {
        return array.find(function (x) {
            return x.name === key;
        }).value;
    },
    SerializeJsonObject: function (form) {
        var arrayData = form.serializeArray();
        var objectData = {};

        $.each(arrayData, function () {
            this.value = !this.value ? '' : this.value;
            serializeObjectSubObject(objectData, this.name, this.value);
        });

        return objectData;
    },
    ByteArrayToFileDownload: function (data) {
        var binaryString = atob(data.fileByteArray);
        var binaryLen = binaryString.length;
        var bytes = new Uint8Array(binaryLen);

        for (var i = 0; i < binaryLen; i++) {
            var ascii = binaryString.charCodeAt(i);
            bytes[i] = ascii;
        }

        var blob = new Blob([bytes], { type: "application/octetstream" });
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = data.fileName;
        link.click();
    }
};

window.setTimeout(function () {
    $(".alert").fadeTo(5000, 0).slideUp(1000,
        function () {
            $(this).remove();
        });
},
    4000);

function decodeJSON(data) {
    try {
        return JSON.parse(data);
    } catch (objError) {
        if (objError instanceof SyntaxError) {
            console.error(objError.name);
        } else {
            console.error(objError.message);
        }
    }
    return undefined;
}

function encodeJSON(data) {
    try {
        return JSON.stringify(data);
    } catch (objError) {
        if (objError instanceof SyntaxError) {
            console.error(objError.name);
        } else {
            console.error(objError.message);
        }
    }
    return undefined;
}

function Swal2Delete(path, tableId) {
    swal.fire({
        title: L("AreYouSure"),
        text: L("YouWontBeAbleToRevertThis"),
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: L("YesDoIt"),
        cancelButtonText: L("NoCancel"),
        closeOnConfirm: false
    }).then(function (e) {
        if (e.value) {
            Common.Ajax("Delete", path, null, null, function () {
                swal.fire(L("Deleted"), L("YourRecordHasBeenDeleted"), "success");
                if (!NullControl(tableId))
                {
                    $('#' + tableId).DataTable().ajax.reload();
                }
            });
        }
    });
}

function ConfirmBox(callback) {
    swal.fire({
        title: L("AreYouSure"),
        text: L("YouWontBeAbleToRevertThis"),
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: L("YesDoIt"),
        cancelButtonText: L("NoCancel"),
        closeOnConfirm: false
    }).then(function (e) {
        if (e.value) {
            callback();
        }
    });
}

function DialogBox(title, inputType, inputPlaceholder, validationMessage, buttonText, callback) {
    Swal.fire({
        title: title,
        input: inputType, //'text', 'email'
        inputAttributes: {
            autocapitalize: 'off'
        },
        inputPlaceholder: inputPlaceholder,
        validationMessage: validationMessage,
        showCancelButton: true,
        confirmButtonText: buttonText,
        cancelButtonText: L("NoCancel"),
        showLoaderOnConfirm: true,
        preConfirm: (input) => {
            return callback(input);
        },
        allowOutsideClick: () => !Swal.isLoading()
    });
}

function InfoBox(message) {
    Swal.fire({
        confirmButtonText: L("Ok"),
        text: message
    });
}

function getUrlParameter(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)");
    var results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function serializeObjectSubObject(obj, key, value) {
    if (key.indexOf('.') !== -1) {
        var attrs = key.split('.');
        var tx = obj;
        for (var i = 0; i < attrs.length - 1; i++) {
            var isArray = attrs[i].indexOf('[') !== -1;
            var isNestedArray = isArray && (i !== attrs.length - 1);
            var nestedArrayIndex;
            if (isArray) {
                nestedArrayIndex = attrs[i].substring(attrs[i].indexOf('[') + 1, attrs[i].indexOf(']'));
                attrs[i] = attrs[i].substring(0, attrs[i].indexOf('['));
                if (tx[attrs[i]] === undefined) {
                    tx[attrs[i]] = [];
                }
                tx = tx[attrs[i]];
                if (isNestedArray) {
                    if (tx[nestedArrayIndex] === undefined) {
                        tx[nestedArrayIndex] = {};
                    }
                    tx = tx[nestedArrayIndex];
                }
            } else {
                if (tx[attrs[i]] === undefined) {
                    tx[attrs[i]] = {};
                }
                tx = tx[attrs[i]];
            }
        }
        serializeObjectSubObject(tx, attrs[attrs.length - 1], value);
    } else {
        var finalArrayIndex = null;
        if (key.indexOf('[') !== -1) {
            finalArrayIndex = key.substring(key.indexOf('[') + 1, key.indexOf(']'));
            key = key.substring(0, key.indexOf('['));
        }
        if (finalArrayIndex == null) {
            obj[key] = value;
        } else {
            if (obj[key] === undefined) {
                obj[key] = [];
            }
            obj[key][finalArrayIndex] = value;
        }
    }
}

function MultipleEmailRegexWithComma(input) {
    var regex = /^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,4},?)+$/;
    return regex.test(input);
}

async function readFileAsDataURL(file) {
    if (file != null) {
        let result_base64 = await new Promise((resolve) => {
            let fileReader = new FileReader();
            fileReader.onload = (e) => resolve(fileReader.result);
            fileReader.readAsDataURL(file);
        });

        return result_base64;
    } else {
        return null;
    }
}

function NullControl(value) {
    if (value === null || value === "" || value === 'undefined' || value === undefined) {
        return true;
    }
    return false;
}

if (typeof module !== 'undefined') {
    module.exports = {
        NullControl,
        MultipleEmailRegexWithComma,
        getUrlParameter,
        encodeJSON,
        decodeJSON,
        Common,
        showNotification,
        InfoBox,
        serializeObjectSubObject,
        Swal2Delete
    };
}
