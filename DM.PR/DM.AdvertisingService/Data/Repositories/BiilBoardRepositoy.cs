using DM.AdvertisingService.Entities;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace DM.AdvertisingService.Data.Repositories
{
    internal class BiilBoardRepositoy
    {
        private List<BillBoard> _bBList;

        public BiilBoardRepositoy()
        {
            _bBList = new List<BillBoard>
            {
                 new BillBoard{ Id = 0, Name = "1XBET.COM", Image = ParseImage("~/Content/Images/XBET.jpg"), Link = "https://1xbet.com/casino/", Description = "Ставки на спорт онлайн"},
                 new BillBoard{ Id = 1, Name = "TUT.BY", Image = ParseImage("~/Content/Images/TUTBY.jpg"), Link = "https://www.tut.by/", Description = "Белорусский новостной партал"},
                 new BillBoard{ Id = 2, Name = "BELMARKET.BY", Image = ParseImage("~/Content/Images/BelMarket.jpg"), Link = "http://www.bel-market.by/", Description = "Сеть универсальных магазинов"},
                 new BillBoard{ Id = 3, Name = "KUFAR.BY", Image = ParseImage("~/Content/Images/kufar.jpg"), Link = "https://www.kufar.by/", Description = "Крупнейшая площадка объявлений"},
                 new BillBoard{ Id = 4, Name = "MTS.BY", Image = ParseImage("~/Content/Images/Mts.jpg"), Link = "https://www.mts.by/", Description = "Крупнейший мобильный оператор"},
                 new BillBoard{ Id = 5, Name = "BELARUSBANK.BY", Image = ParseImage("~/Content/Images/belarusBank.jpg"), Link = "https://belarusbank.by/", Description = "Крупнейший банк Беларуси"}
            };
        }

        public List<BillBoard> GetAll()
        {
            return _bBList;
        }

        #region Helpers

        private byte[] ParseImage(string path)
        {
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            return File.ReadAllBytes(physicalPath);
        }

        #endregion

    }
}