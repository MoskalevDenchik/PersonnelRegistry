$(function ()
{
    $("#Search").click(function ()
    {
        $.ajax({
            url: "Employees/Serch",
            type: "Get",
            data: {
                FirstName: $("input[name='FirstName']").val(),
                LastName: $("input[name='LastName']").val(),
                Middlename: $("input[name='Middlename']").val(),
                WorkTime: $("input[name='WorkTime']").val(),
                IsWorking: $("input[name='IsWorking']").val()
            },
            success: function (response)
            {
                $("#EmployeeList")
            }
        })
    });
})