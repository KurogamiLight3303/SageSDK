using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.CIUtils
{
    public class IDHandler
    {
        public static bool IsNumeric(string id)
        {
            foreach(char i in id)
                if (!char.IsDigit(i))
                    return false;
            return true;
        }

        public static bool IsAlphaNumeric(string id)
        {
            foreach (char i in id)
                if (!(char.IsDigit(i) || char.IsLetter(i)))
                    return false;
            return true;
        }
    }
}
