using System;       
using System.Web.Mvc;

namespace DM.PR.WEB.HtmlHelpers
{
    public static class ImageHalper
    {
        public static string ConvertByteArrayToString(this HtmlHelper html, byte[] array)
        {
            var base64 = Convert.ToBase64String(array);
            return $"data:image;base64,{base64}";
        }
    }
}