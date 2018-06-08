phoneNumber = 0;

$("#AddPhone").on('click', function ()
{
    AddNewPhone();
})

function AddNewPhone()
{
    $.ajax({
        url: "/Employees/AddPhone",
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