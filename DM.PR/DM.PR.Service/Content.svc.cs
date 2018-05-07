using System.Collections.Generic;

namespace DM.PR.Service
{
    public class Content : IContent
    {

        List<string> ads = new List<string>
        {
            "Hello1",
            "Hello2",
            "Hello3"

        };

        public IEnumerable<string> GetContent()
        {
            return ads;
        }
    }
}
