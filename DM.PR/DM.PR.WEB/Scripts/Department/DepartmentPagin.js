$(document).ready(function ()
{
    GetPageData(1, $('#pageSize option:selected').html());
});

function GetPageData(pageNum, pageSize)
{

    $("#tableData").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Departments/GetAll",
        type: "GET",
        data: { pageNumber: pageNum, pageSize: pageSize },
        success: function (result)
        {
            AddRows(result);
            AddPaggin(result.TotalCount, result.CurentPage);
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

function AddPaggin(totalCount, currentPage)
{
    var buttons = '';
    var pagaSize = $('#pageSize option:selected').html();
    var totalPage = Math.ceil(totalCount / pagaSize);
    var nextPage = (currentPage < totalPage) ? currentPage + 1 : currentPage;
    var previosPage = (currentPage > 1) ? currentPage - 1 : currentPage;

    buttons += ' <li  onclick="GetPageData(' + previosPage + ',' + pagaSize + ')" ><a href="#">&laquo;</a></li>';
    for (var i = 1; i <= totalPage; i++)
    {
        var classAtiv = (i == currentPage) ? 'active' : '';
        buttons += '<li onclick="GetPageData(' + i + ',' + pagaSize + ')" class = ' + classAtiv + ' id = "selectedId" ><a href="#">' + i + '</a></li>';
    }
    buttons += ' <li onclick="GetPageData(' + nextPage + ',' + pagaSize + ')"><a href="#">&raquo;</a></li>';

    $("#paged").append(buttons);
 
}

$('#pageSize').on('change', function ()
{
    var PageSize = $('#pageSize option:selected').html(); 

    GetPageData(1, PageSize);
})

