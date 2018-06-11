using System;

namespace DM.PR.Common.Helpers
{
    public static class Helper
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

        public static void ThrowExceptionIfZeroOrNegative(params int[] values)
        {
            foreach (var item in values)
            {
                if (item <= 0)
                {
                    throw new Exception("Неверное число!");
                }
            }
        }

    }
}
