using DM.AdvertisingService.Entities;
using System.ServiceModel;

namespace DM.AdvertisingService.Contracts
{                                                  
    [ServiceContract]
    public interface IAdService
    {
        [OperationContract]
        BillBoard[] GetRandomBiilBoards();
    }
}
