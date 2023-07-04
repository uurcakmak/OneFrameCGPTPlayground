var common = require('./common.js');

beforeEach(() => {
    jest.clearAllMocks();
});

test("NullControl returns true", () => {
    expect(common.NullControl(null)).toBeTruthy()
});

test("NullControl returns false", () => {
    expect(common.NullControl("Not Null")).toBeFalsy();
});

test("getUrlParameter returns null", () => {
    expect(common.getUrlParameter("")).toBeFalsy();
});

test("encodeJSON returns string", () => {
    expect(common.encodeJSON({x: 5, y: 6})).toBe('{"x":5,"y":6}');
});

test("encodeJSON returns undefined when there is an error", () => {
    var obj = {};
    obj.data1 = {data2:obj};
    expect(common.encodeJSON(obj)).toBeUndefined();
});

test("decodeJSON returns object", () => {
    expect(common.decodeJSON('{"data1":5,"data2":6}')).toStrictEqual({data1: 5, data2: 6});
});

test("decodeJSON returns undefined when there is a syntax error", () => {
    expect(common.decodeJSON()).toBeUndefined();
});

test("FindValueInArray returns value", () => {
    expect(common.Common.FindValueInArray([{ name:"string 1", value:"value 1"}, { name:"string 2", value:"value 2"}], "string 1")).toBe("value 1");
});

test("AjaxFailureCallback is successful", () => {
    var xhr = new XMLHttpRequest;
    var error = {message: "message", details: "details", validationErrors: "validationErrors"};
    xhr.responseJSON = {error: error};
    common.Common.DisplaySuccess = jest.fn();
    expect(() => common.Common.AjaxFailureCallback(xhr, "status", "error")).not.toThrow();
});

test("DisplaySuccess is successful when element is undefined", () => {
    var data = {message: "message", type: "Info"};
    $ = jest.fn().mockReturnValueOnce(undefined);
    toastr = jest.fn();
    toastr.info = jest.fn().mockReturnValueOnce(Object);
    expect(() => common.Common.DisplaySuccess(data)).not.toThrow();
});

test("Ajax is successful when element is undefined", () => {
    $ = jest.fn();
    $.ajax = jest.fn();
    expect(() => common.Common.Ajax("Delete", null, null, null, null, null, null, null, null)).not.toThrow();
});

test("MultipleEmailRegexWithComma returns false", () => {
    expect(common.MultipleEmailRegexWithComma("test1@kocsistem.com.tr,test2@kocsistem.com.tr")).toBeTruthy();
});

describe('showNotification', () => {
    beforeEach(() => {
        toastr = jest.fn();
        toastr.info = jest.fn().mockReturnValueOnce(Object);
        toastr.success = jest.fn().mockReturnValueOnce(Object);
        toastr.warning = jest.fn().mockReturnValueOnce(Object);
        toastr.error = jest.fn().mockReturnValueOnce(Object);
    });
    
    test("when type is Info", () => {
        expect(() => common.showNotification("message", "Info")).not.toThrow();
    });
    test("when type is Success", () => {
        expect(() => common.showNotification("message", "Success")).not.toThrow();
    });
    test("when type is Warning", () => {
        expect(() => common.showNotification("message", "Warning")).not.toThrow();
    });
    test("when type is Error", () => {
        expect(() => common.showNotification("message", "Error")).not.toThrow();
    });
    test("when there is no type", () => {
        expect(() => common.showNotification("message")).not.toThrow();
    });
})

test("InfoBox is successful", () => {
    Swal = jest.fn();
    Swal.fire = jest.fn();
    var message = "Ok";
    L = jest.fn().mockReturnValueOnce(message);
    expect(() => common.InfoBox("message")).not.toThrow();
});

test("SerializeJsonObject returns objectData", () => {
    var form = document.createElement("form");
    form.setAttribute("test1", "1");
    form.setAttribute("test2", "2");
    form.serializeArray = jest.fn().mockReturnValueOnce(["test1", "test2"]);
    $ = jest.fn();
    $.each = jest.fn();
    expect(common.Common.SerializeJsonObject(form)).toStrictEqual({});
});

test("ByteArrayToFileDownload is successful", () => {
    var data = {fileByteArray: ["00011011010000101000101100101010"]};
    window.URL.createObjectURL = jest.fn();
    expect(() => common.Common.ByteArrayToFileDownload(data)).not.toThrow();
});

describe('serializeObjectSubObject', () => {
    var obj = {};
    var key = "";
    var value = "value";
    test("when isArray is false", () => {
        key = "string.";
        expect(() => common.serializeObjectSubObject(obj, key, value)).not.toThrow();
    });
    test("when isArray is true", () => {
        key = "[1,2,3].string";
        expect(() => common.serializeObjectSubObject(obj, key, value)).not.toThrow();
    });
    test("when key.indexOf('.') !== -1 is false", () => {
        key = "string";
        expect(() => common.serializeObjectSubObject(obj, key, value)).not.toThrow();
    });
    test("when key.indexOf('.') !== -1 is false and key.indexOf('[') !== -1 is true", () => {
        key = "string[";
        expect(() => common.serializeObjectSubObject(obj, key, value)).not.toThrow();
    });
});
