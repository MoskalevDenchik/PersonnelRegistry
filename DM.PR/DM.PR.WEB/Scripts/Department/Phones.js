phoneNumber = 0;
$("#AddPhone").on('click', function ()
{
    AddNewPhone();
})

function AddNewPhone()
{
    $.ajax({
        url: "/Phone/AddPhone",
        type: "GET",
        data: {
            number: phoneNumber
        },
        success: function (respons)
        {
            $("#Phones").append(respons);
            phoneNumber++;
        }
    })
}