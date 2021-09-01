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
});