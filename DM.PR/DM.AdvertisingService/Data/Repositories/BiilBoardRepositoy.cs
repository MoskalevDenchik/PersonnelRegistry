using DM.AdvertisingService.Entities;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DM.AdvertisingService.Data.Repositories
{
    internal class BiilBoardRepositoy
    {
        private List<BillBoard> _bBList;

        public BiilBoardRepositoy()
        {
            _bBList = new List<BillBoard>
            {
                 new BillBoard{ Id = 0, Name = "1XBET.COM", Image = ParseImage("~/Content/Images/XBET.jpg"), Link = "https://1xbet.com/casino/", Description = "Ставки на спорт онлайн" },
                 new BillBoard{ Id = 1, Name = "TUT.BY", Image = ParseImage("~/Content/Images/TUTBY.jpg"), Link = "https://www.tut.by/", Description = "Белорусский новостной партал" }
            //     new BillBoard{ Id = 2, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" },
            //     new BillBoard{ Id = 3, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" },
            //     new BillBoard{ Id = 4, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" } ,
            //     new BillBoard{ Id = 5, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" },
            //     new BillBoard{ Id = 6, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" },
            //     new BillBoard{ Id = 7, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" },
            //     new BillBoard{ Id = 8, Name = "", Image = ParseImage("~/Content/Images/"), Link = "", Description = "" }
            };
        }

        public List<BillBoard> GetAll()
        {
            return _bBList;
        }

        private byte[] ParseImage(string path)
        {
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            return File.ReadAllBytes(physicalPath);
        }
    }
}