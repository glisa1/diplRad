let lastCursorName = '';
//class Festival {
//    constructor(id, name, day, month, rate, description, numberOfComments, imageName) {
//        this.id = id;
//        this.name = name;
//        this.day = day;
//        this.month = month;
//        this.rate = rate;
//        this.description = description;
//        this.numberOfComments = numberOfComments;
//        this.imageName = imageName;
//    }
//}

$(document).ready(function () {
    //$('#searchInput').on('keyup', function (event) {
    //    var code = event.keyCode || event.which;
    //    if (code !== 13) {
    //        return;
    //    }

    //    var searchVal = $(this).val();
    //    if (searchVal == '' || searchVal == null) {
    //        return;
    //    }

    //    createSearchFestivalQuery(searchVal);
    //});

    //$('#searchButton').click(function () {
    //    var searchVal = $('#searchInput').val();
    //    if (searchVal == '' || searchVal == null) {
    //        return;
    //    }

    //    createSearchFestivalQuery(searchVal);
    //});
    lastCursorName = $('#lastCursor').val();

    $('#searchForm').submit(function () {
        const searchTerm = $('#term').val();
        if (searchTerm == '' || searchTerm == undefined || searchTerm == null) {
            return false;
        }

        const isSearchDefault = $('#isSearchDefault').val() === 'True';
        if (!isSearchDefault) {
            location.href = '/Home/SearchByTags?userId=' + usId + '&term=' + $('#term').val() + '&lastCursorName=' + lastCursorName;
            return false;
        }

        return true;
    });

    const usId = $('#LoggedUserId').val();
    if (usId == '' || usId == undefined || usId == null) {
        $('.loginonly').remove();
    }
    else {
        $('#myTagsSearch').click(function () {
            location.href = '/Home/SearchByTags?userId=' + usId + '&term' + $('#term').val(); + '&lastCursorName' + lastCursorName;
        })
    }
});

function newFestival() {
    location.href = '/Festival/NewFestival';
}

function getMoreFestivals() {
    var searchTerm = $('#term').val();
    let query = '';
    const isSearchDefault = $('#isSearchDefault').val() === 'True';
    if (!isSearchDefault) {
        getMoreFestsByTag(searchTerm);
    }
    else {
        if (searchTerm == '' || searchTerm == undefined || searchTerm == null) {
            query = 'query {festivals(first: 10, after: "' + lastCursorName + '", order: {createdOn: DESC}){ edges { '
                + ' cursor node{ id name day month rate description numberOfComments images tagsList{name color} }} pageInfo{hasNextPage} totalCount }}';
        }
        else {
            query = 'query {festivals(first: 10, after: "' + lastCursorName + '", where:{or: [{name: {contains: "' + searchTerm + '"}}, {description: {contains: "' + searchTerm + '"}}]}, order: {createdOn: DESC}){ edges { '
                + ' cursor node{ id name day month rate description numberOfComments images tagsList{name color} }} pageInfo{hasNextPage} totalCount }}';
        }

        makeGraphQLCall(query);
    }
}

//function createSearchFestivalQuery(term) {
//    var query = 'query{festivals(where:{or: [{name: {contains: "' + term + '"}}, {description: {contains: "' + term + '"}}]})';
//    query += '{ id name day month rate description numberOfComments imageName}}';
//    makeGraphQLCall(query);
//}

function getMoreFestsByTag(searchTerm) {
    $.ajax({
        method: 'POST',
        data: { 'userId': $('#LoggedUserId').val(), 'term': searchTerm, 'lastCursorName': lastCursorName, 'isAjax': true},
        url: '/Home/SearchByTags'
    }).done(function (result) {
        if (result.data) {
            console.log(result.data);
            const edgesArray = result.data.festivalEdges;
            const hasNextPage = result.data.infoPage.hasNextPage;
            if (!hasNextPage) {
                $('#loadMoreFestsContainer').hide();
            }
            lastCursorName = edgesArray[edgesArray.length - 1].cursorName;
            appendNewFestivalsByTags(edgesArray);
        }
    });
}

