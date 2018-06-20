$("input[type='submit']").on('click', function ()
{
    if ($("#userForm").valid())
    {
        event.preventDefault();

        var roles = [];

        var rolesArray = $("#Roles select  option:selected");

        for (var i = 0; i < rolesArray.length; i++)
        {
            roles[i] = rolesArray.eq(i).val();
        }


        var data = {
            Id: $("input[name='Id']").val(),
            EmployeeId: $("input[name='EmployeeId']").val(),
            Login: $("input[name='Login']").val(),
            Password: $("input[name='Password']").val(),
            Roles: roles
        }

        $.ajax({
            type: "POST",
            async: false,
            url: "/User/Create",
            dataType: 'JSON',
            data: data,
            success: function (response)
            {
                var a = $(response);

                if (response.Status === 0)
                {
                     
                }
                else
                {
                    for (var i = 0; i < response.Exception.length; i++)
                    {
                        $('span[data-valmsg-for="' + response.Exception[i].MemberNames[0] + '"]').text(response.Exception[i].ErrorMessage);
                    }
                }
            }
        });
    }
})