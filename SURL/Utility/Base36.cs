using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SURL.Utility
{
    public class Base36
    {
        private const string Keys = "0123456789abcdefghijklmnopqrstuvwxyz";

        private const int Base = 36;

        public int From(string value)
        {
            value = value.ToLower();
            int result = 0;
            for (int i = value.Length - 1; i > -1; i--)
            {
                result *= Base;
                int index = Keys.IndexOf(value[i]);
                if (index == -1)
                {
                    throw new Exception("Link does not exist.");
                }
                result += index;
            }
            return result;
        }

        public string To(int value)
        {
            var sb = new StringBuilder();
            while (value > 0)
            {
                sb.Append(Keys[value % Base]);
                value /= Base;
            }
            return sb.ToString();
        }
    }
}