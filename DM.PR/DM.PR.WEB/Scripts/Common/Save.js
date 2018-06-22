$("input[type='submit']").on('click', function ()
{
    var form = $("#AjaxForm");

    if (form.valid())
    {
        event.preventDefault();

        $.ajax({
            type: form.attr("method"),
            async: false,
            url: form.attr("action"),
            dataType: 'JSON',
            data: form.serialize(),
            success: function (response)
            {
                if (response.Status === 0)
                {
                    window.location.href = form.attr("redirect");
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