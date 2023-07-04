var setMenuTree = function (isDragDrop = false, icon = "la la-shield text-warning") {
    tree.jstree({
        "plugins": isDragDrop ? ["dnd", "state", "types"] : ["wholerow", "checkbox", "types"],
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
                "icon": icon
            },
            "file": {
                "icon": icon
            }
        },
    });
}