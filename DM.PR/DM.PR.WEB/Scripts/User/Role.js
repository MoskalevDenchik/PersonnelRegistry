$("#AddRole").on('click', function ()
{
    AddNewRole();
})

$("#RemoveRole").on('click', function ()
{
    RemoveEmail();
})

function AddNewRole()
{

    var roleNumber = $("#Roles div").length;
    if (roleNumber > $("#Roles div:first select").length)
    {
        alert("Больше ролей не может быть");

    } else
    {
        var copy = $("#Roles div:first").clone();

        copy.find("span:first").attr("data-valmsg-for", "Roles[" + roleNumber + "].Id");

        copy.find("select:first").attr("name", "Roles[" + roleNumber + "].Id");
        copy.find("select:first").attr("id", "Roles_" + roleNumber + "__Id");

        copy.appendTo("#Roles");
    }
}

function RemoveEmail()
{
    if ($("#Roles div").length < 2)
    {
        alert("Один должен быть по любэ")
    }
    else
    {
        $("#Roles div:last").remove();
    }
}