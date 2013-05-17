RVMS.Dialogs.PretragaStajalista = function() {
    var self = this,
               dialogWindow,
               psOpstina,
               psMesto,
               psStajaliste,
               btnPretraziStajalista,
               btnOcisti,
               rezultatiGrid;

    $(function () {
        dialogWindow = $("#pretragaStajalista");
        psOpstina = $("#psOpstine");
        psMesto = $("#psMesto");
        psStajaliste = $("#psStajaliste");
        btnPretraziStajalista = $("#btnPretraziStajalista");
        btnOcisti = $("#btnOcisti");
        rezultatiGrid = $("#rezultatiGrid");

        dialogWindow.jqxWindow({
            theme: RVMS.getTheme(),
            width: 600,
            height: 400,
            isModal: true,
            autoOpen: false,
            title: 'Pretraga stajališta'
        });

        var opstine = new $.jqx.dataAdapter({
            url: '/Opstine/VratiAktivneOpstine',
            datatype: 'json',
        });

        psOpstina = psOpstina.jqxDropDownList({
            theme: RVMS.getTheme(),
            width: 240,
            height: RVMS.ControlHeight,
            valueMember: 'Id',
            displayMember: 'NazivOpstine',
            source: opstine,
            placeHolder: 'Filtriraj po opštini'
        });
        
        var mesta = new $.jqx.dataAdapter({
            url: '/Mesta/VratiSvaAktivnaMesta',
            datatype: 'json',
        });

        psMesto = psMesto.jqxDropDownList({
            theme: RVMS.getTheme(),
            width: 240,
            height: RVMS.ControlHeight,
            valueMember: 'Id',
            displayMember: 'Naziv',
            placeHolder: 'Filtriraj po mestu',
            source: mesta
        });

        psStajaliste = psStajaliste.jqxInput({ theme: RVMS.getTheme(), width: 240, height: RVMS.ControlHeight });

        btnPretraziStajalista.jqxButton({ theme: RVMS.getTheme() });
        btnOcisti.jqxButton({ theme: RVMS.getTheme() });

        rezultatiGrid.jqxGrid({
            theme: RVMS.getTheme(),
            width: 588,
            sortable: true,
            height: 244,
            columns: [
                { text: 'Stajalište', datafield: 'Naziv' },
                { text: 'Mesto', datafield: 'Mesto' },
                { text: 'Opština', datafield: 'Opstina' },
            ]
        });

        // events
        //psOpstina.on('change', self.OsveziMesta);

        btnOcisti.on('click', self.Ocisti);
        
        rezultatiGrid.on('rowdoubleclick', function(event) {
            var row = event.args.rowindex;
            self.IzabranoStajaliste = rezultatiGrid.jqxGrid('getrowdata', row);
            dialogWindow.jqxWindow('close');
        });
    });

    self.Pretrazi = function () {
        var opstina = psOpstina.jqxDropDownList('getSelectedItem');
        var mesto = psMesto.jqxDropDownList('getSelectedItem');
        var data = new $.jqx.dataAdapter({
            url: '/Stajalista/PretraziStajalista',
            datatype: 'json',
            data: {
                idOpstine: opstina ? opstina.value : '',
                idMesta: mesto ? mesto.value : '',
                nazivStajalista: psStajaliste.val()
            }
        });
        rezultatiGrid.jqxGrid({ source: data });

    };

    self.OsveziMesta = function() {
        var izabranaOpstina = psOpstina.jqxDropDownList('getSelectedItem');
        var mesta = new $.jqx.dataAdapter({
            url: '/Mesta/VratiAktivnaMesta',
            datatype: 'json',
            data: { idOpstine: izabranaOpstina ? izabranaOpstina.value : undefined },
        });
        psMesto.jqxDropDownList({ source: mesta });
    };

    self.Ocisti = function() {
        psOpstina.jqxDropDownList('clearSelection');
        psMesto.jqxDropDownList('clearSelection');
        psStajaliste.val('');
        rezultatiGrid.jqxGrid({ source: [] });
    };

    self.OpenDialog = function() {
        dialogWindow.jqxWindow('open');
        self.IzabranoStajaliste = null;
        //self.Ocisti();
        psStajaliste.val('');
        psStajaliste.focus();
    };
};

RVMS.Dialogs.PretragaStajalista = new RVMS.Dialogs.PretragaStajalista();