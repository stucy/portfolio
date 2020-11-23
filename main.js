(function () {
    // clearStorage();
    
    if(localStorage.getItem('numComments') == null)
        localStorage.setItem('numComments', '0');

    let storage = allComments();

    addComments(storage)
}());

$("#comment").on("keypress", e => {
    if(e.keyCode == 13){
        let username = $('#username').val();
        let comment = $('#comment').val();
        
        // console.log({username, comment})

        if(username != '' && comment != ''){

            var currentdate = new Date(); 
            var date = currentdate.getDate() + "/"
                + (currentdate.getMonth()+1)  + "/" 
                + currentdate.getFullYear() + " - "  
                + currentdate.getHours() + ":"  
                + currentdate.getMinutes() + ":" 
                + currentdate.getSeconds();

            let storageItem = `<li><span class="username">${username}:</span>${comment}<span class="date">${date}</span></li>`;

            let numComments = localStorage.getItem('numComments');

            localStorage.setItem(`comment${numComments}`, storageItem);

            let newNumCom = parseInt(numComments) + 1;

            // console.log(newNumCom)

            localStorage.setItem(`numComments`, newNumCom);

            addComments(allComments());
        }
        
        if(username == ''){
            $('#username').addClass('active');
        }

        if(comment == ''){
            $('#comment').addClass('active');
        }

    }
})

$('#username, #comment').on('keypress', function (e) {
    if(e.keyCode != 13){
        $(this).removeClass('active');
    }
})

function allComments() {

    var values = [],
        keys = Object.keys(localStorage).sort(),
        i = keys.length;

    keys.pop();

    // keys.reverse();

    while ( i-- ) {
        values.push( localStorage.getItem(keys[i]) );
    }

    return values;
}


function addComments(comments){
    let html = comments.join('');

    // console.log(comments);
    // console.log(html)
    
    document.getElementById('commentContainer').innerHTML = html
}

function clearStorage(){
    localStorage.clear();
}

$('#mobileNav').click(() => {
    $('#navigation').toggleClass('active');
});

$( "a.scrollLink" ).click(function( event ) {
    event.preventDefault();
    $("html, body").animate({ scrollTop: $($(this).attr("href")).offset().top }, 500);
    $('#navigation').removeClass('active');
});
