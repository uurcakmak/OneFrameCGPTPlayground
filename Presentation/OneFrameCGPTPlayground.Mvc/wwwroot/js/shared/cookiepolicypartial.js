var model = {} ;

function accept() {
    model.required = true;
    model.thirdPartyPartners = true;
    setCookiePolicy(model);
}

function reject() {
    model.required = true;
    model.thirdPartyPartners = false;
    setCookiePolicy(model);
}

function setCookiePolicy(model) {
    var cookieReturnUrl = document.getElementById("jsCookie").getAttribute("data-returnUrl");
    Common.Ajax("Get", "/set-cookie-policy?returnUrl=" + cookieReturnUrl, model, "json", function (url) {
        window.location = url;
    });
}