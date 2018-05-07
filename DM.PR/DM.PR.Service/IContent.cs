using System.Collections.Generic;     
using System.ServiceModel;

namespace DM.PR.Service
{
    [ServiceContract]
    public interface IContent
    {

        [OperationContract]
        IEnumerable<string> GetContent();
                                
    }

     
}
