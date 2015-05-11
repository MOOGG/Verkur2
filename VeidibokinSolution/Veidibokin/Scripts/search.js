$(function () {
    $("#searchform").on('submit', 'form', function () {
        var theForm = $(this);
        $.ajax({
            type: 'POST',
            url: theForm.attr('action'),
            data: theForm.serialize(),
        }).done(function (result) {

            var resultHtml = $(result).find('#results');

            $('#results').replaceWith(resultHtml);

        }).fail(function () {
            alert('Villa kom upp!');
        });

        return false;
    });

});