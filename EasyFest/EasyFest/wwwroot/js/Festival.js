var ratedByUser = 0;
var mymap = null;

$(document).ready(function () {
    var mapArea = $('#map');
    if (mapArea == undefined) {
        return;
    }

    var festName = $('#FestivalName').val();
    var longitude = $('#Longitude').val();
    var latitude = $('#Latitude').val();

    mymap = L.map('map').setView([latitude, longitude], 5.00);
    //L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=sk.eyJ1IjoiZGFuaWplbGdsaXNvdmljIiwiYSI6ImNrcmt6YXgzODE3YnIyb3BlOWRwNW5nMWYifQ.rEAIMDQbEa1ui2bA3sCUNg', {
    //    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    //    maxZoom: 18,
    //    id: 'mapbox/streets-v11',
    //    tileSize: 512,
    //    zoomOffset: -1,
    //    accessToken: 'your.mapbox.access.token'
    //}).addTo(mymap);
    L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {
        attribution: 'Tiles &copy; Esri &mdash; Source: Esri, DeLorme, NAVTEQ, USGS, Intermap, iPC, NRCAN, Esri Japan, METI, Esri China (Hong Kong), Esri (Thailand), TomTom, 2012'
    }).addTo(mymap);

    var marker = L.marker([latitude, longitude]).addTo(mymap);
    marker.bindPopup("<b>" + festName + "</b>").openPopup();

    //setModal();

    // RADIAL PROGRESS BAR
    var festRate = $('#FestivalRate').val();
    InitProgressBar(festRate);
    //var max = -219.99078369140625;
    //$.each($('.progress'), function (index, value) {
    //    percent = festRate * 20;
    //    $(value).children($('.fill')).attr('style', 'stroke-dashoffset: ' + ((100 - percent) / 100) * max);
    //    $(value).children($('.value')).text(festRate + '/5.00');
    //});

    GetRatedByUser();

    //SetRating($('#RatedByUser').val());
    //SetRating();
    mymap.on('locationfound', onLocationFound);

    $('#followFestBtn').click(function () {
        const currentUserId = $('#LoggedUserId').val();
        $.ajax({
            method: 'POST',
            url: '/User/FollowFestival',
            data: { 'userId': currentUserId, 'festivalId': $('#FestivalId').val()},
            success: function (result) {
                if (result.code == 200) {
                    $('#followFestBtn').hide();
                    $('#unfollowFestBtn').show();
                }
            }
        });
    });

    $('#unfollowFestBtn').click(function () {
        const currentUserId = $('#LoggedUserId').val();
        $.ajax({
            method: 'POST',
            url: '/User/UnfollowFestival',
            data: { 'userId': currentUserId, 'festivalId': $('#FestivalId').val() },
            success: function (result) {
                if (result.code == 200) {
                    $('#followFestBtn').show();
                    $('#unfollowFestBtn').hide();
                }
            }
        });
    });

    followOrNot();

    mymap.locate(/*{ setView: true, maxZoom: 16 }*/);
});

$('#addNewComment').click(function () {
    var commentText = $('#newCommentText').val();
    if (commentText == undefined || commentText == '') {
        $('#newCommentValidation').show();
        return;
    }

    $('#newCommentValidation').hide();
    //string userId, string festivalId, string commentBody
    $.ajax({
        type: "POST",
        url: "/Festival/PostComment",
        data: {
            'userId': $('#LoggedUserId').val(), 'festivalId': $('#FestivalId').val(), 'commentBody': commentText
        },
        success: function (result) {
            if (result.code == 200) {
                AddNewComment(commentText, result.creationDate);
                $('#newCommentText').val('');
            }
        }
    });
});

$('.ratingStar').change(function () {
    var rateVal = $(this).val();
    var link = "/Festival/CreateRate";
    if (ratedByUser > 0) {
        link = "/Festival/UpdateRate";
    }

    $.ajax({
        data: {
            'userId': $('#LoggedUserId').val(), 'festivalId': $('#FestivalId').val(), 'rateValue': rateVal},
        type: "POST",
        url: link,
        success: function (result) {
            if (result.code == 200) {
                InitProgressBar(result.rate);
                ratedByUser = rateVal;
            }
        }
    });
});

//Sets rating star if user alerady voted.
function SetRating() {
    if (ratedByUser == 0.5) {
        $('#starhalf').prop('checked', true);
    }
    else if (ratedByUser == 1) {
        $('#star1').prop('checked', true);
    }
    else if (ratedByUser == 1.5) {
        $('#star1half').prop('checked', true);
    }
    else if (ratedByUser == 2) {
        $('#star2').prop('checked', true);
    }
    else if (ratedByUser == 2.5) {
        $('#star2half').prop('checked', true);
    }
    else if (ratedByUser == 3) {
        $('#star3').prop('checked', true);
    }
    else if (ratedByUser == 3.5) {
        $('#star3half').prop('checked', true);
    }
    else if (ratedByUser == 4) {
        $('#star4').prop('checked', true);
    }
    else if (ratedByUser == 4.5) {
        $('#star4half').prop('checked', true);
    }
    else if (ratedByUser == 5) {
        $('#star5').prop('checked', true);
    }
}

