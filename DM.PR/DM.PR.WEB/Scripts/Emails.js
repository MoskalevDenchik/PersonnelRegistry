emailNumber = 1;  

$("#AddEmail").on('click', function ()
{
    AddNewEmail();
})

function AddNewEmail()
{
    //$.ajax({
    //    url: "/Employees/AddEmail",
    //    type: "GET",
    //    data: {
    //        number: emailNumber
    //    },
    //    success: function (respons)
    //    {
    //        $("#Emails").append(respons);
    //        emailNumber++;
    //    }
    //})

    var a = $("#email").clone();
    var b = a.filter("input:last").text();
    a.val("Hello");
    a.prependTo("#Emails");


}