using System.Collections.Generic;
using System.ServiceModel;

namespace DM.WCFService
{
    [ServiceContract]
    public interface IAdService
    {

        [OperationContract]
        IEnumerable<string> GetContent();


    }

}
