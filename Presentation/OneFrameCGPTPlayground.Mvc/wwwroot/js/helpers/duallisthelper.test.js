var duallisthelper = require('./duallisthelper.js');

var data = [];

beforeEach(() => {
    duallisthelper.dualListControl(data);
});

beforeAll(() => {
    data = [{ selected: true, value: "Item1" }, { selected: false, value: "Item2" }];
});

test("dualListControlGetItems returns items", () => {
    expect(duallisthelper.dualListControlGetItems()).toStrictEqual({"SelectedItems": [data[0].value], "UnselectedItems": [data[1].value]});
});

test("dualAddItem is successful", () => {
    expect(() => duallisthelper.dualAddItem("Item2")).not.toThrow();
    expect(duallisthelper.dualListControlGetItems()).toStrictEqual({"SelectedItems": [data[0].value, "Item2"],"UnselectedItems": []});
});

test("dualRemoveItem is successful when unselectedItems.indexOf(item) < 0", () => {
    expect(() => duallisthelper.dualRemoveItem("Item1")).not.toThrow();
    expect(duallisthelper.dualListControlGetItems()).toStrictEqual({"SelectedItems": [], "UnselectedItems": [data[1].value, "Item1"]});
});
