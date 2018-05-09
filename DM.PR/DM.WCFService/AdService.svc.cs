using System.Collections.Generic;

namespace DM.WCFService
{
    public class AdService : IAdService
    {
        List<string> ads = new List<string>
        {
            "Hello1",
            "Hello2",
            "Hello3",
            "Hello4"
        };


        public IEnumerable<string> GetContent()
        {
            return ads;
        }
    }
}
