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