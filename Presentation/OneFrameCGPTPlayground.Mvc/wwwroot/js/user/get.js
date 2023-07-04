"use strict";

var init = function () {
    var ajax = {
        url: "/users/list",
        type: "Get",
        dataSrc: function (gridObj) {
            return gridObj.data.map(function (item, index) {
                return {
                    'username': item.username
                }
            });
        }
    };
    var columns = [
        { data: "username" },
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
                return '<a href="javascript:UserView(' + " '" + e.username + "' " + ');" class="btn kt-label-font-color-2 btn-icon" title="'
                    + L("View") + '">\n<em class="far fa-edit text-primary"></em>\n</a>\n'
                    + '<a href="javascript:UserRole(' + " '" + e.username + "' " + ');" class="btn text-danger btn-icon" title="'
                    + L("UserRole") + '"><em class="fas fa-people-arrows text-warning"></em></a>\n'
                    + '<a href="javascript:UserDelete(' + " '" + e.username + "' " + ');" class="btn text-danger btn-icon" title="'
                    + L("Delete") + '">\n<em class="far fa-trash-alt text-danger"></em>\n</a>';
            }
        }
    ];

    setDataTableOptions($("#table_user"), ajax, columns, columnsDef);
};

jQuery(document).ready(function () {
    init();
});

function UserDelete(username) {
    var path = "/users?username=" + username;
    Swal2Delete(path, "table_user");
}

function UserView(username) {
    Common.Ajax("GET", "users/" + username, null, "text", function (response) {
        var divEditUser = $("#editUser");
        divEditUser.html(response);
        divEditUser.find("#modalEditUser").modal("show");
    });
}

function UserRole(username) {
    Common.Ajax("GET", "users/role-assignments/" + username, null, "text", function (response) {
        var divuserRole = $("#userRole");
        divuserRole.html(response);
        divuserRole.find("#modalUserRole").modal("show");
    });
}
