$(document).ready(function ()
{
    GetPageData(1, 6);
});

function GetPageData(pageNum, pageSize)
{

    $("#tableData").empty();
    $("#paged").empty();

    $.ajax({
        url: "/Employees/GetAll",
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
            + '<td>' + item.LastName + '</td>'
            + '<td>' + item.FirstName + '</td >'
            + '<td>' + item.MiddleName + '</td>'
            + '<td>' + item.Department.Name + '</td>'
            + '<td>' + item.Address + '</td>'
            + '<td>' + item.MaritalStatus.Status + '</td>'
            + '</tr> ';
    }

    $("#tableData").append(rowData);
}

function AddPaggin(totalCount, currentPage)
{
    var buttons = '';
    var pagaSize = 6; // привязать к selectList
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

