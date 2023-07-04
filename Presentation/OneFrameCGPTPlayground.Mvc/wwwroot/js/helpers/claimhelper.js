var setTree = function () {
    tree.jstree({
        "plugins": ["wholerow", "checkbox", "types"],
        'core': {
            'check_callback': true,
            'data': [
            ],
            'themes': {
                responsive: true
            },
        },
        'checkbox': {
            three_state: true,
            cascade_to_disabled: false,
        },
        "types": {
            "default": {
                "icon": "la la-shield text-warning"
            },
            "file": {
                "icon": "la la-shield text-warning"
            }
        },
    });
}

var listChange = function (list, path) {
    list.change(function () {
        var selectedValue = $(this).val();
        if (selectedValue === null || selectedValue === "" || selectedValue === undefined) {
            tree.jstree(true).settings.core.data = "[]";
            tree.jstree(true).refresh(false, true);

            return;
        }

        var url = path + selectedValue;
        Common.Ajax("GET",
            url,
            null,
            null,
            function (result) {
                tree.jstree(true).settings.core.data = result;
                tree.jstree(true).refresh(false, true);
                Common.DisplaySuccess(result);
            });
    });
}
