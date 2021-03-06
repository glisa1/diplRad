let map = null;
var layerGroup = null;
let selectedTags = [];

$(document).ready(function () {
    map = L.map('newMap').setView([44.824894, 20.450627], 6.0);
    //L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=sk.eyJ1IjoiZGFuaWplbGdsaXNvdmljIiwiYSI6ImNrcmt6YXgzODE3YnIyb3BlOWRwNW5nMWYifQ.rEAIMDQbEa1ui2bA3sCUNg', {
    //    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    //    maxZoom: 18,
    //    id: 'mapbox/streets-v11',
    //    tileSize: 512,
    //    zoomOffset: -1,
    //    accessToken: 'your.mapbox.access.token'
    //}).addTo(map);

    L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Tiles &copy; Esri &mdash; Source: Esri, DeLorme, NAVTEQ, USGS, Intermap, iPC, NRCAN, Esri Japan, METI, Esri China (Hong Kong), Esri (Thailand), TomTom, 2012'
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
        var form = $(this);

        if (dateFrom > dateTo) {
            setModal('Start date can not be greater than end date.');
            return false;
        }

        //isNullOrUndefOrEmpty
        if (isNullOrUndefOrEmpty($('#Name').val()) || isNullOrUndefOrEmpty($('#StartDate').val()) || isNullOrUndefOrEmpty($('#EndDate').val()) || isNullOrUndefOrEmpty($('#UploadedImages').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Latitude').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Longitude').val()) || isNullOrUndefOrEmpty($('#FestivalLocation_Address').val())) {
            setModal('You did not provide us with some neccessary data for new festival. Have in mind that there are two sections for creating new festival, the "Festival details" and "Location details" sections.');
            return false;
        }

        //form.append('<input type="hidden" id="selectedTag" name="model.SelectedTags" value="System.Collections.Generic.List`1[System.String]">');
        selectedTags.forEach(function (val, i) {
            form.append('<input type="hidden" id="selectedTag_' + i + '" name="SelectedTags" value="' + val + '">');//[' + i + '] 
        });

        return true;
    });

    $('.tag-btn').click(function () {
        if ($(this).hasClass('selected-tag')) {

            const index = selectedTags.indexOf($(this).val());
            if (index > -1) {
                selectedTags.splice(index, 1);
            }

            $('svg', this).remove();

            $(this).removeClass('selected-tag');
        }
        else {
            selectedTags.push($(this).val());

            let newElem = '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check" viewBox="0 0 16 16">';
            newElem += '<path d="M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z" />';
            newElem += '</svg>';

            $(this).append(newElem);

            $(this).addClass('selected-tag');
        }
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