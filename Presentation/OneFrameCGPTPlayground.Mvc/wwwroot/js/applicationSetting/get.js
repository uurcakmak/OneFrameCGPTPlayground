"use strict";

    var init = function () {
        var ajax = {
            url: "/application-settings/list",
            type: "Get",
            dataSrc: function (gridObj) {
                return gridObj.data.map(function (item, index) {
                    return {
                        'id': item.id,
                        'key': item.key,
                        'value': item.value,
                        'valueType': item.valueType,
                        'categoryName': item.categoryName,
                        'isStatic': item.isStatic,
                        'status': item.isStatic ? "Static" : "Not Static",
                    }
                });
            }
        };
        var gridColumns = [
            { data: "key" },
            { data: "value" },
            { data: "valueType" },
            { data: "categoryName", orderable: false },
            { data: "status", orderable: false },
            { data: null, responsivePriority: -1 }
        ];
        var gridColumnsDef = [
            {
                className: "text-center",
                targets: -1,
                "width": "100px",
                title: L("Actions"),
                orderable: false,
                render: function (a, t, e, n) {
                    if (e.isStatic) {
                        return '<a href="javascript:AppSettingView(' + " '" + e.id + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                            + L("View")
                            + '">\n<em class="far fa-edit text-primary"></em>\n</a>';
                    }
                    else {
                        return '<a  href="javascript:AppSettingView(' + " '" + e.id + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                            + L("View") + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n'
                            + '<a href="javascript:AppSettingDelete(' + "'" + e.id + "'" + ');" class="btn text-danger btn-icon" title="'
                            + L("Delete") + '">\n'
                            + '<em class="far fa-trash-alt text-danger"></em>\n</a>';
                    }
                }
            }
        ];
        setDataTableOptions($("#table_appSetting"), ajax, gridColumns, gridColumnsDef);
    };

jQuery(document).ready(function () {
    init();
});

function AppSettingDelete(id) {
    var path = "/application-settings?id=" + id;
    var tableId = "table_appSetting";
    Swal2Delete(path, tableId);
}

function AppSettingView(id) {
    Common.Ajax("GET", "application-settings/" + id, null, "text", function (response) {
        var divAppSetting = $("#editApplicationSetting");
        divAppSetting.html(response);
        divAppSetting.find("#modalEditApplicationSetting").modal("show");
    });
}
