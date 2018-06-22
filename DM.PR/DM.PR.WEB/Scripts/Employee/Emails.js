emailNumber = 1;
$("#AddEmail").on('click', function ()
{
    AddNewEmail();
})
 
function AddNewEmail()
{
    var copy = $("#Emails div:first").clone();

    copy.find("span:first").attr("data-valmsg-for", "Emails[" + emailNumber + "].Address");

    copy.find("input[type='hidden']").attr("name", "Emails[" + emailNumber + "].Id");
    copy.find("input[type='hidden']").attr("id", "Emails_" + emailNumber + "__Id");

    copy.find("input[type='text']").attr("name", "Emails[" + emailNumber + "].Address");
    copy.find("input[type='text']").attr("id", "Emails_" + emailNumber + "__Address");

    copy.appendTo("#Emails");
    emailNumber++;
}