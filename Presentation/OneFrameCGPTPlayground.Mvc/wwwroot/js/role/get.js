"use strict";
var init = function () {
        var ajax = {
            url: "/roles/list",
            type: "Get",
            dataSrc: function (gridObj) {
                return gridObj.data.map(function (item, index) {
                    return {
                        'name': item.name,
                        'displayText': item.displayText,
                        'description': item.description
                    }
                });
            }
        };
        var columns = [
            { data: "name" },
            { data: "displayText", orderable: false },
            { data: "description", orderable: false },
            { data: null, responsivePriority: -1 }
        ];
        var columnsDef = [
            {
                className: "text-center",
                targets: -1,
                "width": "100px",
                title: L("Actions"),
                orderable: false,
                render: function (a, t, e, n) {
                    return '<a href="javascript:RoleView(' + " '" + e.name + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                        + L("View")
                        + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n'
                        + '<a href="javascript:RoleDelete(' + "'" + e.name + "'" + ');" class="btn text-danger btn-icon" title="'
                        + L("Delete") + '">\n<em class="far fa-trash-alt text-danger"></em>\n</a>';
                }
            }
        ];
        setDataTableOptions($("#table_role"), ajax, columns, columnsDef);
    };

jQuery(document).ready(function () {
    init();
});

function RoleDelete(name) {
    var path = "/roles?name=" + name;
    Swal2Delete(path, "table_role");
}

function RoleView(name) {
    Common.Ajax("GET", "roles/" + name, null, "text", function (response) {
        var divEditRole = $("#editRole");
        divEditRole.html(response);
        divEditRole.find("#modalEditRole").modal("show");
    });
}
