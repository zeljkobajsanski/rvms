RVMS.Pages.RelacijePage = function() {
    var self = this,
        nazivRelacije,
        btnSave,
        daljinar,
        polaznaStanica,
        dolaznaStanica,
        rastojanje,
        vreme,
        btnDodaj,
        idRelacije,
        grid,
        mapa,
        duzinaRelacije,
        vremeVoznje,
        brzina,
        btnPretrazi1,
        btnPretrazi2,
        pretragaStajalista,
        info = new google.maps.InfoWindow(),
        stajalisteZaPozicioniranje,
        izabranoStajalisteId,
        markers = [],
        btnRefresh;

    $(document).ready(function () {
        
        nazivRelacije = $("#nazivRelacije");
        nazivRelacije.jqxInput({ theme: RVMS.getTheme(), placeHolder: 'Naziv relacije...', width: 340, height: RVMS.ControlHeight });
        btnSave = $("#btnSave");
        btnSave.jqxButton({ theme: RVMS.getTheme() });
        btnSave.on('click', _insertRelaciju);
        
        daljinar = $("#daljinar");

        polaznaStanica = $("#polaznaStanica");
        polaznaStanica.jqxDropDownList({
            theme: RVMS.getTheme(),
            width: 240,
            height: RVMS.ControlHeight,
            placeHolder: 'Polazna stanica...',
            valueMember: 'Id',
            displayMember: 'Naziv'
        });
        
        dolaznaStanica = $("#dolaznaStanica");
        dolaznaStanica.jqxDropDownList({
            theme: RVMS.getTheme(),
            width: 240,
            height: RVMS.ControlHeight,
            placeHolder: 'Dolazna stanica...',
            valueMember: 'Id',
            displayMember: 'Naziv'
        });
        
        _osveziStajalista();

        rastojanje = $("#rastojanje");
        rastojanje.jqxNumberInput({
            theme: RVMS.getTheme(),
            width: 50,
            height: RVMS.ControlHeight,
            inputMode: 'simple',
            textAlign: 'right',
            decimalDigits: 2,
            decimalSeparator: '.'
        });
        
        vreme = $("#vreme");
        vreme.jqxNumberInput({
            theme: RVMS.getTheme(),
            width: 50,
            height: RVMS.ControlHeight,
            inputMode: 'simple',
            textAlign: 'right',
            decimalDigits: 0,
        });

        btnDodaj = $("#btnDodaj");
        btnDodaj.jqxButton({
            theme: RVMS.getTheme(),
            width: 52
        });
        btnDodaj.on('click', function() {
            var psItem = polaznaStanica.jqxDropDownList('getSelectedItem');
            var dsItem = dolaznaStanica.jqxDropDownList('getSelectedItem');
            var dsIndex = dolaznaStanica.jqxDropDownList('getSelectedIndex');
            var r = rastojanje.val();
            var v = vreme.val();

            if (!psItem || !dsItem) return;
            if (psItem.value == dsItem.value) return;

            $.ajax({
                url: '/Relacije/InsertMedjustanicnoRastojanje',
                type: 'POST',
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify({
                    RelacijaId: idRelacije,
                    PolaznoStajalisteId: psItem.value,
                    DolaznoStajalisteId: dsItem.value,
                    Rastojanje: r,
                    VremeVoznje: v
                }),
                success: function() {
                    _osveziGrid();
                    dolaznaStanica.jqxDropDownList('clearSelection');
                    rastojanje.val(0);
                    vreme.val(0);
                    polaznaStanica.jqxDropDownList('selectIndex', dsIndex);
                    dolaznaStanica.jqxDropDownList('focus');
                    
                },
                error: function(err) {
                    
                }
            });

        });
        daljinar.hide();

        grid = $("#grid");
        grid.jqxGrid({
           columns: [
               {text: 'Polazno stajalište', datafield: 'PolaznoStajaliste', editable: false, cellsrenderer: _stajalisteCellRenderer },
               { text: 'Dolazno stajalište', datafield: 'DolaznoStajaliste', editable: false, cellsrenderer: _stajalisteCellRenderer },
               { text: 'Rastojanje', datafield: 'Rastojanje', align: 'right', cellsalign: 'right', cellsformat: 'd2', editable: true, columntype: 'numberinput', width: 62 },
               { text: 'Dužina relacije', datafield: 'DuzinaRelacije', align: 'right', cellsalign: 'right', cellsformat: 'd2', editable: false, width: 84 },
               { text: 'Vreme vožnje', datafield: 'VremeVoznje', align: 'right', cellsalign: 'right', editable: true, columntype: 'numberinput', width: 80 },
               { text: 'Ukupno vreme', datafield: 'VremeVoznjePoRelaciji', align: 'right', cellsalign: 'right', editable: false, width: 86 },
               { text: '#', datafield: 'Id', cellsrenderer: _obrisiCellTemplate, width: 20, editable: false }
           ],
           editable: true,
           theme: RVMS.getTheme(),
           columnsheight: 32
        });
        grid.on('cellclick', function(event) {
            if (event.args.column.datafield == "Id") {
                if (confirm('Da li želite da obrišete stavku?')) {
                    _obrisi(event.args.value);
                }
            }
        }).on('rowselect', function(e) {
            var data = grid.jqxGrid('getrowdata', e.args.rowindex);
            polaznaStanica.val(data.DolaznoStajalisteId);
        });
        
        var relacijaJson = $("#Relacija").val();
        var relacija = JSON.parse(relacijaJson);
        if (relacija) {
            nazivRelacije.val(relacija.Naziv);
            idRelacije = relacija.Id;
            daljinar.show();
            _initMap();
            _osveziGrid();
        }
        duzinaRelacije = $("#duzinaRelacije");
        vremeVoznje = $("#vremeVoznje");
        brzina = $("#brzina");

        pretragaStajalista = $("#pretragaStajalista");
        
        btnPretrazi1 = $("#btnPretrazi1").jqxButton({ theme: RVMS.getTheme() });
        btnPretrazi1.on('click', function() {
            RVMS.Dialogs.PretragaStajalista.OpenDialog();
            pretragaStajalista.off();
            pretragaStajalista.on('close', function() {
                if (RVMS.Dialogs.PretragaStajalista.IzabranoStajaliste) {
                    polaznaStanica.val(RVMS.Dialogs.PretragaStajalista.IzabranoStajaliste.Id);
                }
            });
        });
        btnPretrazi2 = $("#btnPretrazi2").jqxButton({ theme: RVMS.getTheme() });
        btnPretrazi2.on('click', function () {
            RVMS.Dialogs.PretragaStajalista.OpenDialog();
            pretragaStajalista.off();
            pretragaStajalista.on('close', function () {
                if (RVMS.Dialogs.PretragaStajalista.IzabranoStajaliste) {
                    dolaznaStanica.val(RVMS.Dialogs.PretragaStajalista.IzabranoStajaliste.Id);
                }
            });
            rastojanje.find('input').focus();
        });
        btnRefresh = $(".btnRefresh");
        btnRefresh.jqxButton({ theme: RVMS.getTheme() });
        btnRefresh.on('click', _osveziStajalista);
        
        stajalisteZaPozicioniranje = $("#stajalisteZaPozicioniranje");
    });
    
    self.izaberiStajaliste = function (id, naziv) {
        stajalisteZaPozicioniranje.text(naziv);
        izabranoStajalisteId = id;
    };

    function _insertRelaciju() {
        var naziv = nazivRelacije.val();
        if (naziv) {
            $.ajax({
                url: '/Relacije/SacuvajRelaciju',
                type: 'POST',
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify({ Id: idRelacije, naziv: naziv }),
                success: function (id) {
                    idRelacije = id;
                    daljinar.show();
                    _initMap();
                },
                error: function(err) {
                    alert(err);
                }
            });
        }
    }
    
    function _osveziStajalista() {
        var staniceDataSource = new $.jqx.dataAdapter({
            url: '/Stajalista/VratiAktivnaStajalista',
            datatype: 'json',
            async: true,
            id: 'Id',
        }, {
            //downloadComplete: function() {
            //    staniceDataSource.dataBind();
            //    var records = staniceDataSource.getGroupedRecords(['Opstina'], 'stajalista', 'grupa', [{ name: 'Opstina', map: 'grupa' }]);
            //    var source = [];
            //    $.each(records, function (i, item) {
            //        $.each(item.stajalista, function (i1, stajaliste) {
            //            source.push({
            //                html: "<div style='padding: 0 0 0 2px'>" + stajaliste.Naziv + "</div>",
            //                group: item.grupa,
            //                label: stajaliste.Naziv,
            //                Naziv: stajaliste.Naziv,
            //                Id: stajaliste.Id
            //            });
            //        });
            //    });
            //    polaznaStanica.jqxDropDownList({ source: source });
            //    dolaznaStanica.jqxDropDownList({ source: source });
            }
        );
        polaznaStanica.jqxDropDownList({ source: staniceDataSource });
        dolaznaStanica.jqxDropDownList({ source: staniceDataSource });
    }
    
    function _osveziGrid() {
        var dataSource = new $.jqx.dataAdapter({
            url: "/Relacije/MedjustanicnaRastojanja",
            data: { idRelacije: idRelacije },
            datatype: 'json',
            updaterow: function(rid, value, commit) {
                var data = {
                    Id: value.Id,
                    Rastojanje: value.Rastojanje,
                    VremeVoznje: value.VremeVoznje
                };
                $.ajax({
                    url: '/Relacije/UpdateMedjustanicnoRastojanje',
                    type: 'POST',
                    contentType: 'application/json; charset=UTF-8',
                    data: JSON.stringify(data),
                    success: function() {
                        commit(true);
                        _osveziGrid();
                    },
                    error: function (err) { commit(false); }
                });
            },
        },
        {
            loadComplete: function(data) {
                var len = data.length,
                          bounds,
                          directionsRequest = { travelMode: google.maps.TravelMode.DRIVING },
                          waypoints = [];
                markers = [];
                for (var i = 0; i < len; i++) {
                    var stajaliste = data[i];

                    if (i == 0) {
                        var polazak = new google.maps.LatLng(stajaliste.LatitudaPolaznogStajalista, stajaliste.LongitudaPolaznogStajalista);
                        directionsRequest.origin = polazak;
                        bounds = new google.maps.LatLngBounds(polazak),
                                 new google.maps.LatLng(stajaliste.LatitudaPolaznogStajalista, stajaliste.LongitudaPolaznogStajalista);
                    }
                    if (stajaliste.LatitudaPolaznogStajalista && stajaliste.LongitudaPolaznogStajalista) {
                        var marker = new google.maps.Marker({ map: mapa, position: new google.maps.LatLng(stajaliste.LatitudaPolaznogStajalista, stajaliste.LongitudaPolaznogStajalista), draggable: true });
                        marker.idStajalista = stajaliste.PolaznoStajalisteId;
                        google.maps.event.addListener(marker, 'dragend', function () {
                            _azurirajPoziciju(marker);
                        });
                        markers.push(marker);
                        _dodeliInfo(marker, stajaliste);
                    }
                    if (stajaliste.LatitudaDolaznogStajalista && stajaliste.LongitudaDolaznogStajalista) {
                        var waypoint = new google.maps.LatLng(stajaliste.LatitudaDolaznogStajalista, stajaliste.LongitudaDolaznogStajalista);
                        directionsRequest.destination = waypoint;
                        if (i < len - 1) {
                            waypoints.push({ location: waypoint });
                        } else {
                            polaznaStanica.val(stajaliste.DolaznoStajalisteId);
                        }
                        marker = new google.maps.Marker({ map: mapa, position: waypoint, draggable: true });
                        marker.idStajalista = stajaliste.DolaznoStajalisteId;
                        google.maps.event.addListener(marker, 'dragend', function () {
                            _azurirajPoziciju(marker);
                        });
                        markers.push(marker);
                        _dodeliInfo(marker, stajaliste);
                        bounds.extend(new google.maps.LatLng(stajaliste.LatitudaDolaznogStajalista, stajaliste.LongitudaDolaznogStajalista));
                    }
                }
                var directionsService = new google.maps.DirectionsService();
                directionsService.route(directionsRequest, function(response, status) {
                    if (status == google.maps.DirectionsStatus.OK) {
                        var display = new google.maps.DirectionsRenderer();
                        display.setMap(mapa);
                        display.setDirections(response);
                    }
                });
                mapa.fitBounds(bounds);
                if (len > 0) {
                    var poslednji = data[len - 1];
                    duzinaRelacije.text(poslednji.DuzinaRelacije + " km");
                    vremeVoznje.text(poslednji.VremeVoznjePoRelaciji + " min");
                    var srednjaSaobracajnaBrzina = poslednji.DuzinaRelacije / (poslednji.VremeVoznjePoRelaciji / 60);
                    brzina.text((Math.round(srednjaSaobracajnaBrzina * 10) / 10).toFixed(2) + " km/h");
                }
            }
        });
        grid.jqxGrid({ source: dataSource });
    }
    
    function _initMap() {
        var mapOptions = {
            center: new google.maps.LatLng(RVMS.Latituda, RVMS.Longituda),
            zoom: RVMS.ZoomLevel,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        mapa = new google.maps.Map(document.getElementById("mapa"), mapOptions);
        google.maps.event.addListener(mapa, 'click', function(e) {
            if (izabranoStajalisteId) {
                if (!_postojiMarker(izabranoStajalisteId)) {
                    var marker = new google.maps.Marker({ map: mapa, position: e.latLng, draggable: true });
                    marker.idStajalista = izabranoStajalisteId;
                    markers.push(marker);
                    _azurirajPoziciju(marker);
                    google.maps.event.addListener(marker, 'dragend', function() {
                        _azurirajPoziciju(marker);
                    });
                }
            }
        });
    }
    
    function _postojiMarker(idStajalista) {
        var postoji = false;
        markers.forEach(function (item) {
            if (item.idStajalista === idStajalista) {
                postoji = true;
                return;
            }
        });
        return postoji;
    }
    
    function _azurirajPoziciju(marker) {
        $.ajax({
            url: '/Stajalista/AzurirajKoordinatu',
            type: 'POST',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ Id: marker.idStajalista, GpsLatituda: marker.getPosition().lat(), GpsLongituda: marker.getPosition().lng() }),
            success: function() {
                RVMS.Common.showSuccess("Pozicija stajališta je ažurirana");
            },
            error: function() {
                RVMS.Common.showWarning("Neuspešno ažuriranje pozicije stajališta");
            }
        });
    }

    function _obrisiCellTemplate(row, column, value) {
        var wrapper = $("<div>");
        var img = $("<input type='image' src='/Content/images/gnome_edit_delete.png' style='padding: 4px 0 0 4px' />");
        wrapper.append(img);
        return wrapper.html();
    }
    
    function _obrisi(id) {
        $.ajax({
            url: '/Relacije/ObrisiMedjustanicnoRastojanje',
            type: 'POST',
            data: { id: id },
            success: function () { _osveziGrid(); },
            error: function () {  }
        });
    }
    
    function _dodeliInfo(marker, stajaliste) {
        google.maps.event.addListener(marker, 'click', function () {
            var content = "<div class='infoWindow'>" +
                "<h1>" + stajaliste.PolaznoStajaliste + "</h1>" +
                "<p>" + stajaliste.PolaznoStajaliste + " - " + stajaliste.DolaznoStajaliste + "</p>" +
                "<p>Rastojanje: " + stajaliste.Rastojanje + " km</p>" +
                "<p>Vreme vožnje: " + stajaliste.VremeVoznje + " min</p>" +
                "</div>";
            info.setContent(content);
            info.open(mapa, marker);
        });
    }
    
    
    
    function _stajalisteCellRenderer(row, columnfield, value) {
        var lnk = $("<a class='stajalisteLink' href='javascript:;'></a>");
        var data = grid.jqxGrid('getrowdata', row);
        var id = null,
            naziv = null;
        
        if (columnfield == 'PolaznoStajaliste') {
            id = data.PolaznoStajalisteId;
            naziv = data.PolaznoStajaliste;
            if (data.LatitudaPolaznogStajalista && data.LongitudaPolaznogStajalista) {
                lnk.addClass('globe');
            }
        } else if (columnfield == 'DolaznoStajaliste') {
            id = data.DolaznoStajalisteId;
            naziv = data.DolaznoStajaliste;
            if (data.LatitudaDolaznogStajalista && data.LongitudaDolaznogStajalista) {
                lnk.addClass('globe');
            }
        }

        var callback = 'RVMS.Pages.RelacijePage.izaberiStajaliste(' + id + ',' +  '"' + naziv + '")';
        lnk.attr('onclick', callback);
        lnk.text(value);
        var link = $("<div>").append(lnk).html();
        return link;
    }
};