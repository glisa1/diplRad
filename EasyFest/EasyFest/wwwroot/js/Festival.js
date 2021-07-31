$(document).ready(function () {
    var mapArea = $('#map');
    if (mapArea == undefined) {
        return;
    }

    var festName = $('#FestivalName').val();
    var longitude = $('#Longitude').val();
    var latitude = $('#Latitude').val();

    var mymap = L.map('map').setView([latitude, longitude], 14.95);
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=sk.eyJ1IjoiZGFuaWplbGdsaXNvdmljIiwiYSI6ImNrcmt6YXgzODE3YnIyb3BlOWRwNW5nMWYifQ.rEAIMDQbEa1ui2bA3sCUNg', {
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'your.mapbox.access.token'
    }).addTo(mymap);

    var marker = L.marker([latitude, longitude]).addTo(mymap);
    marker.bindPopup("<b>" + festName + "</b>").openPopup();

    //setModal();

    // RADIAL PROGRESS BAR
    var festRate = $('#FestivalRate').val();
    var max = -219.99078369140625;
    $.each($('.progress'), function (index, value) {
        percent = festRate * 20;
        $(value).children($('.fill')).attr('style', 'stroke-dashoffset: ' + ((100 - percent) / 100) * max);
        $(value).children($('.value')).text(festRate + '/5.00');
    });
});

$('#addNewComment').click(function () {
    var commentText = $('#newCommentText').val();
    if (commentText == undefined || commentText == '') {
        $('#newCommentValidation').show();
        return;
    }

    $('#newCommentValidation').hide();
    alert('Treba pozvati mutaciju sa tekstom:' + commentText);
});

//function setModal() {
//    // Get the modal
//    var modal = document.getElementById("myModal");

//    // Get the button that opens the modal
//    var btn = document.getElementById("myBtn");

//    // Get the <span> element that closes the modal
//    var span = document.getElementsByClassName("close")[0];

//    // When the user clicks on the button, open the modal
//    btn.onclick = function () {
//        modal.style.display = "block";
//    }

//    // When the user clicks on <span> (x), close the modal
//    span.onclick = function () {
//        emptyModalValues();
//        modal.style.display = "none";
//    }

//    // When the user clicks anywhere outside of the modal, close it
//    window.onclick = function (event) {
//        if (event.target == modal) {
//            emptyModalValues();
//            modal.style.display = "none";
//        }
//    }
//}

//function emptyModalValues() {
//    $('#newCommentValidation').hide();
//    $('#newCommentText').val('');
//}