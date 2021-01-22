//Open Modal
$('a[data-toggle="modal"]').click(e => {
    let id = $(e.target).data('target');
    $(id).addClass('open');
    $('#backdrop').addClass('active');
});

//Close Modal
$('a[data-close="modal"]').click(e => {
    let id = $(e.target).data('target');
    console.log(id);
    $(id).removeClass('open');
    $('#backdrop').removeClass('active');
});

$('#openRegister').click(() => {
    $('#login').removeClass('open');
});

$('#backdrop').click(() => {

    if ($('#backdrop').hasClass('active')) {
        $('.modal.open').removeClass('open');
        $('#backdrop').removeClass('active');
    }

});