var site = require('./site.js');

var message = "message";

beforeAll(() => {
    jest.clearAllMocks();
    L = jest.fn().mockReturnValue(message);
});

test("notEmpty returns Required message when fieldName is null", () => {
    expect(site.ValidationMessages.notEmpty(null)).toStrictEqual({"message": message});
});

test("notEmpty returns Required2 message", () => {
    expect(site.ValidationMessages.notEmpty("fieldName")).toStrictEqual({"message": message});
});

test("emailValidation returns EmailValidationMessage message when fieldName is null", () => {
    expect(site.ValidationMessages.emailValidation(null)).toStrictEqual({"message": message});
});

test("emailValidation returns InvalidEmail message", () => {
    expect(site.ValidationMessages.emailValidation("fieldName")).toStrictEqual({"message": message});
});

test("stringLength returns MinLengthValidationMessage message when length is 0", () => {
    expect(site.ValidationMessages.stringLength(0)).toStrictEqual({"message": message,"min": 0});
});

test("stringLength returns MinLengthValidationMessage message when length is different from 0", () => {
    expect(site.ValidationMessages.stringLength(3)).toStrictEqual({"message": message,"min": 3});
});
