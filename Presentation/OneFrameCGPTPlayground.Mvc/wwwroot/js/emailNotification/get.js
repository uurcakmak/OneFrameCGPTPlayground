"use strict";
var emailNotification = [];
var init = function () {
    var ajax = {
        url: "/email-notifications/list",
        type: "Get",
        dataSrc: function (gridObj) {
            emailNotification = gridObj.data;
            return gridObj.data.map(function (item, index) {
                return {
                    'id': item.id,
                    'subject': item.subject,
                    'from': item.from,
                    'to': item.to,
                    'insertedDate': item.insertedDate,
                    'message': item.isSent === true ? messageForSuccess : messageForUnSuccess,
                    'isSent': item.isSent,
                    'sentDate': item.sentDate,
                    'body': item.body,
                }
            });
        }
    };
    var columns = [
        { data: "subject" },
        { data: "from" },
        { data: "to" },
        {
            data: "insertedDate", type: "datetime", render: function (data) {
                return moment(data).format('L LT');
            }
        },
        { data: "message" },
        {
            data: "sentDate", type: "datetime", render: function (data) {
                return moment(data).format('L LT');
            }
        },
        { data: null, responsivePriority: -1 }
    ];

    var columnsDef = [
        {
            className: "text-center",
            targets: -1,
            "width": "150px",
            title: L("Actions"),
            orderable: false,
            render: function (a, t, e, n) {
                return '<a href="javascript:viewDetail(' + "'" + e.id + "'" + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                    + L("View")
                    + '">\n<em class="far fa-file-alt text-primary"></em>\n</a>\n'
                    + '<a href="javascript:sendMail(' + "'" + e.id + "'" + ');" class="btn btn-icon" title="'
                    + L("Send") + '">\n' + '<em class="far fa-paper-plane text-warning"></em>\n</a>';
            }
        }
    ];

    setDataTableOptions($("#table_emailNotification"), ajax, columns, columnsDef);
};

jQuery(document).ready(function () {
    moment.locale(currentCulture)
    init();
});

function viewDetail(id) {
    $('#body').html("");
    var data = emailNotification.filter(x => x.id === id)[0];
    var body = data != null ? data.body : "";
    $('#body').append(body);
    $('#detailModal').modal('show');
}

function sendMail(id) {
    ConfirmBox(function () {
        let url = "/email-notifications/send?id=" + id;
        Common.Ajax("GET", url, null);
    });
}