function makeGraphQLCall(queryValue) {
    $.ajax({
        method: 'POST',
        //data: JSON.stringify({ 'query': 'query{ \nfestivals \n{ \nid \nname \n}\n}' }),
        data: JSON.stringify({ 'query': queryValue }),
        url: 'https://localhost:44337/graphql',
        contentType: 'application/json'
    }).done(function (result) {
        if (result.data) {
            console.log(result.data);
            const edgesArray = result.data.festivals.edges;
            const hasNextPage = result.data.festivals.pageInfo.hasNextPage;
            if (!hasNextPage) {
                $('#loadMoreFestsContainer').hide();
            }
            lastCursorName = edgesArray[edgesArray.length - 1].cursor;
            appendNewFestivals(edgesArray);
        }
    });
}

function appendNewFestivals(festivals) {
    // appendovati na #festivalsContainer
    for (var i = 0; i < festivals.length; i++) {
        const festival = festivals[i].node;
        var newFestivalHtml = '<div class="col-lg-6 mb-4"><div class="card h-100 festivalFader">';
        //if (festival.imageName != '' || festival.imageName != null || festival.imageName != undefined) {
        //    newFestivalHtml += '<a href="' + location.href + '/Festival/FestivalDetails?festivalId=' + festival.id + '"><img class="card-img-top" src="/Home/Image?imageName=' + festival.imageName + '" alt=""></a>';
        //}
        newFestivalHtml += '<div id="carouselFestivalList" class="carousel slide" data-ride="carousel"><div class="carousel-inner">';
        for (var j = 0; j < festival.images.length; j++) {
            if (j == 0) {
                newFestivalHtml += '<div class="carousel-item active">';
            }
            else {
                newFestivalHtml += '<div class="carousel-item">';
            }
            newFestivalHtml += '<a href="' + location.href + 'Festival/FestivalDetails?festivalId=' + festival.id + '"><img class="d-block w-100" src="/Home/Image?imageName=' + festival.images[j] + '" alt=""></a>';
            newFestivalHtml += '</div>';
        }
        newFestivalHtml += '</div>';
        newFestivalHtml += '<div style="position:absolute; bottom: 0;">';
        for (var j = 0; j < festival.tagsList.length; j++) {
            newFestivalHtml += '<div class="btn" style="margin-left:5px; margin-bottom: 10px; background-color:' + festival.tagsList[j].color + '">' + festival.tagsList[j].name + '</div>';
        }
        newFestivalHtml += '</div></div>';

        newFestivalHtml += '<div class="card-body festInfoContainer"><div style="width: 100%;">';
        newFestivalHtml += '<h4 class="card-title" style="float:left; width:80%;"><a href="' + location.href + 'Festival/FestivalDetails?festivalId=' + festival.id + '">' + festival.name + '</a></h4>';

        newFestivalHtml += '<div style="float:right; width:auto;"><div><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-chat-left-dots" viewBox="0 0 16 16">' +
            '<path d="M14 1a1 1 0 0 1 1 1v8a1 1 0 0 1-1 1H4.414A2 2 0 0 0 3 11.586l-2 2V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12.793a.5.5 0 0 0 .854.353l2.853-2.853A1 1 0 0 1 4.414 12H14a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"></path>' +
                '<path d="M5 6a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"></path>' +
            '</svg><strong>' + festival.numberOfComments + '</strong></div>';
        newFestivalHtml += '<div><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-star" viewBox="0 0 16 16">' +
            '<path d="M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.522-3.356c.33-.314.16-.888-.282-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288L8 2.223l1.847 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.565.565 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z"></path>' +
            '</svg><strong>' + festival.rate + '</strong></div></div>';
        //newFestivalHtml += '<div style="float:right; width:auto;"><i class="glyphicon glyphicon-star"><span>' + festival.rate + '</span></i>';
        //newFestivalHtml += '<i class="glyphicon glyphicon-comment"><span>' + festival.numberOfComments + '</span></i></div>';
        newFestivalHtml += '<div style="float:left;"><p class="card-text">' + festival.description + '</p></div>';
        newFestivalHtml += '</div></div>';
        $('#festivalsContainer').append(newFestivalHtml);
        $('.carousel').carousel({ interval: 5000 });
    }
}

