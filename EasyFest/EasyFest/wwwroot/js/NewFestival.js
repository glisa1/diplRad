let map = null;

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
        }

        $('#' + otherId).removeClass('active');
        $('#' + thisId).addClass('active');
    });
});

function onMapClick(e) {
    alert("You clicked the map at " + e.latlng + " zoomed " + e.zoom);
    $('#FestivalLocation_Latitude').val(e.latlng.lat);
    $('#FestivalLocation_Longitude').val(e.latlng.lng);

    L.marker([e.latlng.lat, e.latlng.lng]).addTo(map);
}