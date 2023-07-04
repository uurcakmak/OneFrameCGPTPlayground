"use strict";

var init = function () {
    var ajax = {
        url: "/languages/list",
        type: "Get",
        dataSrc: function (gridObj) {
            return gridObj.data.map(function (item, index) {
                return {
                    'id': item.id,
                    'name': item.name,
                    'code': item.code,
                    'image': item.image,
                    'isDefault': item.isDefault === true ? yes : no,
                    'isActive': item.isActive === true ? yes : no,
                    'updatedDate': item.updatedDate
                }
            });
        }
    };
    var columns = [
        {
            data: "name", render: function (a, t, e, n) {
                return '<div class="d-flex align-items-center px-3">'
                    + '<div class="symbol symbol-20px me-5">'
                    + '<img src = ' + e.image + ' alt = ' + e.name + ' />'
                    + '</div>'
                    + '<div class="d-flex flex-column"><div class="d-flex align-items-center fs-5">' + e.name + '</div></div>'
                    + '</div>'
            }
        },
        { data: "code" },
        {
            data: "isDefault", render: function (a, t, e, n) {
                if (e.isDefault === yes) {
                    return '<span class="badge badge-success">' + e.isDefault + "</span";
                } else {
                    return '<span class="badge badge-light">' + e.isDefault + "</span";
                }
            }
        },
        {
            data: "isActive", render: function (a, t, e, n) {
                if (e.isActive === yes) {
                    return '<span class="badge badge-success">' + e.isActive + "</span";
                } else {
                    return '<span class="badge badge-light">' + e.isActive + "</span";
                }
            }
        },
        {
            data: "updatedDate", type: "datetime", render: function (data) {
                if (data != null) {
                    return moment(data).format('L LT');
                }
                return null;
            }
        },
        {data:null, responsivePriority: -1}
    ];
    var columnsDef = [
        {
            targets: -1,
            "width": "100px",
            title: L("Actions"),
            orderable: false,
            render: function (a, t, e, n) {
                var editButton = '<a href="javascript:LanguageView(' + " '" + e.id + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                                 + L("View") + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n';

                var deleteButton = '<a href="javascript:LanguageDelete(' + "'" + e.id + "'" + ');" class="btn text-danger btn-icon" title="'
                                    + L("Delete") + '">\n'
                                    + '<em class="far fa-trash-alt text-danger"></em>\n</a>';

                var buttons = "";
                buttons += editButton;

                if (e.isDefault === no) {
                    buttons += deleteButton;
                }

                return buttons;
            }
        }
    ];
    setDataTableOptions($("#table_language"), ajax, columns, columnsDef);
};

jQuery(document).ready(function () {
    moment.locale(currentCulture)
    init();
});


function LanguageDelete(id) {
    var path = "/languages?id=" + id;
    var tableId = "table_language";
    Swal2Delete(path, tableId);
}


function LanguageView(id) {
    Common.Ajax("GET", "languages/" + id, null, "text", function (response) {
        var divLanguage = $("#editLanguage");
        divLanguage.html(response);
        divLanguage.find("#modalEditLanguage").modal("show");
    });
}
