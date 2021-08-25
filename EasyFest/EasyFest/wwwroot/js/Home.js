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
});

function newFestival() {
    location.href = '/Festival/NewFestival';
}

function getMoreFestivals() {
    var searchTerm = $('#term').val();
    let query = '';
    if (searchTerm == '' || searchTerm == undefined || searchTerm == null) {
        query = 'query {festivals(first: 10, after: "' + lastCursorName + '"){ edges { '
            + ' cursor node{ id name day month rate description numberOfComments imageName festivalLocation{city state}}} pageInfo{hasNextPage} totalCount }}';
    }
    else {
        query = 'query {festivals(first: 10, after: "' + lastCursorName + '", where:{or: [{name: {contains: "' + searchTerm + '"}}, {description: {contains: "' + searchTerm + '"}}]}){ edges { '
            + ' cursor node{ id name day month rate description numberOfComments imageName festivalLocation{city state}}} pageInfo{hasNextPage} totalCount }}';
    }

    makeGraphQLCall(query);
}

//function createSearchFestivalQuery(term) {
//    var query = 'query{festivals(where:{or: [{name: {contains: "' + term + '"}}, {description: {contains: "' + term + '"}}]})';
//    query += '{ id name day month rate description numberOfComments imageName}}';
//    makeGraphQLCall(query);
//}

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
        var newFestivalHtml = '<div class="col-lg-6 mb-4"><div class="card h-100">';
        if (festival.imageName != '' || festival.imageName != null || festival.imageName != undefined) {
            newFestivalHtml += '<a href="' + location.href + '/Festival/FestivalDetails?festivalId=' + festival.id + '"><img class="card-img-top" src="/Home/Image?imageName=' + festival.imageName + '" alt=""></a>';
        }
        newFestivalHtml += '<div class="card-body festInfoContainer"><div style="width: 100%;">';
        newFestivalHtml += '<h4 class="card-title" style="float:left; width:80%;"><a href="#">' + festival.name + '</a></h4>';
        newFestivalHtml += '<div style="float:right; width:auto;"><i class="glyphicon glyphicon-star"><span>' + festival.rate + '</span></i>';
        newFestivalHtml += '<i class="glyphicon glyphicon-comment"><span>' + festival.numberOfComments + '</span></i></div>';
        newFestivalHtml += '<div style="float:left;"><p class="card-text">' + festival.description + '</p></div>';
        newFestivalHtml += '</div></div>';
        $('#festivalsContainer').append(newFestivalHtml);
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