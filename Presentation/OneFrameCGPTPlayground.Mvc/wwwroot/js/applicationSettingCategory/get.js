"use strict";
var init = function () {
    var ajax = {
        url: "/application-setting-categories/list",
        type: "Get",
        dataSrc: function (gridObj) {
            return gridObj.data.map(function (item, index) {
                return {
                    'id': item.id,
                    'name': item.name,
                    'description': item.description,
                };
            });
        },
    };

    var columns = [
        { data: "name" },
        { data: "description", orderable: false },
        { data: null, responsivePriority: -1 }
    ];

    var columnsDef = [
        {
            className: "text-center",
            targets: -1,
            "width": "100px",
            title: "",
            orderable: false,
            render: function (a, t, e, n) {
                return '<a href="javascript:AppSettingCategoryView(' + " '" + e.id + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                    + L("View")
                    + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n'
                    + '<a href="javascript:AppSettingCategoryDelete(' + "'" + e.id + "'" + ');" class="btn text-danger btn-icon" title="'
                    + L("Delete")
                    + '">\n<em class="far fa-trash-alt text-danger"></em>\n</a>';
            }
        }
    ];

    setDataTableOptions($("#table_appSettingCategory"), ajax, columns, columnsDef);
};

jQuery(document).ready(function () {
    init();
});

function AppSettingCategoryDelete(id) {
    var path = "/application-setting-categories?id=" + id;
    var tableId = "table_appSettingCategory";
    Swal2Delete(path, tableId);
}

function AppSettingCategoryView(id) {
    Common.Ajax("GET", "application-setting-categories/" + id, null, "text", function (response) {
        var divEditApplicationSettingCategory = $("#editApplicationSettingCategory");
        divEditApplicationSettingCategory.html(response);
        divEditApplicationSettingCategory.find("#modalEditApplicationSettingCategory").modal("show");
    });
}
