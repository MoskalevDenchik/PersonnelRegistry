emailNumber = 0;
//$(document).ready(function ()
//{
//    AddNewEmail();
//});

$("#AddEmail").on('click', function ()
{
    AddNewEmail();
})

function AddNewEmail()
{
    $.ajax({
        url: "/Employees/AddEmail",
        type: "GET",
        data: {
            number: emailNumber
        },
        success: function (respons)
        {
            $("#Emails").append(respons);
            emailNumber++;
        }
    })
}