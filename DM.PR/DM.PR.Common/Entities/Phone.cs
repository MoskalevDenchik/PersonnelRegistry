namespace DM.PR.Common.Entities
{
    public class Phone : IEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public KindPhone Kind { get; set; }
    }
}
