let selectedTags = [];

$(document).ready(function () {
    var hasError = $('#HasError').val();
    if (hasError) {
        var errorMsg = $('#ErrorMessage').val();
        $('#errorMessageModal').text(errorMsg);
        setModal();
    }

    $('.reg-tag-btn').click(function () {
        if ($(this).hasClass('reg-selected-tag')) {

            const index = selectedTags.indexOf($(this).val());
            if (index > -1) {
                selectedTags.splice(index, 1);
            }

            $('svg', this).remove();

            $(this).removeClass('reg-selected-tag');
        }
        else {
            selectedTags.push($(this).val());

            let newElem = '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check" viewBox="0 0 16 16">';
            newElem += '<path d="M10.97 4.97a.75.75 0 0 1 1.07 1.05l-3.99 4.99a.75.75 0 0 1-1.08.02L4.324 8.384a.75.75 0 1 1 1.06-1.06l2.094 2.093 3.473-4.425a.267.267 0 0 1 .02-.022z" />';
            newElem += '</svg>';

            $(this).append(newElem);

            $(this).addClass('reg-selected-tag');
        }
    });

    $('#newUserForm').submit(function () {
        var form = $(this);
        selectedTags.forEach(function (val, i) {
            form.append('<input type="hidden" id="selectedTag_' + i + '" name="SelectedTags" value="' + val + '">');//[' + i + '] 
        });

        return true;
    });
});

function setModal() {
    // Get the modal
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal
    //var btn = document.getElementById("myBtn");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    //btn.onclick = function () {
    modal.style.display = "block";
    //}

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