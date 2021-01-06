$(document).ready(() => {
    if (localStorage.getItem('sessID') == null) {
        let newSess = Math.random().toString(36).substr(2, 9);
        localStorage.setItem('sessID', newSess);
    }

    Number.prototype.padLeft = function (base, chr) {
        var len = (String(base || 10).length - String(this).length) + 1;
        return len > 0 ? new Array(len).join(chr || '0') + this : this;
    }

})

$("#comment").on("keypress", e => {
    if (e.keyCode == 13) {
        let username = $('#username').val();
        let comment = $('#comment').val();
        let session = localStorage.getItem('sessID');

        /*console.log({username, comment})*/

        if (username != '' && comment != '') {

            var currentdate = new Date();
            var date = currentdate.getDate() + "-"
                + (currentdate.getMonth() + 1) + "-"
                + currentdate.getFullYear() + "  "
                + currentdate.getHours() + ":"
                + currentdate.getMinutes() + ":"
                + currentdate.getSeconds();

            $.ajax({
                type: 'POST',
                url: "Comments/Create",
                headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                data: {
                    "Username": username,
                    "Comment": comment,
                    "userSession": session,
                    "Date": date
                },
                success: function (res) {
                    console.log(res);

                    if (res.communicationCode == 1) {

                        let date =  [currentdate.getDate().padLeft(),
                                    (currentdate.getMonth() + 1).padLeft(),
                                    currentdate.getFullYear()].join('.') + ' ' +
                                    [currentdate.getHours().padLeft(),
                                    currentdate.getMinutes().padLeft(),
                                    currentdate.getSeconds().padLeft()].join(':') + ' ';

                        let commentEl = `<li><span class="username">${username}:</span>${comment}<span class="date">${date}<span class="delete" onclick="deleteComment(${res.comment.commentId}, this)">x</span></span></li>`;
                        document.getElementById('commentContainer').insertAdjacentHTML('afterbegin', commentEl);
                        $('#username').val('');
                        $('#comment').val('');

                    } else {
                        toastr.warning('Oops! Something went wrong!');
                    }

                },
                error: function (err) {
                    console.log(err);
                    toastr.warning('Oops! Something went wrong!');
                }
            })
           
        }

        if (username == '') {
            $('#username').addClass('active');
        }

        if (comment == '') {
            $('#comment').addClass('active');
        }

    }
})

const deleteComment = function (id, el) {

    if (confirm('Are you sure you want to delete your comment?')) {

        let session = localStorage.getItem('sessID');

        $.ajax({
            type: 'POST',
            url: `Comments/Delete/${id}/${session}`,
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            data: {
                "sess": session
            },
            success: function (res) {
                console.log(res);

                if (res.communicationCode == 1) {

                    let elem = el.parentNode.parentNode;
                    $(elem).remove();
                    toastr.success('Comment was deleted!');

                } else if (res.communicationCode == 2) {
                    toastr.warning('You can\' delete another persons comment!');
                }
                else {
                    toastr.warning('Oops! Something went wrong!');
                }

            },
            error: function (err) {
                console.log(err);
                toastr.warning('Oops! Something went wrong!');
            }
        })

    }
}

$('#username, #comment').on('keypress', function (e) {
    if (e.keyCode != 13) {
        $(this).removeClass('active');
    }
})