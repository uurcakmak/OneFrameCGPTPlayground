function readJsonFile(file, callback) {
    Common.Ajax("GET", file, null, "json", function (data) {
        callback(data);
    }, null, false);
}

var resourceDataList = [];

function L(resourceKey) {
    if (resourceDataList.length === 0) {
        readJsonFile("../resources/localizations.json",
            function (list) {
                for (var key in list) {
                    if (list.hasOwnProperty(key)) {
                        resourceDataList[list[key].Key] = list[key].Values[window.currentCulture];
                    }
                }

                resourceDataList.push("");
            });
    }
    return resourceDataList[resourceKey];
}