var selectedItems;
var unselectedItems;

var dualAddItem = function (item) {
    if (unselectedItems.indexOf(item) > -1) {
        unselectedItems.splice(unselectedItems.indexOf(item), 1);
    }
    if (selectedItems.indexOf(item) < 0) {
        selectedItems.push(item);
    }
}

var dualRemoveItem = function (item) {
    if (selectedItems.indexOf(item) > -1) {
        selectedItems.splice(selectedItems.indexOf(item), 1);
    }

    if (unselectedItems.indexOf(item) < 0) {
        unselectedItems.push(item);
    }
}

var newDualListBox = function (name, optionsData) {
    dualListControl(optionsData);
    var dualListbox = new DualListbox(name,
        {
            addEvent: dualAddItem,
            removeEvent: dualRemoveItem,
            availableTitle: L("Unselected"),
            selectedTitle: L("Selected"),
            addButtonText: ">",
            removeButtonText: "<",
            addAllButtonText: ">>",
            removeAllButtonText: "<<",
            options: optionsData,
        });
    dualListbox.addEventListener("added",
        function (event) {
            var value = event.addedElement.getAttribute("data-id");
            dualAddItem(value);
        });
    dualListbox.addEventListener("removed",
        function (event) {
            var value = event.removedElement.getAttribute("data-id");
            dualRemoveItem(value);
        });
}


function dualListControl(data) {
    selectedItems = [];
    unselectedItems = [];
    data.map(function (item) {
        if (item.selected) {
            selectedItems.push(item.value);
        } else {
            unselectedItems.push(item.value);
        }
    });
}


function dualListControlGetItems() {
    return {
        SelectedItems: selectedItems,
        UnselectedItems: unselectedItems
    };
}

if (typeof module !== 'undefined') {
    module.exports = {
        dualListControlGetItems,
        dualListControl,
        dualAddItem,
        dualRemoveItem,
        newDualListBox
    };
}
