RVMS.Pages.PregledRelacijaPage = function () {
    var grid;


    // Initialization
    $(document).ready(function() {
        grid = $("#grid");
        grid.jqxGrid({
            theme: RVMS.getTheme(),
            columns: [
                //{ text: 'ID', datafield: 'Id' },
                { text: 'Naziv relacije', datafield: 'Naziv' },
                { text: 'Dužina relacije [km]', datafield: 'DuzinaRelacije', align: 'right', cellsalign: 'right', cellsformat: 'd1', width: 122 },
                { text: 'Vreme vožnje [min]', datafield: 'VremeVoznje', align: 'right', cellsalign: 'right', width: 122 },
                { text: 'Saobraćajna brzina [km/h]', datafield: 'SrednjaSaobracajnaBrzina', align: 'right', cellsalign: 'right', cellsformat: 'd2', width: 152 },
                { text: 'Izmeni', datafield: 'Id', cellsrenderer: _actionColumnRenderer, width: 48 },
                { text: "Briši", cellsrenderer: function () { return "<img src='/Content/images/gnome_edit_delete.png' alt='' style='cursor: pointer; padding: 4px' />"; }, width: 48 }
            ],
            width: 1000,
            height: 600,
            pageable: true,
            pagesize: 20,
            pagesizeoptions: ['10', '20', '50']
        });
        grid.on('cellclick', function(event) {
            if (event.args.column.text == "Briši") {
                _obrisi(event.args.rowindex);
            }
        });
        _osveziGrid();
    });
    
    function _osveziGrid() {
        var dataSource = new $.jqx.dataAdapter({
            url: '/Relacije/VratiRelacije',
            datatype: 'json',
            id: 'Id',
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'Naziv' },
                { name: 'DuzinaRelacije' },
                { name: 'VremeVoznje' },
                { name: 'SrednjaSaobracajnaBrzina' }
            ],
        });
        grid.jqxGrid({ source: dataSource });
    }

    function _obrisi(id) {
        var data = grid.jqxGrid('getrowdata', id);
        if (confirm('Da li želite da obrišete stavku?')) {
            $.ajax({
                url: '/Relacije/Obrisi',
                type: 'POST',
                data: { id: data.Id },
                success: function () {
                    _osveziGrid();
                },
                error: function () { }
            });
        }
    };

    function _actionColumnRenderer(row, column, value) {
        var s = "<a href='/Relacije/Izmeni/" + value + "'><img src='/Content/images/edit.png' alt='' style='margin: 4px 8px 0 8px;' /></a>";
        return s;
    }
};