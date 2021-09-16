function deleteAccount(accId) {
    setModal(accId);
};

function setModal(accountId) {
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
        deleteAccountConfirmed(accountId);
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

function setErrorModal() {
    // Get the modal
    var modal = document.getElementById("myErrorModal");

    // Get the button that opens the modal
    var btnOk = document.getElementById("btnOk");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[1];

    // When the user clicks on the button, open the modal
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

    btnOk.onclick = function () {
        modal.style.display = "none";
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

function deleteAccountConfirmed(accountId) {
    $.ajax({
        type: "POST",
        data: { 'userId': accountId },
        url: '/User/DeleteAccount',
        success: function (result) {
            if (result.code == 200) {
                location.href = '/Home/Index';
            }
        }
    })
}

$(document).ready(function () {
    $('#editImgBtn').click(function () {
        $('#file-input').trigger('click');
    });

    $('#file-input').change(function () {
        const elem = $(this);
        if (elem.val() == null || elem.val() == undefined || elem.val() == '') {
            return;
        }

        var file = document.getElementById('file-input').files[0];
        var imageGuid = $('#imageGuid').val();

        var formData = new FormData();
        formData.append('imageGuid', imageGuid);
        formData.append('fileBytes', file);

        //var reader = new FileReader();
        //var fileByteArray = [];
        //reader.readAsArrayBuffer(file);
        //reader.onloadend = function (evt) {
        //    if (evt.target.readyState == FileReader.DONE) {
        //        var arrayBuffer = evt.target.result,
        //            array = new Uint8Array(arrayBuffer);
        //        for (var i = 0; i < array.length; i++) {
        //            fileByteArray.push(array[i]);
        //        }
        //    }
        //}

        $.ajax({
            url: '/User/EditProfileImage',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: formData,
            success: function (result) {
                if (result.code == 200) {
                    var img = $('#profileImgContainer');
                    img.prop('src', '/Home/ProfileImage?imageName=' + imageGuid + '&' + + new Date().getTime())
                }
                else {
                    setErrorModal();
                }
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    });

    $('.my-prof-tag-btn').each(function (index) {
        if ($.inArray($(this).val(), selectedTags) !== -1) {
            let newElem = '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check" viewBox="0 0 16 16">';
            newElem += '<path d="M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z" />';
            newElem += '</svg>';

            $(this).append(newElem);
            $(this).addClass('user-selected-tag');
        }
    });

    $('.my-prof-tag-btn').click(function () {
        const userId = $('#userId').val();
        if ($(this).hasClass('user-selected-tag')) {
            removeTag(this, userId, $(this).val());
        }
        else {
            addTag(this, userId, $(this).val());
        }
    });
});

function addTag(elem, userId, tagId) {
    $.ajax({
        method: 'POST',
        data: { 'userId': userId, 'tagId': tagId },
        url: '/User/AddTagToUser',
        success(response) {
            if (response.code == 200) {
                selectedTags.push($(elem).val());

                let newElem = '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check" viewBox="0 0 16 16">';
                newElem += '<path d="M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z" />';
                newElem += '</svg>';

                $(elem).append(newElem);

                $(elem).addClass('user-selected-tag');
            }
            else {
                setTimeout(function () {
                    $('#tags-error-p').show();
                }, 2000);
            }
        },
        error: function () {
            setTimeout(function () {
                $('#tags-error-p').show();
            }, 2000);
        }
    });
}

function removeTag(elem, userId, tagId) {
    $.ajax({
        method: 'POST',
        data: {'userId': userId, 'tagId': tagId},
        url: '/User/RemoveTagFromUser',
        success: function (response) {
            if (response.code == 200) {
                const index = selectedTags.indexOf(tagId);
                if (index > -1) {
                    selectedTags.splice(index, 1);
                }

                $('svg', elem).remove();

                $(elem).removeClass('user-selected-tag');
            }
            else {
                setTimeout(function () {
                    $('#tags-error-p').show();
                }, 2000);
            }
        },
        error: function () {
            setTimeout(function () {
                $('#tags-error-p').show();
            }, 2000);
        }
    });
}