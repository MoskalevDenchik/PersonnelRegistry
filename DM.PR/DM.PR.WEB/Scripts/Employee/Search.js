﻿pageNumber = 1;
pageSize = 5;

$("#Search").on('click', function ()
{
    GetPageData();
});

$('#pageSize').on('change', function ()
{
    pageSize = $('#pageSize option:selected').html();
    pageNumber = 1;
    GetPageData();
})

$('#paged').on('click', 'li', function ()
{
    var pageSize = $('#pageSize option:selected').html();
    var clicked = $(this).html();
    pageNumber = $(clicked).data('page');
    GetPageData(pageSize);
})

function GetPageData()
{
    $("#EmployeesList").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Employees/GetEmployees",
        type: "GET",
        data: {                                             
            firstName: $("input[id='FirstName']").val(),
            lastName: $("input[id='LastName']").val(),
            middleName: $("input[id='MiddleName']").val(),
            fromYear: $("input[id='FromYear']").val(),
            toYear: $("input[id='ToYear']").val(),
            WorkStatusId: $("select[id='WorkStatusId'] option:checked").val(),
            pageNumber: pageNumber,
            pageSize: pageSize

        },
        success: function (respons)
        {
            $('#EmployeesList').append(respons);
            var a = $("#EmployeesList div input").val();

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

