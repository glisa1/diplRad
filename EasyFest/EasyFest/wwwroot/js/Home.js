//var query = JSON.stringify("query{ \nfestivals \n{ \nid \nname \n}\n}");

var query = {};
query.festivals = { name };

var obj = JSON.stringify(query);

$.ajax({
    method: 'POST',
    data: obj,
    url: 'https://localhost:44337/graphql',
    contentType: 'application/json'
}).done(function(result) {
        cosnole.log(result);
});