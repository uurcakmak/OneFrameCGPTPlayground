"use strict";

var init = function () {
    var ajax = {
        url: "/email-templates/list",
        type: "Get",
        dataSrc: function (gridObj) {
            return gridObj.data.map(function (item, index) {
                return {
                    'id': item.id,
                    'updatedDate': new Date(item.updatedDate).toLocaleString(),
                    'name': item.name,
                    'supportedLanguages': item.supportedLanguages,
                }
            });
        }
    };

    var columns = [
        { data: "name" },
        {
            data: "updatedDate", type: "datetime", render: function (data) {
                if (data != null) {
                    return moment(data).format('L LT');
                }
                return null;
            }
        },
        { data: "supportedLanguages", orderable: false },
        { data: null, responsivePriority: -1 }
    ]

    var columnsDef = [
        {
            className: "text-center",
            targets: -1,
            "width": "150px",
            title: L("Actions"),
            orderable: false,
            render: function (a, t, e, n) {
                return '<a href="/email-templates/' + e.id + '" class="btn kt-label-font-color-2 btn-icon" title="'
                    + L("View")
                    + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n';
            }
        }
    ];

    setDataTableOptions($("#table_emailTemplate"), ajax, columns, columnsDef);
};

jQuery(document).ready(function () {
    moment.locale(currentCulture)
    init();
});
