using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Test.Classes
{
    class PasswordChecker
    {
        private string[] patterns;
        private int minLen;

        public PasswordChecker(int minLen, params string[] patterns)
        {
            this.patterns = patterns;
            this.minLen = minLen;
        }

        public bool Check(string password)
        {
            if (password.Length < minLen)
                return false;

            foreach (var pattern in patterns)
            {
                if (!Regex.IsMatch(password, pattern))
                    return false;
            }
            return true;
        }
    }
}
