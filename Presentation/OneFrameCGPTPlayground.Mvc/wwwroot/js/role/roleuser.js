var table_usersinrole = $("#table_usersinrole");
var UsersInRoleDatatablesDataSourceAjaxClient = {
    init: function () {
        $.fn.dataTable.ext.order["dom-checkbox"] = function (settings, col) {
            return this.api().column(col, { order: "index" }).nodes().map(function (td, i) {
                return $("input", td).prop("checked") ? "1" : "0";
            });
        };
        table_usersinrole.on("click",
            'input[type="checkbox"]',
            function () {
                var tr = $(this).closest("tr");
                var row = table_usersinrole.DataTable().rows(tr);
                var rowData = row.data()[0];
                rowData.isInRoleUI = $(this).prop("checked");
            });
    }
};

jQuery(document).ready(function () {
    UsersInRoleDatatablesDataSourceAjaxClient.init();
});
