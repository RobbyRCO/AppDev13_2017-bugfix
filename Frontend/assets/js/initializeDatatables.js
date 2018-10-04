/**
 * Created by Spape on 28/04/2017.
 */
function initializeDatatable($tableName){
    var table;

    table = $('#' + $tableName).DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]
    });

    $('body').on('click', '#' + $tableName + ' tbody tr', function () {
        var data = table.row(this).data();
        setHiddenField('id_opdracht', data[0]);
        submitForm('id-form');
    });
    return table;
}
