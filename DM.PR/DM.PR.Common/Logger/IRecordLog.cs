namespace DM.PR.Common.Logger
{
    public interface IRecordLog
    {
        void MakeInfo(string message);
        void MaleError(object ex);
    }
}
