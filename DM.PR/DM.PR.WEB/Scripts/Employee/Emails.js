$("#AddEmail").on('click', function ()
{
    AddNewEmail();
})

$("#RemoveEmail").on('click', function ()
{
    RemoveEmail();
})
 
function AddNewEmail()
{

    var emailNumber = $("#Emails div").length;
    if (emailNumber>2)
    {
        alert("Достаточно E-mails");

    } else
    {
        var copy = $("#Emails div:first").clone();

        copy.find("span:first").attr("data-valmsg-for", "Emails[" + emailNumber + "].Address");

        copy.find("input[type='hidden']").attr("name", "Emails[" + emailNumber + "].Id");
        copy.find("input[type='hidden']").attr("id", "Emails_" + emailNumber + "__Id");

        copy.find("input[type='text']").attr("name", "Emails[" + emailNumber + "].Address");
        copy.find("input[type='text']").attr("id", "Emails_" + emailNumber + "__Address");

        copy.appendTo("#Emails");
    }



}

function RemoveEmail()
{
    if ($("#Emails div").length < 2)
    {
        alert("Один должен быть по любэ")
    }
    else
    {
        $("#Emails div:last").remove();
    }

}