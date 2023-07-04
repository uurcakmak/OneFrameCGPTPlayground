"use strict";

var init = function () {
    var ajax = {
        url: "/login-audit-logs/list",
        type: "Get",
        dataSrc: function (gridObj) {
            return gridObj.data.map(function (item, index) {
                return {
                    'ip': item.ip,
                    'hostname': item.hostname,
                    'macAddress': item.macAddress,
                    'applicationUserName': item.applicationUserName,
                    'osName': item.osName,
                    'browserDetail': item.browserDetail,
                    'success': item.success === true ? messageForSuccess : messageForUnSuccess,
                    'insertedDate': item.insertedDate
                }
            });
        }
    };
    var columns = [
        { data: "ip" },
        { data: "hostname" },
        { data: "macAddress" },
        { data: "applicationUserName" },
        { data: "osName" },
        { data: "browserDetail" },
        { data: "success" },
        {
            data: "insertedDate", type: "datetime", render: function (data) {

                return moment(data).format('L LT');
            }
        }
    ];
    var columnsDef = null;
    setDataTableOptions($("#table_loginAuditLog"), ajax, columns, columnsDef);
};

var startDate = $("#startDate");
var finishDate = $("#finishDate");

$('#excelExport').on("click", function (e) {
    var dataToPost = {
        "StartDate": startDate.val() === '' ? moment(new Date()).format('YYYY-MM-DD') : startDate.val(),
        "EndDate": finishDate.val() === '' ? moment(new Date()).format('YYYY-MM-DD') : finishDate.val()
    };

    var url = "/login-audit-logs/export-excel";
    Common.Ajax("POST", url, dataToPost, "json", function (data) {
        Common.ByteArrayToFileDownload(data);
    });
});

$('#pdfExport').on("click", function (e) {
    var dataToPost = {
        "StartDate": startDate.val() === '' ? moment(new Date()).format('YYYY-MM-DD') : startDate.val(),
        "EndDate": finishDate.val() === '' ? moment(new Date()).format('YYYY-MM-DD') : finishDate.val()
    };

    Common.Ajax("POST", "/login-audit-logs/pdf-export", dataToPost, null,
        function (response) {
            if (response && response.fileByteArray) {
                Common.ByteArrayToFileDownload(response);
                Common.DisplaySuccess({ message: L("DownloadStarted"), type: 'Info' });
            } else {
                showNotification(L("DefaultError"), "Error");
            }
        });
});

$('#filterApply').click(function () {
    dateFilter('table_loginAuditLog', startDate.val(), finishDate.val());
});

$('#filterReset').click(function () {
    startDate.val('');
    finishDate.val('');
    dateFilter('table_loginAuditLog', startDate.val(), finishDate.val());
});

jQuery(document).ready(function () {
    moment.locale(currentCulture)
    init();

    $('.datetimepicker-input').datetimepicker({
        locale: currentCulture,
        format: 'L'
    });
});