function GetRatedByUser() {
    //var queryToSend = { 'query': 'query{festivalById(id: "' + $('#FestivalId').val() + '"){rateByCurrentUser}}' };
    $.ajax({
        type: "post",
        url: "/Festival/GetUserRateForFestival",
        data: { 'festivalId': $('#FestivalId').val(), 'userId': $('#LoggedUserId').val()},
        success: function (response) {
            if (response.code == 200) {
                ratedByUser = response.rate;
                SetRating();
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function InitProgressBar(progress) {
    var max = -219.99078369140625;
    $.each($('.progress'), function (index, value) {
        percent = progress * 20;
        $(value).children($('.fill')).attr('style', 'stroke-dashoffset: ' + ((100 - percent) / 100) * max);
        $(value).children($('.value')).text(progress + '/5.00');
    });
}

function AddNewComment(commentText, commentDate) {
    var newElem = '<li><div class="commenterImage"><img src="https://placekitten.com/50/50" />';
    newElem += '<span class="sub-text">' + $('#LoggedUsername').val() + '</span></div>';
    newElem += '<div class="commentText">';
    newElem += '<p class="">' + commentText + '</p> <span class="date sub-text">' + commentDate + '</span>';
    newElem += '</div></li>';

    $('#commentListUl').append(newElem);
}

function deleteFestival() {
    var festivalId = $('#FestivalId').val();
    setModal(festivalId);
}

function updateFestival() {
    var festivalId = $('#FestivalId').val();
    location.href = '/Festival/UpdateFestival?festivalId=' + festivalId;
}

function setModal(festivalId) {
    // Get the modal
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal
    var btnNo = document.getElementById("btnNo");

    var btnYes = document.getElementById("btnYes");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    btnNo.onclick = function () {
        modal.style.display = "none";
    }

    btnYes.onclick = function () {
        deleteFestivalConfirmed(festivalId);
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

function CloseModal() {
    var modal = document.getElementById("myModal");
    modal.style.display = "none";
}

function deleteFestivalConfirmed(festivalId) {
    var loggedInUser = $('#LoggedUserId').val();
    $.ajax({
        type: "POST",
        data: { 'festivalId': festivalId, 'userId': loggedInUser },
        url: '/Festival/DeleteFestival',
        success: function (result) {
            if (result.code == 200) {
                location.href = '/Home/Index';
            }
            else {
                if (!result.authorized) {
                    CloseModal();
                    setErrorModalText('You are not authorized for this action!');
                    setErrorModal();
                }
                if (result.message) {
                    CloseModal();
                    setErrorModalText(result.message);
                    setErrorModal();
                }
            }
        }
    })
}

function setErrorModal() {
    // Get the modal
    var modal = document.getElementById("myErrorModal");

    // Get the button that opens the modal
    //var btnNo = document.getElementById("btnNo");

    //var btnYes = document.getElementById("btnYes");

    // Get the <span> element that closes the modal
    //var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    //btnNo.onclick = function () {
    //    modal.style.display = "none";
    //}

    //btnYes.onclick = function () {
    //    deleteFestivalConfirmed(festivalId);
    //}

    // When the user clicks on <span> (x), close the modal
    //span.onclick = function () {
    //    modal.style.display = "none";
    //}

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}

function setErrorModalText(text) {
    $('#errorMessageModalError').text('');
    $('#errorMessageModalError').text(text);
}

function onLocationFound(e) {
    var circleStyle = {};
    circleStyle.radius = e.accuracy;
    circleStyle.color = 'red';
    circleStyle.fillColor = '#f03';
    circleStyle.fillOpacity = 0.5;

    var customIcon = L.icon({
        iconUrl: '/Festival/GetPin',
        shadowUrl: '/Festival/GetShadow',

        iconSize: [75, 75], // size of the icon
        shadowSize: [50, 64], // size of the shadow
        iconAnchor: [35, 75], // point of the icon which will correspond to marker's location
        shadowAnchor: [4, 62],  // the same for the shadow
        popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
    });

    var markerStyle = {};
    markerStyle.color = 'red';
    markerStyle.icon = customIcon;

    L.marker(e.latlng, markerStyle).addTo(mymap)
        .bindPopup("You are within " + circleStyle.radius + " meters from this point");//.openPopup();

    L.circle(e.latlng, circleStyle).addTo(mymap);
}

function followOrNot() {
    const currentUserId = $('#LoggedUserId').val();
    if (currentUserId == '' || currentUserId == undefined || currentUserId == null) {
        return;
    }

    $.ajax({
        method: 'POST',
        url: '/User/CheckIfUserFollows',
        data: { 'userId': currentUserId, 'festivalId': $('#FestivalId').val() },
        success: function (result) {
            if (result.code == 200) {
                if (result.follows) {
                    $('#unfollowFestBtn').show();
                }
                else {
                    $('#followFestBtn').show();
                }
            }
        }
    })
}