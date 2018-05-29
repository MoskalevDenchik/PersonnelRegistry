using DM.AdvertisingService.Business.Services;
using DM.AdvertisingService.Entities;

namespace DM.AdvertisingService.Contracts.Implement
{
    public class AdService : IAdService
    {
        private readonly BillBoardService _bBService;

        public AdService()
        {
            _bBService = new BillBoardService();
        }
        public BillBoard[] GetRandomBiilBoards()
        {
            return _bBService.GetRandomBillBoards().ToArray();
        }
    }
}