﻿var mymap = null;

$(document).ready(function () {

    var numOfLocations = $('#FesLocNum').val();

    /*var */mymap = L.map('allMap').setView([44.1301508, 22.5104071], 4.0);
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
        //var popupHtml = '<div><a href="/Festival/FestivalDetails?festivalId=' + festId + '">' + festName + '</a>';
        //popupHtml += '<br><span>Rated: ' + festRate + '</span></div>';
        //var markercss =
        //{
        //    'width': '300px',
        //    'height': '200px'
        //}
        marker.bindPopup(createPopup(festId, festName, festRate, i), { className: 'allMapPopup'});
    }
});

function createPopup(Id, name, rate, index) {
    var popupHtml = '<div id="carouselExampleControls" class="carousel slide" data-ride="carousel"><div class="carousel-inner" style="max-height:135px">';

    var imageCount = $('#fest_' + index + '_imgCount').val();
    for (var j = 0; j < imageCount; j++) {
        var image = $('#fest_' + index + '_img_' + j).val();
        if (j === 0) {
            popupHtml += '<div class="carousel-item active">';
            popupHtml += '<img class="d-block w-100" src="/Home/Image?imageName=' + image + '" alt="...">';
            popupHtml += '</div>';
        }
        else {
            popupHtml += '<div class="carousel-item">';
            popupHtml += '<img class="d-block w-100" src="/Home/Image?imageName=' + image + '" alt="...">';
            popupHtml += '</div>';
        }
    }

    popupHtml += '</div>';
    if (imageCount > 1) {
        popupHtml += '<a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">';
        popupHtml += '<span class="carousel-control-prev-icon" aria-hidden="true"></span>';
        popupHtml += '<span class="sr-only">Previous</span>';
        popupHtml += '</a>';
        popupHtml += '<a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">';
        popupHtml += '<span class="carousel-control-next-icon" aria-hidden="true"></span>';
        popupHtml += '<span class="sr-only">Next</span>';
        popupHtml += '</a>';
    }
    popupHtml += '</div>';

    popupHtml += '<div><a href="/Festival/FestivalDetails?festivalId=' + Id + '">' + name + '</a>';
    popupHtml += '<br><span>Rated: ' + rate + '</span></div>';

    return popupHtml;
}