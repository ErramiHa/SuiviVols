function getlistAeroport() {
    var selectAeroport = $('#SelectedArrivee');
    $.ajax({
        type: 'get',
        url: "/Home/GetlisteAeroport",
        data: { Code: $('#SelectedDepart').val() },
        dataType: 'json',
        success: function (data) {
            selectAeroport.empty();
            if (data.length === 0) {
                selectAeroport.append("<option value='-1'>Sélectionner un aéroport</option>");
            } else {
                selectAeroport.append("<option value='-1'>Sélectionner un aéroport</option>");
                $.each(data, function (element) {
                    if (element !== undefined && element !== null) {
                        selectAeroport.append("<option value='" + data[element].codeAeroport + "'>" + data[element].nomAeroport + "</option>");
                    }
                });
               
            }
        }
    });
}
