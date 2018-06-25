$("#buttonForm a").on("click", function ()
{
    var action = $(this).attr("href") + '/' + $("#tableData tr[class='active'] td:first").text();
    $(this).attr("href", action)
})

$('#tableData').on('click', 'tr', function ()
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

    $("#buttonForm a").filter(".changing").toggleClass("disabled", !($(this).hasClass("active")));
})


