let map = null;
var layerGroup = null;

$(document).ready(function () {
    map = L.map('newMap').setView([44.824894, 20.450627], 6.0);
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=sk.eyJ1IjoiZGFuaWplbGdsaXNvdmljIiwiYSI6ImNrcmt6YXgzODE3YnIyb3BlOWRwNW5nMWYifQ.rEAIMDQbEa1ui2bA3sCUNg', {
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'your.mapbox.access.token'
    }).addTo(map);
    layerGroup = L.layerGroup().addTo(map);
    map.on('click', onMapClick);

    $('.newFestivalNavLink').click(function () {
        var thisId = $(this).prop('id');
        var otherId = 'detailsPill';
        if (thisId == 'detailsPill') {
            otherId = 'locationPill';
            $('#festivalLocationContainer').hide();
            $('#festivalDetailsContainer').show();
        }
        else {
            $('#festivalDetailsContainer').hide();
            $('#festivalLocationContainer').show();
            map.invalidateSize(true);
        }

        $('#' + otherId).removeClass('active');
        $('#' + thisId).addClass('active');
    });

    $('#FestivalLocation_Latitude').change(function () {
        var newLatVal = $(this).val();
        var lngVal = $('#FestivalLocation_Longitude').val();
        if (lngVal == '' || lngVal == undefined || lngVal == null) {
            return;
        }

        layerGroup.clearLayers();
        L.marker([newLatVal, lngVal]).addTo(layerGroup);
    });

    $('#FestivalLocation_Longitude').change(function () {
        var newLngVal = $(this).val();
        var latVal = $('#FestivalLocation_Latitude').val();
        if (latVal == '' || latVal == undefined || latVal == null) {
            return;
        }

        layerGroup.clearLayers();
        L.marker([latVal, newLngVal]).addTo(layerGroup);
    });

    $('#newFestForm').submit(function () {

        var dateFrom = Date.parse($('#StartDate').val());
        var dateTo = Date.parse($('#EndDate').val());

        if (dateFrom > dateTo) {
            setModal('Start date can not be greater than end date.');
            return false;
        }

        //isNullOrUndefOrEmpty
        if (isNullOrUndefOrEmpty($('#Name').val()) || isNullOrUndefOrEmpty($('#StartDate').val()) || isNullOrUndefOrEmpty($('#EndDate').val()) || isNullOrUndefOrEmpty($('#UploadedImages').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Latitude').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Longitude').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Address').val())) {
            setModal('You did not provide us with some neccessary data for new festival. Have in mind that there are two sections for creating new festival, the "Festival details" and "Location details" sections.');
            return false;
        }

        return true;
    });
});

function onMapClick(e) {
    layerGroup.clearLayers();
    $('#FestivalLocation_Latitude').val(e.latlng.lat);
    $('#FestivalLocation_Longitude').val(e.latlng.lng);

    L.marker([e.latlng.lat, e.latlng.lng]).addTo(layerGroup);
}

function setModal(message) {

    $('#errorMessageModal').text(message);

    // Get the modal
    var modal = document.getElementById("newFestModal");

    var btnOk = document.getElementById("btnOk");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    btnOk.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}

function isNullOrUndefOrEmpty(prop) {
    if (prop == '' || prop == null || prop == undefined) {
        return true;
    }

    return false;
}