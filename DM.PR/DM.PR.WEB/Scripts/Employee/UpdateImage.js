$("#UploadImg").on('click', function ()
{
    UnloadImage();
})


function UnloadImage()
{
    var data = new FormData();
    var files = $("#imageBrowes").get(0).files;
    if (files.length > 0)
    {
        data.append("imageBrowes", files[0]);
    }

    $.ajax({
        url: "/Employees/AddImage",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response)
        {
            $("#UserImage").attr("src", response.imagePath);
            var a = $("input[type=hidden]:last").val();
            $("input[type=hidden]:last").val(response.imagePath);
        }
    });
}