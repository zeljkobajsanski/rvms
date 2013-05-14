RVMS.Pages.StajalistaPage = function() {
    var opstine,
        mesta,
        nazivStajalista,
        btnSave,
        btnCopy,
        stajalista,
        mapa,
        marker,
        izabranoStajaliste,
        stanica;


    $(document).ready(function() {
        opstine = $("#opstineCombo");
        mesta = $("#mestaCombo");
        nazivStajalista = $("#nazivStajalista");
        stanica = $("#stanica");

        var opstineDataAdapter = new $.jqx.dataAdapter({
            url: '/Opstine/VratiAktivneOpstine',
            datatype: 'json',
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'NazivOpstine' }
            ]
        });

        opstine.jqxDropDownList({
            source: opstineDataAdapter,
            width: 180,
            height: RVMS.ControlHeight,
            theme: RVMS.getTheme(),
            displayMember: 'NazivOpstine',
            valueMember: 'Id',
            placeHolder: 'Izaberite opštinu...'
        });
        opstine.on('change', function() {
            _osveziMesta();
            _osveziStajalista();
        });

        mesta.jqxDropDownList({
            width: 180,
            height: RVMS.ControlHeight,
            theme: RVMS.getTheme(),
            displayMember: 'Naziv',
            valueMember: 'Id',
            placeHolder: 'Izaberite mesto...'
        });
        mesta.on('change', function () {
            _osveziStajalista();
        });

        btnCopy = $("#btnCopy");
        btnCopy.jqxButton({ theme: RVMS.getTheme() });
        btnCopy.on('click', function() {
            var selektovanoMesto = mesta.jqxDropDownList('getSelectedItem');
            if (selektovanoMesto) {
                nazivStajalista.val(selektovanoMesto.label);
                nazivStajalista.focus();
            }
        });

        nazivStajalista.jqxInput({
            width: 180,
            height: RVMS.ControlHeight,
            theme: RVMS.getTheme()
        });

        stanica = $("#stanica");
        stanica.jqxCheckBox({ theme: RVMS.getTheme() });

        btnSave = $("#btnSave");
        btnSave.jqxButton({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        btnSave.on('click', _sacuvaj);

        stajalista = $("#stajalista");
        stajalista.jqxGrid({
            theme: RVMS.getTheme(),
            editable: true,
            columns: [
                { text: 'ID', datafield: 'Id', editable: false },
                {
                    text: 'Naziv stajališta', datafield: 'Naziv', editable: true, validation: function (cell, value) {
                        if (!value) {
                            return { result: false, message: 'Naziv stajališta je obavezan' };
                        }
                        return true;
                    }
                },
                { text: 'Aktivno', datafield: 'Aktivan', editable: true, columntype: 'checkbox' },
                { text: 'Stanica', datafield: 'Stanica', editable: true, columntype: 'checkbox' }
            ],
            width: 594,
            sortable: true
        });
        stajalista.on('rowselect', _izabranoStajaliste);

        _initMap();
    });
    
    function _osveziMesta() {
        var opstina = _izabranaOpstina();
        if (opstina) {
            var dataSource = new $.jqx.dataAdapter({
                url: '/Mesta/VratiAktivnaMesta',
                datatype: 'json',
                data: {idOpstine: opstina},
                datafields: [
                    { name: 'Id', type: 'int' },
                    { name: 'Naziv' }
                ]
            });
            mesta.jqxDropDownList({
                source: dataSource
            });
        }
    }
    
    function _izabranaOpstina() {
        var item = opstine.jqxDropDownList('getSelectedItem');
        if (item) {
            return item.value;
        }
        return null;
    }
    
    function _izabranoMesto() {
        var item = mesta.jqxDropDownList('getSelectedItem');
        if (item) {
            return item.value;
        }
        return null;
    }
    
    function _sacuvaj() {
        var opstina = _izabranaOpstina();
        if (!opstina) return;
        var naziv = nazivStajalista.val();
        if (!naziv) return;
        var mesto = _izabranoMesto();
        $.ajax({
            url: '/Stajalista/Insert',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ OpstinaId: opstina, MestoId: mesto, Naziv: naziv, Stanica: stanica.jqxCheckBox('checked') }),
            success: function () {
                RVMS.Common.showDataSaved();
                nazivStajalista.val('');
                stanica.jqxCheckBox('uncheck');
                _osveziStajalista();
            },
            error: function(err) {
                alert(err);
            }
        });
    }
    
    function _osveziStajalista() {
        var dataSource = new $.jqx.dataAdapter({
            url: '/Stajalista/VratiStajalista',
            datatype: 'json',
            id: 'Id',
            data: {idOpstine: _izabranaOpstina(), idMesta: _izabranoMesto()},
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'Naziv' },
                { name: 'Aktivan', type: 'bool' },
                { name: 'MestoId', type: 'int' },
                { name: 'OpstinaId', type: 'int' },
                { name: 'GpsLatituda' },
                { name: 'GpsLongituda' },
                { name: 'Stanica', type: 'bool'}
            ],
            updaterow: function(rid, value, commit) {
                _update(value, commit);
            }
        });

        stajalista.jqxGrid({ source: dataSource });
        stajalista.jqxGrid('clearselection');
    }
    
    function _initMap() {
        var mapOptions = {
            center: new google.maps.LatLng(RVMS.Latituda, RVMS.Longituda),
            zoom: RVMS.ZoomLevel,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        mapa = new google.maps.Map(document.getElementById("mapa"),
            mapOptions);
        google.maps.event.addListener(mapa, 'click', function (e) {
            if (!izabranoStajaliste) return;
            
            izabranoStajaliste.GpsLatituda = e.latLng.lat();
            izabranoStajaliste.GpsLongituda = e.latLng.lng();
            _update(izabranoStajaliste);
            if (!marker) {
                _createMarker();
            }
            marker.setPosition(new google.maps.LatLng(izabranoStajaliste.GpsLatituda, izabranoStajaliste.GpsLongituda));
        });
    }
    
    function _izabranoStajaliste(e) {
        var row = e.args.rowindex;
        izabranoStajaliste = stajalista.jqxGrid('getrowdata', row);
        if (!marker) _createMarker();
        else {
            if (izabranoStajaliste.GpsLatituda && izabranoStajaliste.GpsLongituda) {
                var location = new google.maps.LatLng(izabranoStajaliste.GpsLatituda, izabranoStajaliste.GpsLongituda);
                marker.setPosition(location);
                mapa.setCenter(location);
            } else {
                marker.setMap(null);
                marker = undefined;
            }
        }
    }
    
    function _update(stajaliste, commit) {
        $.ajax({
            url: '/Stajalista/Update',
            contentType: 'application/json; charset=UTF-8',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(stajaliste),
            success: function () {
                if (commit) {
                    commit(true);
                    RVMS.Common.showDataSaved();
                }
            },
            error: function () {
                if (commit) commit(false);
            }
        });
    }

    function _createMarker() {
        marker = new google.maps.Marker({
            map: mapa,
            draggable: true,
        });
        if (izabranoStajaliste) {
            if (izabranoStajaliste.GpsLatituda && izabranoStajaliste.GpsLongituda) {
                var location = new google.maps.LatLng(izabranoStajaliste.GpsLatituda, izabranoStajaliste.GpsLongituda);
                marker.setPosition(location);
                mapa.setCenter(location);
            }
            google.maps.event.addListener(marker, 'dragend', function(event) {
                izabranoStajaliste.GpsLatituda = event.latLng.lat();
                izabranoStajaliste.GpsLongituda = event.latLng.lng();
                _update(izabranoStajaliste);
            });
        }
    }
};