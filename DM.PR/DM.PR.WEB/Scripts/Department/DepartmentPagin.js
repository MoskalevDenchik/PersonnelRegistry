pageNumber = 1;

$(document).ready(function ()
{
    GetPageData($('#pageSize option:selected').html());
});

$('#pageSize').on('change', function ()
{
    var pageSize = $('#pageSize option:selected').html();
    pageNumber = 1;
    GetPageData(pageSize);
})

$('#paged').on('click', 'li', function ()
{
    var pageSize = $('#pageSize option:selected').html();
    var clicked = $(this).html();
    pageNumber = $(clicked).data('page');
    GetPageData(pageSize);
})


function GetPageData(pageSize)
{
    $("#tableData").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Departments/GetAll",
        type: "GET",
        data: {
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        success: function (result)
        {
            AddRows(result);
            AddPaggin(result.TotalCount);
        }
    })
}

function AddRows(data)
{
    var rowData = "";

    for (let item of data.Data)
    {
        rowData += '<tr>'
            + '<td>' + item.Id + '</td >'
            + '<td>' + item.Name + '</td >'
            + '<td>' + item.Address + '</td>'
            + '<td>' + item.Description + '</td>'
            + '</tr> ';
    }

    $("#tableData").append(rowData);
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

