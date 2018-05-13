using System.Collections.Generic;   

namespace DM.PR.Business.Providers
{
    public interface IAdProvider
    {
        IReadOnlyCollection<string> GetContent();
    }
}
