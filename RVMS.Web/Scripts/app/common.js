Array.max = function (array) {
    return Math.max.apply(Math, array);
};

Array.min = function (array) {
    return Math.min.apply(Math, array);
};

RVMS.Common = {

    deleteButton: function () {
        return "<input type='image' src='/Content/images/gnome_edit_delete.png' style='padding: 4px 0 0 4px' />";
    },
    showInfo: function(message) {
        toastr.info(message, "RVMS - Info", {positionClass: 'toast-bottom-right'});
    },
    showWarning: function (message) {
        toastr.warning(message, "RVMS - Upozorenje", { positionClass: 'toast-bottom-right' });
    },
    showEror: function (message) {
        toastr.error(message, "RVMS - Greška", { positionClass: 'toast-bottom-right' });
    },
    showSuccess: function (message) {
        toastr.success(message, "RVMS - Uspešna operacija", { positionClass: 'toast-bottom-right' });
    },
    showDataSaved: function() {
        toastr.success("Podaci su uspešno sačuvani", "RVMS - Uspešna operacija", { positionClass: 'toast-bottom-right' });
    },
    showGenericError: function() {
        toastr.error("Željena operacija nije uspela", "RVMS - Greška", { positionClass: 'toast-bottom-right' });
    }
};