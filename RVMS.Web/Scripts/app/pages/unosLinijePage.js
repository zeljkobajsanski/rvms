RVMS.Pages.UnosLinijePage = function() {
    var self = this,
        stajalista,
        btnIzaberiStajaliste,
        mapa,
        mogucaStajalista = [],
        stajalistaLinije = [],
        linija,
        grid,
        duzinaLinije = 0,
        nazivLinije,
        btnSave,
        prevoznici;

    $(document).ready(function () {

        // Model
        linija = JSON.parse($("#Linija").val());
        
        
        // Naziv linije
        nazivLinije = $("#nazviLinije");
        nazivLinije.jqxInput({ theme: RVMS.getTheme(), height: RVMS.ControlHeight, width: 360 });
        nazivLinije.val(linija.Naziv);

        prevoznici = $("#prevoznici");
        var prevozniciDataSource = new $.jqx.dataAdapter({
            url: '/Prevoznici/VratiAktivnePrevoznike',
            datatype: 'json',
            datafields: [{name: 'Id', type: 'int'}, {name: 'Naziv'}]
        });
        prevoznici.jqxDropDownList({
            theme: RVMS.getTheme(),
            height: RVMS.ControlHeight,
            width: 180,
            source: prevozniciDataSource,
            displayMember: 'Naziv',
            valueMember: 'Id',
            placeHolder: 'Izaberite prevoznika...'
        });
        
        // Button Save
        btnSave = $("#btnSave");
        btnSave.jqxButton({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        btnSave.on('click', _sacuvajLiniju);

        // DropDownList stajalista
        stajalista = $("#stajalista");
        stajalista.jqxDropDownList({
            theme: RVMS.getTheme(),
            width: 176,
            height: RVMS.ControlHeight,
            placeHolder: 'Izaberite stalajište...',
            valueMember: 'id'
        });
        _ucitajStajalista();

        // btn
        btnIzaberiStajaliste = $("#btnIzaberiStajaliste");
        btnIzaberiStajaliste.jqxButton({
            theme: RVMS.getTheme(),
            width: 48,
            height: 24
        });
        btnIzaberiStajaliste.click(function() {
            var selected = stajalista.jqxDropDownList('getSelectedItem');
            if (selected) {
                self.izaberiStajaliste(selected.value);
            }
        });
        
        // Mapa
        _initMap();

        // Grid
        grid = $("#grid");
        grid.jqxGrid({
            theme: RVMS.getTheme(),
            columns: [
                { text: 'Stanica/Stajalište', datafield: 'Naziv' },
                { text: 'Rastojanje', datafield: 'Udaljenost', cellsformat: 'd2', cellsalign: 'right'},
                { text: '#', datafield: 'Id', cellsrenderer: RVMS.Common.deleteButton, width: 24 }
            ],
            width: 176,
            height: 500,
            autoheight: true,
        });
        grid.on('cellclick', _obrisi);

        if (linija.Id == 0) {
            $("#linijaWrapper").hide();
            nazivLinije.jqxInput('focus');
        } else {
            self.ucitajLiniju(linija.Id);
            $("#linijaWrapper").show();
        }
    });
    
    // Public API

    this.izaberiStajaliste = function (idStajalista) {
        $.ajax({
            url: '/Linije/IzaberiStajaliste',
            type: 'POST',
            data: { idLinije:linija.Id, id: idStajalista },
            success: function(stajalistaLinije) {
                _prikaziStajalista(stajalistaLinije);
                _ucitajStajalista();
            },
            error: function() {
                
            }
        });
        
    };
    
    this.obrisiMogucaStajalista = function () {
        $.each(mogucaStajalista, function (i, stajaliste) {
            stajaliste.setMap(null);
        });
        mogucaStajalista = [];
    };

    this.ucitajLiniju = function(idLinije) {
        $.ajax({
            url: '/Linije/UcitajLiniju',
            data: { id: idLinije },
            success: function(response) {
                _obrisiStajalistaLinije();
                _prikaziStajalista(response.Stajalista);
                linija = response;
                prevoznici.val(linija.PrevoznikId);
            },
            error: function(err) {
                RVMS.showGenericEror();
            }
        });
    };

    // Private methods

    function _initMap() {
        var mapOptions = {
            center: new google.maps.LatLng(RVMS.Latituda, RVMS.Longituda),
            zoom: RVMS.ZoomLevel,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        mapa = new google.maps.Map(document.getElementById("mapa"),
            mapOptions);
    }

    function _ucitajStajalista() {
        $.ajax({
            url: '/Linije/VratiSusednaStajalista',
            data: { idLinije: linija.Id },
            success: function(data) {
                var source = [];
                $.each(data, function(i, item) {
                    source.push({
                        html: '<div>' + item.Naziv + "</div>",
                        label: item.Naziv,
                        group: item.Opstina,
                        id: item.Id
                    });
                });
                stajalista.jqxDropDownList({ source: source });
                _prikaziMogucaStajalista(data);
            }
        });
    }

    
    
    function _prikaziMogucaStajalista(stajalista) {
        self.obrisiMogucaStajalista();
        var bounds;
        $.each(stajalista, function (i, stajaliste) {
            
            if (stajaliste.Latituda && stajaliste.Longituda) {
                var coord = new google.maps.LatLng(stajaliste.Latituda, stajaliste.Longituda);
                if (i == 0) {
                    bounds = new google.maps.LatLngBounds(coord, coord);
                } else {
                    bounds.extend(coord);
                }
                var marker = new google.maps.Marker({
                    map: mapa,
                    position: coord,
                    idStajalista: stajaliste.Id
                });
                mogucaStajalista.push(marker);
                google.maps.event.addListener(marker, 'click', function() {
                    self.izaberiStajaliste(marker.idStajalista);
                });
                google.maps.event.addListener(marker, 'mouseover', function () {
                    marker.setTitle(stajaliste.Naziv);
                });
            }
        });
        mapa.fitBounds(bounds);
    }
    
    function _obrisiStajalistaLinije() {
        $.each(stajalistaLinije, function (i, marker) {
            marker.setMap(null);
        });
        stajalistaLinije = [];
    }

    function _prikaziStajalista(stajalista) {
        var len = stajalista.length;
        duzinaLinije = 0;
        $.each(stajalista, function (i, stajaliste) {
            if (stajaliste.Latituda && stajaliste.Longituda) {
                var marker = new google.maps.Marker({
                    map: mapa,
                    position: new google.maps.LatLng(stajaliste.Latituda, stajaliste.Longituda),
                    icon: '/Content/images/home (1).png',
                    title: stajaliste.Naziv
                });
                stajalistaLinije.push(marker);
            }
            duzinaLinije += !isNaN(stajaliste.Udaljenost) ? stajaliste.Udaljenost : 0;
        });
        var dataSource = new $.jqx.dataAdapter({ tye: 'array', localdata: stajalista });
        grid.jqxGrid({ source: dataSource });
        duzinaLinije = (Math.round(duzinaLinije * 10) / 10).toFixed(2);
        $("#duzinaLinije").text(duzinaLinije + " km");

        if (len > 0) {
            var directionsService = new google.maps.DirectionsService();
            var request = {
                origin: new google.maps.LatLng(stajalista[0].Latituda, stajalista[0].Longituda),
                destination: new google.maps.LatLng(stajalista[len - 1].Latituda, stajalista[len - 1].Longituda),
                travelMode: google.maps.TravelMode.DRIVING,
                waypoints: []
            };
            $.each(stajalista, function(i, stajaliste) {
                request.waypoints.push({ location: new google.maps.LatLng(stajaliste.Latituda, stajaliste.Longituda) });
            });
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    var directionsDisplay = new google.maps.DirectionsRenderer();
                    directionsDisplay.setMap(mapa);
                    directionsDisplay.setDirections(response);
                }
            });
        }
    }
    
    function _obrisi(e) {
        if (e.args.column.datafield == 'Id') {
            if (confirm('Da li želite da obrišete stavku?')) {
                var row = grid.jqxGrid('getrowdata', e.args.rowid);
                $.ajax({
                    url: '/Linije/ObrisiStavku',
                    data: { id: row.IdStajalistaLinije },
                    success: function () {
                        self.ucitajLiniju(linija.Id);
                    }
                });
            }
        }
    }
    
    function _sacuvajLiniju() {
        var naziv = nazivLinije.val();
        var idPrevoznika = prevoznici.val();
        if (!naziv || !idPrevoznika) {
            RVMS.Common.showWarning("Popunite podatke linije");
            return;
        }
        var l = {
            Id: linija.Id,
            Naziv: naziv,
            PrevoznikId: prevoznici.val()
        };
        $.ajax({
            url: '/Linije/SacuvajLiniju',
            type: 'POST',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify(l),
            success: function (response) {
                linija = response;
                $("#linijaWrapper").show();
                RVMS.Common.showDataSaved();
            },
            error: function (err) {
                alert(err);
            }
        });
    }
};