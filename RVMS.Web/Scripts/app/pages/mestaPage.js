RVMS.Pages.Mesta = function () {
    $(document).ready(function () {
        var opstineCombo = $("#opstineCombo"),
            mestaGrid = $("#mesta"),
            nazivMesta = $("#nazivMesta"),
            btnSacuvaj = $("#btnSave"),
            selektovanaOpstina,
            loader = $("#loader");

        loader.hide();

        // Opstine dropdown
        var opstineDataAdapter = new $.jqx.dataAdapter({
            url: '/Opstine/VratiAktivneOpstine',
            datatype: 'json',
            id: 'Id',
            height: RVMS.ControlHeight,
            datafields: [{ name: 'Id', type: 'int' }, {name: 'NazivOpstine'}]
        });
        opstineCombo.jqxDropDownList({
            source: opstineDataAdapter,
            width: 180,
            height: RVMS.ControlHeight,
            theme: RVMS.getTheme(),
            displayMember: 'NazivOpstine',
            valueMember: 'Id',
            placeHolder: 'Izaberite opštinu...'
        });
        opstineCombo.on('change', function (e) { var value = e.args.item.value;
            selektovanaOpstina = value;
            var data = new $.jqx.dataAdapter({
                url: '/Mesta/VratiMesta',
                id: 'Id',
                datatype: 'json',
                datafields: [{ name: 'Id', type: 'int' }, { name: 'Naziv' }, { name: 'Aktivan', type: 'bool' },
                             {name: 'OpstinaId', type: 'int'}],
                data: { idOpstine: selektovanaOpstina },
                updaterow: function(rowid, val, commit) {
                    $.ajax({
                        url: '/Mesta/Update',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=UTF-8',
                        data: JSON.stringify(val),
                        success: function() {
                            commit(true);
                            RVMS.Common.showDataSaved();
                        },
                        error: function(err) {
                            commit(false);
                            RVMS.Common.showGenericError();
                        }
                    });
                }
            });
            
            mestaGrid.jqxGrid({ source: data });
        });

        // Naziv mesta input
        nazivMesta.jqxInput({ theme: RVMS.getTheme(), width: 180, height: RVMS.ControlHeight });
        
        // Sačuvaj
        btnSacuvaj.jqxButton({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        btnSacuvaj.on('click', function() {
            if (!selektovanaOpstina) {
                RVMS.Common.showWarning("Izaberite opštinu");
                return false;
            }
            var nazivMestaValue = nazivMesta.val();
            if (!nazivMestaValue) {
                RVMS.Common.showWarning("Unesite naziv mesta");
                return false;
            }
            loader.show();
            btnSacuvaj.jqxButton({ disabled: true });
            $.ajax({
                url: '/Mesta/Insert',
                contentType: 'application/json; charset=UTF-8',
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify({ OpstinaId: selektovanaOpstina, Naziv: nazivMestaValue }),
                success: function () {
                    loader.hide();
                    btnSacuvaj.jqxButton({ disabled: false });
                    nazivMesta.val('');
                    RVMS.Common.showDataSaved();
                    mestaGrid.jqxGrid('updatebounddata');
                },
                error: function (err) {
                    loader.hide();
                    btnSacuvaj.jqxButton({ disabled: false });
                    RVMS.Common.showGenericError();
                }
            });
        });
        
        // Mesta grid
        mestaGrid.jqxGrid({
            columns: [
                { text: 'ID', datafield: 'Id', editable: false },
                { text: 'Naziv mesta', datafield: 'Naziv', editable: true, validation: function(cell, value) {
                    if (!value) {
                        return { result: false, message: 'Naziv mesta je obavezan' };
                    }
                    return true;
                    }
                },
                { text: 'Aktivno', datafield: 'Aktivan', editable: true, columntype: 'checkbox' }
            ],
            editable: true,
            sortable: true,
            theme: RVMS.getTheme()
        });
    });
};