function appendNewFestivalsByTags(festivals) {
    // appendovati na #festivalsContainer
    for (var i = 0; i < festivals.length; i++) {
        const festival = festivals[i].festival;
        var newFestivalHtml = '<div class="col-lg-6 mb-4"><div class="card h-100 festivalFader">';
        //if (festival.imageName != '' || festival.imageName != null || festival.imageName != undefined) {
        //    newFestivalHtml += '<a href="' + location.href + '/Festival/FestivalDetails?festivalId=' + festival.id + '"><img class="card-img-top" src="/Home/Image?imageName=' + festival.imageName + '" alt=""></a>';
        //}
        newFestivalHtml += '<div id="carouselFestivalList" class="carousel slide" data-ride="carousel"><div class="carousel-inner">';
        for (var j = 0; j < festival.images.length; j++) {
            if (j == 0) {
                newFestivalHtml += '<div class="carousel-item active">';
            }
            else {
                newFestivalHtml += '<div class="carousel-item">';
            }
            newFestivalHtml += '<a href="/Festival/FestivalDetails?festivalId=' + festival.id + '"><img class="d-block w-100" src="/Home/Image?imageName=' + festival.images[j] + '" alt=""></a>';
            newFestivalHtml += '</div>';
        }
        newFestivalHtml += '</div>';
        newFestivalHtml += '<div style="position:absolute; bottom: 0;">';
        for (var j = 0; j < festival.tags.length; j++) {
            newFestivalHtml += '<div class="btn" style="margin-left:5px; margin-bottom: 10px; background-color:' + festival.tags[j].color + '">' + festival.tags[j].name + '</div>';
        }
        newFestivalHtml += '</div></div>';

        newFestivalHtml += '<div class="card-body festInfoContainer"><div style="width: 100%;">';
        newFestivalHtml += '<h4 class="card-title" style="float:left; width:80%;"><a href="/Festival/FestivalDetails?festivalId=' + festival.id + '">' + festival.name + '</a></h4>';

        newFestivalHtml += '<div style="float:right; width:auto;"><div><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-chat-left-dots" viewBox="0 0 16 16">' +
            '<path d="M14 1a1 1 0 0 1 1 1v8a1 1 0 0 1-1 1H4.414A2 2 0 0 0 3 11.586l-2 2V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12.793a.5.5 0 0 0 .854.353l2.853-2.853A1 1 0 0 1 4.414 12H14a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"></path>' +
            '<path d="M5 6a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z"></path>' +
            '</svg><strong>' + festival.numberOfComments + '</strong></div>';
        newFestivalHtml += '<div><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-star" viewBox="0 0 16 16">' +
            '<path d="M2.866 14.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.522-3.356c.33-.314.16-.888-.282-.95l-4.898-.696L8.465.792a.513.513 0 0 0-.927 0L5.354 5.12l-4.898.696c-.441.062-.612.636-.283.95l3.523 3.356-.83 4.73zm4.905-2.767-3.686 1.894.694-3.957a.565.565 0 0 0-.163-.505L1.71 6.745l4.052-.576a.525.525 0 0 0 .393-.288L8 2.223l1.847 3.658a.525.525 0 0 0 .393.288l4.052.575-2.906 2.77a.565.565 0 0 0-.163.506l.694 3.957-3.686-1.894a.503.503 0 0 0-.461 0z"></path>' +
            '</svg><strong>' + festival.rate + '</strong></div></div>';
        //newFestivalHtml += '<div style="float:right; width:auto;"><i class="glyphicon glyphicon-star"><span>' + festival.rate + '</span></i>';
        //newFestivalHtml += '<i class="glyphicon glyphicon-comment"><span>' + festival.numberOfComments + '</span></i></div>';
        newFestivalHtml += '<div style="float:left;"><p class="card-text">' + festival.description + '</p></div>';
        newFestivalHtml += '</div></div>';
        $('#festivalsContainer').append(newFestivalHtml);
        $('.carousel').carousel({ interval: 5000 });
    }
}

//function prepareFestivals(data) {
//    var festivals = [];

//    for (let i = 0; i < data.length; i++) {
//        let d = data[i];
//        festivals.push(new Festival(d.id, d.name, d.day, d.month, d.rate, d.description, d.numberOfComments, d.imageName));
//    }

//    return festivals;
//}

//function renderFoundFestivals(festivals) {

//    // First clear all previously shown festivals.
//    const myNode = document.getElementById("festivalsContainer");
//    myNode.innerHTML = '';

//    if (festivals.length === 0) {
//        // Render found nothing.
//        return;
//    }
//}