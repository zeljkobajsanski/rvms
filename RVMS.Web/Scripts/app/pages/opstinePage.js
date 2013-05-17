RVMS.Pages.OpstinePage = function() {
    var noviUnos,
        btnNew,
        loader;

    $(document).ready(function () {
        loader = $("#loader");
        noviUnos = $("#noviUnos");
        noviUnos.jqxInput({ theme: RVMS.getTheme(), width: 140, height: RVMS.ControlHeight });
        btnNew = $("#btnNew");

        loader.hide();

        btnNew.jqxButton({ theme: RVMS.getTheme() });
        btnNew.on('click', function() {
            data = {
                nazivOpstine: noviUnos.val()
            };
            if (!data.nazivOpstine) {
                RVMS.Common.showWarning("Unesite naziv opštine");
                return false;
            }
            
            loader.show();
            btnNew.jqxButton({ disabled: true });
            $.ajax({
                url: '/Opstine/Insert',
                contentType: 'application/json',
                type: 'POST',
                data: JSON.stringify(data),
                success: function () {
                    loader.hide();
                    btnNew.jqxButton({ disabled: false });
                    noviUnos.val('');
                    $("#grid").jqxGrid('updatebounddata');
                    RVMS.Common.showDataSaved();
                },
                error: function () {
                    loader.hide();
                    btnNew.jqxButton({ disabled: false });
                    RVMS.Common.showGenericError();
                }
            });
            return false;
        });
        
        var source = {
            url: '/Opstine/VratiOpstine',
            datatype: "json",
            datafields: [{ name: 'Id', type: 'int' }, { name: 'NazivOpstine' }, { name: 'Aktivan', type: 'bool' }],
            id: 'id'
        };
        var data = new $.jqx.dataAdapter(source, {
            updaterow: function (rowId, value, commit) {
                $.ajax({
                    url: '/Opstine/Update',
                    contentType: 'application/json; charset=UTF-8',
                    type: 'POST',
                    data: JSON.stringify(value),
                    success: function () {
                        noviUnos.val('');
                        RVMS.Common.showDataSaved();
                        $("#grid").jqxGrid('updatebounddata');
                    },
                    error: function () {
                        RVMS.Common.showGenericError();
                    }
                });
                commit(true);
            },
        });

        $("#grid").jqxGrid({
            theme: RVMS.getTheme(),
            source: data,
            editable: true,
            sortable: true,
            columns: [{ text: 'ID', datafield: 'Id', editable: false },
                      {
                          text: 'Naziv opštine', datafield: 'NazivOpstine', validation: function (cell, value) {
                              if (!value) {
                                  return {
                                      result: false,
                                      message: 'Morate uneti naziv opštine'
                                  };
                              }
                              return true;
                          }
                      }, { text: 'Aktivna', datafield: 'Aktivan', columntype: 'checkbox' }]
        });


    });
};