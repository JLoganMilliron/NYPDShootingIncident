using System;

namespace NYPDShootingInc
{
    public static class staticTypes
    {

    }

    public static class staticHelpers
    {
        public static string getPercentageFromFloat(float num) {
            float multipliedNum = num*100;
            if (multipliedNum < 1) {
                return "<1%";
            }
            else {
                return Math.Round(multipliedNum) + "%";
            }
        }
    }
}