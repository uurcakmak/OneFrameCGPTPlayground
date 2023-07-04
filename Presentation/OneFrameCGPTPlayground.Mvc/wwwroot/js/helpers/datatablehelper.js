var setDataTableOptions = function (table, ajax, columns, columnDefs) {
    table.DataTable({
        responsive: true,
        searchDelay: 100,
        searching: true,
        processing: true,
        serverSide: true,
        ordering: true,
        pagingType: "full_numbers",
        language: {
            processing: L("DataTable_processing"),
            lengthMenu: "_MENU_",
            info: "_START_ - _END_ / _TOTAL_",
            infoEmpty: L("DataTable_infoEmpty"),
            infoFiltered: L("DataTable_infoFiltered"),
            infoPostFix: L("DataTable_infoPostFix"),
            loadingRecords: L("Loading"),
            zeroRecords: L("DataTable_zeroRecords"),
            emptyTable: L("DataTable_emptyTable"),
            aria: {
                sortAscending: L("DataTable_sortAscending"),
                sortDescending: L("DataTable_sortDescending")
            }
        },
        ajax: ajax,
        columns: columns,
        columnDefs: columnDefs
    });

    $('.datatable-search').on('keyup', function () {
        table.DataTable().search(this.value).draw();
    });
}

var dateFilter = function (tableId, startDate, finishDate) {
    var initDate = startDate !== "" ? startDate : moment(new Date("01/01/1900")).format('DD/MM/YYYY');
    var endDate = finishDate !== "" ? finishDate : moment(new Date("01/01/9999")).format('DD/MM/YYYY');
    $('#' + tableId).DataTable().search(`${initDate},${endDate} 23:59:00`).draw();
}

