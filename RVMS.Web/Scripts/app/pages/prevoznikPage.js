RVMS.Pages.PrevoznikPage = function() {

    var nazivPrevoznika,
        adresaPrevoznika,
        mestoPrevoznika,
        btnSave,
        model;

    $(document).ready(function() {

        model = JSON.parse($("#Model").val());

        nazivPrevoznika = $("#nazivPrevoznika");
        nazivPrevoznika.jqxInput({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        
        adresaPrevoznika = $("#adresaPrevoznika");
        adresaPrevoznika.jqxInput({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        
        mestoPrevoznika = $("#mestoPrevoznika");
        mestoPrevoznika.jqxInput({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });

        btnSave = $("#btnSave");
        btnSave.jqxButton({ theme: RVMS.getTheme(), height: RVMS.ControlHeight });
        btnSave.on('click', _sacuvaj);

        if (model.Id > 0) {
            nazivPrevoznika.val(model.Naziv);
            adresaPrevoznika.val(model.Adresa);
            mestoPrevoznika.val(model.Mesto);
        }

    });
    
    function _sacuvaj() {
        if (nazivPrevoznika.val() && adresaPrevoznika.val() && mestoPrevoznika.val()) {
            var prevoznk = {
                Id: model.Id,
                Naziv: nazivPrevoznika.val(),
                Adresa: adresaPrevoznika.val(),
                Mesto: mestoPrevoznika.val()
            };
            $.ajax({
                url: '/Prevoznici/Sacuvaj',
                type: 'POST',
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(prevoznk),
                success: function(response) {
                    model = response;
                    RVMS.Common.showDataSaved();
                },
                error: function(err) {
                    RVMS.Common.showGenericError();
                }
            });
        } else {
            RVMS.Common.showWarning("Popunite podatke prevoznika");
        }
    }

};