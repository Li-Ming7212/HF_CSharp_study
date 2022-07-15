using System;
using System.Collections.Generic;
using System.Text;

namespace JewelThief
{
    class Safe
    {
        private string contents = "precious jewels";
        private string safeCombination = "12345";//密码

        public string Open(string combination)
        {
            if (combination == safeCombination) return contents;
            return "";
        }

        public void PickLock(Locksmith lockpicker)
        {
            lockpicker.Combination = safeCombination;
        }
    }

}
