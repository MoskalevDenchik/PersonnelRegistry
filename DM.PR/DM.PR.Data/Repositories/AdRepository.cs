using System.Collections.Generic;
using DM.PR.Data.AdServiceClient;
using DM.PR.Data.Intefaces;

namespace DM.PR.Data.Repositories
{
    public class AdRepository : IAdRepository
    {
        public IEnumerable<string> GetAll()
        {
            //AdServiceClient.AdServiceClient service = new AdServiceClient.AdServiceClient();
            return new List<string>
            {
                "Hello1",
                "Hello2"
            };

        }
    }
}
