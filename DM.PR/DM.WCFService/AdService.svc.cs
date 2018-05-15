using System.Collections.Generic;

namespace DM.WCFService
{
    public class AdService : IAdService
    {
        List<string> ads = new List<string>
        {
            "Реклама 1",
            "Реклама 2",
            "Реклама 3",
            "Реклама 4",
            "Реклама 5",
            "Реклама 6",
            "Реклама 7"
        };                            

        public IEnumerable<string> GetContent()
        {
            return ads;
        }
    }
}
