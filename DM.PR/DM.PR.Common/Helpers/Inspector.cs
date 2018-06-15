using System;

namespace DM.PR.Common.Helpers
{
    public static class Inspector
    {
        public static void ThrowExceptionIfNull(params object[] obj)
        {
            foreach (var item in obj)
            {
                if (item == null)
                {
                    throw new ArgumentNullException(item.GetType().ToString(), "Hello");
                }
            }
        }

    }
}
