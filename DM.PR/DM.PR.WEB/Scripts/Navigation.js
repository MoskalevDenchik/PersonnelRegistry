$(document).ready(function ()
{
    GetPageData(0, 1);
});

$("#menu li").on('click', function ()
{
    var curent = $(this).html();        
    var departmentId = $(curent).data("department");
    GetPageData(departmentId, 1);
});

$("#paged li").on('click', function ()
{
    var curent = $(this).html();
    var pageNumber = $(curent).data("page");
    var department = $("#menu li:active")
    GetPageData(,page);
});



function GetPageData(departmentId, pageNumber)
{
    $("#EmployeesList").empty();

    $.ajax({
        url: "/Employees/GetPageEmployeesByDepartmentId",
        type: "GET",
        data: {
            departmentId: departmentId,
            page: pageNumber
        },
        success: function (response)
        {
            $("#EmployeesList").append(response);
        }
    })
}