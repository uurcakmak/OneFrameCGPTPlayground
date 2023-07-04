"use strict"

var tree = $("#tree");

var KTMenuOrderGeneral = function () {
    var menuOrderSaveButton;
    var $menuOrderSaveButton = $(menuOrderSaveButton)

    var initTree = function () {
        var url = "/menus/tree";
        Common.Ajax("GET",
            url,
            null,
            null,
            function (result) {
                if (Array.isArray(result)) {
                    tree.jstree(true).settings.core.data = result;
                    tree.jstree(true).refresh(false, true);
                }
                Common.DisplaySuccess(result);
            });
    }

    var generateChildMenuItem = function (parentId, children) {
        var result = [];

        for (var i = 0; i < children.length; i++) {
            var child = children[i];

            var item = {
                id: parseInt(child.id),
                parentId: parseInt(parentId),
                orderId: i,
            };

            result.push(item);

            if (child.children.length > 0) {
                var childrenResult = generateChildMenuItem(child.id, child.children);
                result = $.merge(result, childrenResult);
            }
        }

        return result;
    }

    var generateMenuOrderTree = function (jsonList) {
        var result = [];

        for (var i = 0; i < jsonList.length; i++) {
            var item = jsonList[i];

            result.push({
                id: parseInt(item.id),
                parentId: null,
                orderId: i,
            });

            if (item.children.length > 0) {
                var childrenResult = generateChildMenuItem(item.id, item.children);
                result = $.merge(result, childrenResult);
            }
        }

        return result;
    }

    var handleOrderMenu = function (e) {
        menuOrderSaveButton.addEventListener('click', function (e) {
            e.preventDefault();

            ConfirmBox(function () {
                $menuOrderSaveButton.addClass("disabled").attr("disabled", true);
                $menuOrderSaveButton.attr('data-kt-indicator', 'on');

                var menuList = generateMenuOrderTree($("#tree").jstree(true).get_json());
                var model = { menuList: menuList };
                Common.Ajax("POST", "/menus/order", model);
            });
        });
    }

    return {
        init: function () {
            menuOrderSaveButton = document.querySelector('#menuOrderSaveButton');

            initTree();
            handleOrderMenu();
        }
    }
}();
setMenuTree(true, "la la-circle-o text-warning");

KTUtil.onDOMContentLoaded(function () {
    KTMenuOrderGeneral.init();
});