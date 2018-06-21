$("input[type='submit']").on('click', function ()
{
    if ($("#EmployeeForm").valid())
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            async: false,
            url: "/Employees/Save",
            dataType: 'JSON',
            data: $("#EmployeeForm").serialize(),
            success: function (response)
            {
                if (response.Status === 0)
                {
                    window.location.href = "/Employees/Index";
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