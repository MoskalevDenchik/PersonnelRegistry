$("input[type='submit']").on('click', function ()
{
    if ($("#StatusForm").valid())
    {
        event.preventDefault();

        $.ajax({
            type: "POST",
            async: false,
            url: "/WorkStatus/Save",
            dataType: 'JSON',
            data: $("#StatusForm").serialize(),
            success: function (response)
            {
                if (response.Status === 0)
                {
                    window.location.href = "/WorkStatus/Index";
                }
                else
                {
                    for (var i = 0; i < response.Exception.length; i++)
                    {
                        $('span[data-valmsg-for="' + response.Exception[i].MemberNames[0] + '"]').text(response.Exception[i].ErrorMessage);
                    }
                }
            }
        });
    }
})