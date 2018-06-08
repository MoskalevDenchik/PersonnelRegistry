pageNumber = 1;
departmentId = 0;
pageSize = 5;

$(document).ready(function ()
{
    pageSize = $('#pageSize option:selected').html();
    GetPageData(departmentId, pageSize);
});

$("#menu li").on('click', function ()
{
    var curent = $(this).html();
    departmentId = $(curent).data("department");
    pageNumber = 1;
    GetPageData(departmentId, pageSize);
});

$('#paged').on('click', 'li', function ()
{
    var curent = $(this).html();
    pageNumber = $(curent).data("page");

    GetPageData(departmentId, pageSize);
})

$('#pageSize').on('change', function ()
{
    pageNumber = 1;
    pageSize = $('#pageSize option:selected').html();
    GetPageData(departmentId, pageSize);
})

function GetPageData(departmentId, pageSize)
{
    $("#EmployeesList").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Employees/GetPageEmployeesByDepartmentId",
        type: "GET",
        dataType: 'html',
        data: {
            departmentId: departmentId,
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (response)
        {
            $("#EmployeesList").append(response);
            AddPaggin($("#EmployeesList div input").val());
        }
    })
}

function AddPaggin(totalCount)
{
    var buttons = '';
    var pagaSize = $('#pageSize option:selected').html();
    var totalPage = Math.ceil(totalCount / pagaSize);
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