using System.Collections.Generic;

namespace DM.PR.WEB.Models.Employee
{
    public class AddPhoneViewModel
    {
        public int Number { get; set; }

        public IReadOnlyCollection<Common.Entities.KindPhone> KindList { get; set; }
    }
}