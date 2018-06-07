
$("#Search").on('click', function ()
{
    GetPageData(1);
});



function GetPageData(pageNumber)
{
    $("#EmployeeList").empty();
    $("#paged").empty();
                                                       
    $.ajax({
        url: "/Employees/GetPageEmployeesBySearchParams",
        type: "GET",
        data: {
            firstName: $("input[name='firstName']").val(),
            lastName: $("input[name='lastName']").val(),
            middlename: $("input[name='middlename']").val(),
            fromYear: $("input[name='fromYear']").val(),
            toYear: $("input[name='toYear']").val(),
            IsWorking: $("input[name='IsWorking']").prop('checked'), 
            page: pageNumber
        },
        success: function (respons)
        {
            $('#EmployeeList').append(respons);
        }
    })
}

