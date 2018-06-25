$("input[type='submit']").on('click', function ()
{
    var form = $("#AjaxForm");
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

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
                    alert("Объект был успешно сохранен, нажмите ОК что бы прейти к списку");
                    window.location.href = form.attr("redirect");
                }
                else
                {
                    for (var i = 0; i < response.Exceptions.length; i++)
                    {
                        $('span[data-valmsg-for="' + response.Exceptions[i].MemberNames[0] + '"]').text(response.Exceptions[i].ErrorMessage);
                    }
                }
            }
        });
    }
})