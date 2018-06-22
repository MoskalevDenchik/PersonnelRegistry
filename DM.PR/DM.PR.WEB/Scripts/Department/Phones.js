phoneNumber = 1;
$("#AddPhone").on('click', function ()
{
    AddNewPhone();
})

function AddNewPhone()
{
    var copy = $("#Phones div:first").clone();

    copy.find("span:first").attr("data-valmsg-for", "Phones[" + phoneNumber + "].Number");

    copy.find("input[type='hidden']").attr("name", "Phones[" + phoneNumber + "].Id");
    copy.find("input[type='hidden']").attr("id", "Phones" + phoneNumber + "__Id");

    copy.find("input[type='text']").attr("name", "Phones[" + phoneNumber + "].Number");
    copy.find("input[type='text']").attr("id", "Phones" + phoneNumber + "__Number");

    copy.appendTo("#Phones");
    phoneNumber++;
}