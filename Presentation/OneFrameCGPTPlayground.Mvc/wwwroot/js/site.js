if (!String.format) {
    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        format = format + "";
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

var ValidationMessages = {
    notEmpty: function (fieldName) {
        if (fieldName === undefined || fieldName === null || fieldName === '') {
            return {
                message: L("Required")
            };
        }
        return {
            message: String.format(L("Required2"), fieldName)
        };
    },
    emailValidation: function (fieldName) {
        if (fieldName === undefined || fieldName === null || fieldName === '') {
            return {
                message: L("EmailValidationMessage")
            };
        }
        return {
            message: String.format(L("InvalidEmail"), fieldName)
        };
    },
    stringLength: function (minLength) {
        var minLengthVal = parseInt(minLength) || 0;
        if (minLengthVal === 0) {
            return {
                min: minLength,
                message: L("MinLengthValidationMessage")
            };
        }
        return {
            min: minLength,
            message: String.format(L("MinLengthValidationMessage"), minLength)
        };
    }
};

if (typeof module !== 'undefined') {
    module.exports = { ValidationMessages };
}
