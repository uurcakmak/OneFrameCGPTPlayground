var autoLogoutListener = function () {
    var timer = null;
    var countdownTimer = null;
    var idleTimeout = 600000;
    var dialogTimeout = 30000;
    var dialogShowTime = 0;
    var logoutTime = 0;
    var dialogShown = false;
    var documentTitle = document.title;
    var isEnabled = true;

    var setTimeouts = function () {
        var idleTimeoutStorage = localStorage.getItem('AUTO_LOGOUT_IDLE_TIMEOUT');
        var dialogTimeoutStorage = localStorage.getItem('AUTO_LOGOUT_DIALOG_TIMEOUT');
        var dialogEnableStorage = localStorage.getItem('AUTO_LOGOUT_DIALOG_ENABLE');

        if (isEnabled) {
            if (NullControl(idleTimeoutStorage) || NullControl(dialogTimeoutStorage) || NullControl(dialogEnableStorage)) {
                Common.Ajax("GET", "/configurations/auto-logout", null, "json", function (data) {
                    idleTimeout = data.identityAutoLogoutIdleTimeout;
                    dialogTimeout = data.identityAutoLogoutDialogTimeout;
                    isEnabled = data.identityAutoLogoutIsEnabled;
                    localStorage.setItem('AUTO_LOGOUT_IDLE_TIMEOUT', idleTimeout);
                    localStorage.setItem('AUTO_LOGOUT_DIALOG_TIMEOUT', dialogTimeout);
                    localStorage.setItem('AUTO_LOGOUT_DIALOG_ENABLE', isEnabled);

                }, null, false, false);
            } else {
                idleTimeout = idleTimeoutStorage;
                dialogTimeout = dialogTimeoutStorage;
                isEnabled = Boolean(dialogEnableStorage);
            }
        }
    }

    var logout = function () {
        window.location.href = logoutUrl;
    }

    var changeProgressBar = function () {
        var remainingMilliseconds = logoutTime - $.now();
        var progressDiv = $('#auto-logout-dialog-progress');
        progressDiv.attr('aria-valuenow', (remainingMilliseconds / dialogTimeout) * 100);
        progressDiv.css('width', progressDiv.attr('aria-valuenow') + '%');
    }

    var loginExpireTimeChecker = function () {
        changeProgressBar();
        if ($.now() > logoutTime) {
            logout();
        }
    }

    var openDialog = async function () {
        var dialogModal = $('#auto-logout-dialog');
        if (!dialogModal.hasClass('show')) {
            dialogModal.modal('show');
            document.title = L('AutoLogoutDocumentTitle');
            countdownTimer = window.setInterval(loginExpireTimeChecker, 300);
        }
    }
   
    var autoLogoutChecker = function () {
        var lastActivityTime = parseInt(localStorage.getItem('AUTO_LOGOUT_LAST_ACTIVITY_TIME'));
        dialogShowTime = new Date(lastActivityTime).setMilliseconds(idleTimeout);
        logoutTime = new Date(dialogShowTime).setMilliseconds(dialogTimeout);
        if ($.now() > dialogShowTime) {
            if (!dialogShown) {
                openDialog();
            }
        }
        timer = setTimeout(autoLogoutChecker, 2000);
    }

    var startTimer = function () {
        clearTimeout(timer);
        localStorage.setItem('AUTO_LOGOUT_LAST_ACTIVITY_TIME', $.now());
        autoLogoutChecker();
    }

    return {
        init: function () {
         
            setTimeouts();
            if (isEnabled) {
                $('body').on('load scroll mousemove dblclick contextmenu click keypress pageshow resize focus drag copy cut paste', function () {
                    if (!dialogShown) {
                        startTimer();
                    }
                });
                startTimer();
            }

                var confirmModal = $('#auto-logout-dialog');

                confirmModal.find('#logout').click(function (event) {
                    dialogShown = false;
                    logout();
                });

            confirmModal.find('#stayLoggedIn').click(function (event) {
                dialogShown = false;
                $(this).data('bs.modal', null);
                clearInterval(countdownTimer);
                startTimer();
                document.title = documentTitle;
                confirmModal.modal('hide');
            });


            confirmModal.on('show.bs.modal', function () {
                dialogShown = true;
            });
        }
    }
}();

$(document).ready(function () {
    autoLogoutListener.init();
});
