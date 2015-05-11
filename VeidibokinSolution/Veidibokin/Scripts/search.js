$(function () {
    $('#searchbutton').click(function () {
        console.log("clicked");
        $.ajax({
            url: $(this).data('url'),
            type: 'GET',
            cache: false,
            success: function (result) {
                console.log("Success", result);
                $('#msmqpartial').html(result);
            }
        });
        return false;
    });
});