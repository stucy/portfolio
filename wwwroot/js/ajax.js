$(document).ready(() => {

    Number.prototype.padLeft = function (base, chr) {
        var len = (String(base || 10).length - String(this).length) + 1;
        return len > 0 ? new Array(len).join(chr || '0') + this : this;
    }

})

//Add Comment
$("#comment").on("keypress", e => {
    if (e.keyCode == 13) {
        let username = $('#username').val();
        let comment = $('#comment').val();
        let userId = $('#user_id').val();

        console.log({ username, comment, userId})

        if (username != '' && comment != '') {

            let currentdate = new Date();

            /*let date = currentdate.toLocaleString();*/

            let model = { Username: username, Comment: comment, UserId: userId}

            $.ajax({
                type: 'POST',
                url: "Comments/Create",
                headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
                data: model,
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

//Delete Comment
const deleteComment = function (id, el) {

    if (confirm('Are you sure you want to delete your comment?')) {

        let userId = $('#user_id').val();

        let model = { CommentId: id, UserId: userId }

        $.ajax({
            type: 'POST',
            url: `Comments/Delete/`,
            data: model,
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
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

//Remove Error message
$('#username, #comment').on('keypress', function (e) {
    if (e.keyCode != 13) {
        $(this).removeClass('active');
    }
})

//Regiser
$('#registerSubmit').click(() => {
    let username = $('#register_username').val();
    let email = $('#register_email').val();
    let pass1 = $('#register_pass1').val();
    let pass2 = $('#register_pass2').val();

    console.log({ username, email, pass1, pass2 });

    if (pass1 != pass2) {
        toastr.warning("Passwords do not match!");
        return;
    }

    if (username == '' || email == '' || pass1 == '') {
        toastr.warning('Please fill all of the inputs!');
        return
    }

    let model = { UserName: username, Email: email, Password: pass1 }

    $.ajax({
        type: 'POST',
        url: `User/Register/`,
        data: model,
        success: function (res) {

            console.log(res);
            if (res.communicationCode == 0) {
                toastr.warning("There was an error with your registration");
            }
            else if (res.communicationCode == 1) {
                toastr.success('You have successfully registered! Please login!');

                $('#register_username').val('');
                $('#register_email').val('');
                $('#register_pass1').val('');
                $('#register_pass2').val('');

                $('#register').removeClass('open');
                $('#backdrop').removeClass('active');
            }
            else if (res.communicationCode == 2) {
                toastr.warning("This email is already taken!");
            }

        },
        error: function (err) {
            console.log(err);
            toastr.warning('Oops! Something went wrong!');
        }
    });

})

//Login
$('#loginSubmit').click(() => {
    let username = $('#login_username').val();
    let pass = $('#login_password').val();

    console.log({ username, pass });

    let model = { Email: username, Password: pass };

    $.ajax({
        type: 'POST',
        url: `User/Login/`,
        data: model,
        success: function (res) {
            console.log(res);

            if (res.communicationCode == 0) {
                toastr.warning('This user does not exist!');
            }
            else if (res.communicationCode == 1) {
                toastr.warning('Wrong Email or Pasword!');
            }
            else if (res.communicationCode == 2) {
                window.location.reload();
            }

        },
        error: function (err) {
            console.log(err);
            toastr.warning('Oops! Something went wrong!');
        }
    });

})

$('#mobileNav').click(() => {
    $('#navigation').toggleClass('active');
});

$("a.scrollLink").click(function (event) {
    event.preventDefault();
    $("html, body").animate({ scrollTop: $($(this).attr("href")).offset().top }, 500);
    $('#navigation').removeClass('active');
});