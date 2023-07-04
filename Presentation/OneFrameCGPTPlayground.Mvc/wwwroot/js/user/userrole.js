"use strict";

var KTUserRoleGeneral = function () {
    var userRoleForm;
    var userRoleSubmitButton;

    var handleUserRoleForm = function (e) {
        userRoleSubmitButton.addEventListener('click', function (e) {
            e.preventDefault();
            var formUserRole = $(userRoleForm);
            var items = dualListControlGetItems();
            var formData = formUserRole.data();
            var userRoleData = { AssignedRoles: items.SelectedItems, UnassignedRoles: items.UnselectedItems, Username: formData.userName };
            Common.Ajax(formUserRole.attr("method"), formUserRole.attr("action"), userRoleData);
        });
    };

    return {
        init: function () {
            userRoleForm = document.querySelector('#userRoleForm');
            userRoleSubmitButton = document.querySelector('#userRoleButton');

            handleUserRoleForm();
            dualListInit();
        }
    };
    function dualListInit() {
        var formOptions = $('#userRoleForm').data();
        newDualListBox('#kt_dual_listbox_2', formOptions.options);
    }
}();

KTUtil.onDOMContentLoaded(function () {
    KTUserRoleGeneral.init();
});
