using UnityEngine;

namespace DebugPrinter
{
    public class PrintDebug : MonoBehaviour
    {
        private static string m_divide = " : ";
        private static string m_seperator = "  |  ";

        public static void PrintInDebug(string WhatYouDebugging, string PrintThis)
        {
            Debug.Log(WhatYouDebugging + m_divide + PrintThis);
        }

        public static void PrintInDebug(string WhatYouDebugging, string[] PrintThis)
        {
            var stringToPrint = WhatYouDebugging + m_divide;
            foreach (var item in PrintThis)
            {
                stringToPrint = stringToPrint + item + m_seperator;
            }
            Debug.Log(stringToPrint);
        }
    }
}

