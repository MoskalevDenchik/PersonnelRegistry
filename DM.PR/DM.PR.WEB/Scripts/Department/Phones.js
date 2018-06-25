$("#AddPhone").on('click', function ()
{
    AddNewPhone();
})

$("#RemovePhone").on('click', function ()
{
    RemovePhone();
})


function AddNewPhone()
{
    var phoneNumber = $("#Phones div").length;

    if (phoneNumber > 2)
    {
        alert("Достаточно телефонов")

    } else
    {
        var copy = $("#Phones div:first").clone();

        copy.find("span:first").attr("data-valmsg-for", "Phones[" + phoneNumber + "].Number");

        copy.find("input[type='hidden']").attr("name", "Phones[" + phoneNumber + "].Id");
        copy.find("input[type='hidden']").attr("id", "Phones" + phoneNumber + "__Id");

        copy.find("input[type='text']").attr("name", "Phones[" + phoneNumber + "].Number");
        copy.find("input[type='text']").attr("id", "Phones" + phoneNumber + "__Number");
        copy.find("input[type='text']").val("");

        copy.appendTo("#Phones");
    }
}


function RemovePhone()
{
    if ($("#Phones div").length < 2)
    {
        alert("Один должен быть по любэ")
    }
    else
    {
        $("#Phones div:last").remove();
    }

}