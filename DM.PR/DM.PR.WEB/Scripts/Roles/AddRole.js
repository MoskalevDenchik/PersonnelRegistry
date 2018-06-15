roleNumber = 1;

$("#AddRole").on('click', function ()
{
    AddNewRole();
})

function AddNewRole()
{
    $.ajax({
        url: "/User/AddRole",
        type: "GET",
        data: {
            selectedId: 0,
            number: roleNumber
        },
        success: function (respons)
        {
            $("#Roles").append(respons);
            roleNumber++;
        }
    })
}