pageNumber = 1;
departmentId = 0;
pageSize = 5;
var parentActive;

$(document).ready(function ()
{
    GetPageData(departmentId, pageSize);
});

$("#menu").on('click', " li a", function ()
{
    var active = $("#menu li[class ='active']");

    if (parentActive == $(this).parent())
    {                                               
        active.val();                                                 
    }
    parentActive = $(this).parent();
    active.find("ul[class ='Children']").empty();
    active.removeClass('active');
    $(this).parent().addClass("active");

    pageNumber = 1;
    departmentId = $(this).data("department");
    GetPageData(departmentId, pageSize);

    UpdateMenu(departmentId);
});

$('#paged').on('click', 'li', function ()
{
    var curent = $(this).html();
    pageNumber = $(curent).data("page");

    GetPageData(departmentId, pageSize);
})

function UpdateMenu(departmentId)
{
    $.ajax({
        url: "/Navigation/GetDepartmentChildren",
        type: "GET",
        data: {
            departmentId: departmentId,
        },
        success: function (response)
        {
            if (response != null)
            {
                $("#menu li[class ='active'] ul").append(response);
            }
        }
    })
}

function GetPageData(departmentId, pageSize)
{
    $("#EmployeesList").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Employees/GetEmployeesByDepartmentId",
        type: "GET",
        data: {
            departmentId: departmentId,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (response)
        {
            $("#EmployeesList").append(response);
            var totalCount = $("#EmployeesList div input").val();

            if (totalCount > pageSize)
            {
                AddPaggin(totalCount);
            }
        }
    })
}

function AddPaggin(totalCount)
{
    var buttons = '';
    var totalPage = Math.ceil(totalCount / pageSize);
    var nextPage = (pageNumber < totalPage) ? pageNumber + 1 : pageNumber;
    var previosPage = (pageNumber > 1) ? pageNumber - 1 : pageNumber;

    buttons += '<li ><a  data-page=' + previosPage + ' href="#">&laquo;</a></li>';
    for (var i = 1; i <= totalPage; i++)
    {
        var classAtiv = (i == pageNumber) ? 'active' : '';
        buttons += '<li  class = ' + classAtiv + ' id = "selectedId" ><a data-page=' + i + ' href="#">' + i + '</a></li>';
    }
    buttons += ' <li ><a data-page=' + nextPage + ' href="#">&raquo;</a></li>';

    $("#paged").append(buttons);
}