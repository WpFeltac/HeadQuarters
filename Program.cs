using System;
using System.Reflection;

namespace HeadQuarters
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("HeadQuarters v" + Assembly.GetEntryAssembly().GetName().Version.ToString());
        }
    }
}
