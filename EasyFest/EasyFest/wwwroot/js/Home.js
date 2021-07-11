var query = "query{ \nfestivals \n{ \nid \nname \n}\n}";

$.ajax({
    method: 'POST',
    data: JSON.stringify(query),
    url: 'https://localhost:44337/graphql',
    contentType: 'application/json'
}).done(function(result) {
        cosnole.log(result);
}).error(function(result) {
    cosnole.log(result);
});