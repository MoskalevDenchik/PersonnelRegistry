$("#buttonForm input[type='submit']").on('click', function ()
{
    var action = $("#buttonForm").attr('action') + $(this).data("action");
    $("#buttonForm").attr('action', action);
})


$('.tableClick').on('click', 'tr', function ()
{
    var clickedRow = $(this);

    clickedRow.parent().children().each(function (index, element)
    {
        if ($(element).hasClass("active") && $(element).get(0).innerText != clickedRow.get(0).innerText)
        {
            $(element).removeClass("active");
        }
    });
    clickedRow.toggleClass("active");

    $("#buttonForm input[type='submit']").toggleClass("disabled", !($(this).hasClass("active")));

    $('#userId').val(clickedRow.children(0).html());
})

$('.selectlist').on('change', function ()
{
    $('.selectlist option:selected').html();
    $('#myForm').submit();
})

