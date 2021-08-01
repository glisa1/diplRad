$(document).ready(function () {

    var numOfLocations = $('#FesLocNum').val();

    var mymap = L.map('allMap').setView([44.1301508, 22.5104071], 4.0);
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=sk.eyJ1IjoiZGFuaWplbGdsaXNvdmljIiwiYSI6ImNrcmt6YXgzODE3YnIyb3BlOWRwNW5nMWYifQ.rEAIMDQbEa1ui2bA3sCUNg', {
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'your.mapbox.access.token'
    }).addTo(mymap);

    for (var i = 0; i < numOfLocations; i++) {
        var latitude = $('#festLocLat' + i).val();
        var longitude = $('#festLocLon' + i).val();
        var festName = $('#festLocName' + i).val();
        var festRate = $('#festLocRate' + i).val();
        var festId = $('#festLocId' + i).val();
        var marker = L.marker([latitude, longitude]).addTo(mymap);
        var popupHtml = '<div><a href="/Festival/FestivalDetails?festivalId' + festId + '">' + festName + '</a>';
        popupHtml += '<span>Rated: ' + festRate + '</span></div>';
        marker.bindPopup(popupHtml);
    }
});