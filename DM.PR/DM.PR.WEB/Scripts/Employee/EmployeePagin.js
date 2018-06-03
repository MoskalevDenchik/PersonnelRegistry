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

    for (var i = 0; i < data.Data.length; i++)
    {
        rowData += '<tr>'
            + '<td>' + data.Data[i].Id + '</td >'
            + '<td>' + data.Data[i].LastName + '</td>'
            + '<td>' + data.Data[i].FirstName + '</td >'
            + '<td>' + data.Data[i].MiddleName + '</td>'
            + '<td>' + data.Data[i].Department.Name + '</td>'
            + '<td>' + data.Data[i].Address + '</td>'
            + '<td>' + data.Data[i].MaritalStatus.Status + '</td>'
            + '</tr> ';
    }

    $("#tableData").append(rowData);
}

function AddPaggin(totalCount, currentPage)
{
    var pagin = '';
    var totalPage = Math.ceil(totalCount / 6);
    var nextPage = (currentPage < totalPage) ? currentPage + 1 : currentPage;
    var previosPage = (currentPage > 1) ? currentPage - 1 : currentPage;

    pagin += ' <li  onclick="GetPageData(' + previosPage + ',' + 6 + ')" ><a href="#">&laquo;</a></li>';
    for (var i = 1; i <= totalPage; i++)
    {
        var classAtiv = '';
        if (i == currentPage) { classAtiv = 'active'; }
        pagin += '<li onclick="GetPageData(' + i + ',' + 6 + ')" class = ' + classAtiv + ' id = "selectedId" ><a href="#">' + i + '</a></li>';
    }
    pagin += ' <li onclick="GetPageData(' + nextPage + ',' + 6 + ')"><a href="#">&raquo;</a></li>';

    $("#paged").append(pagin)
}
    
