document.getElementById("languageSelectList").onchange = function () {
    var url = "/set-language?culture=" + this.value + "&returnUrl=" + location.pathname;
    window.location = url;
};
