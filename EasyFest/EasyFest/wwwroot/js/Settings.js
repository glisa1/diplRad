$(document).ready(function () {
    $('#newTagBtn').click(function () {
        const newTagName = $('#tag-name').val();
        if (newTagName == undefined || newTagName == null || newTagName == '') {
            $('#new-tag-error').hide();
            return;
        }
        const newTagColor = $('#tag-cl-picker').val();

        $.ajax({
            method: 'POST',
            data: { 'TagName': newTagName, 'TagColor': newTagColor },
            url: "/Home/NewTag",
            success: function(result) {
                if (result.code == 200) {
                    //new element
                    $('#new-tag-error').hide();

                    let newTagBtn = '<button class="btn tag-btn mr-2 mb-2" style="background-color: ' + newTagColor + '" type="button">' + newTagName + '</button>';

                    $('#settings-tags-container').append(newTagBtn);

                    $('#tag-name').val('');
                    $('#tag-cl-picker').val('');

                    //append to #settings-tags-container
                }
                else if (result.code == 400) {
                    //error
                    if (result.message != undefined || result.message != null || result.message != '') {
                        $('#new-tag-error').text(result.message);
                    }
                    else {
                        $('#new-tag-error').text('There was an error creating new tag.');
                    }

                    $('#new-tag-error').show();
                }
            },
            error: function(result) {
                    $('#new-tag-error').text('There was an error creating new tag.');
            }
        });
    });
});