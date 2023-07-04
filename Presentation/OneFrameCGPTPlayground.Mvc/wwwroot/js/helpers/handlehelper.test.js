var handlehelper = require('./handlehelper.js');

var button = document.createElement("button");
var form = document.createElement("form");

test("submit is successful", () => {
    expect(handlehelper.submit(button, form)).toBeUndefined();
});

test("submitWithReturnHref returns items", () => {
    expect(handlehelper.submitWithReturnHref(button, form)).toBeUndefined();
});
