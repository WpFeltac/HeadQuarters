using System;
using System.Collections.Generic;
using System.Text;

namespace HeadQuarters
{
    public class Division
    {
        //Id
        public string ID
        {
            get
            {
                return Number.ToString() + Type.Substring(0, 3);
            }
        }
        //Type
        static string[] types = new string[]
        {
            "Infantry",//0
            "Armored",//1
            "Cavalry",//2
            "Mountain",//3
            "Airborne",//4
            "Artillery",//5
            "Security"//6
        };
        public int typeIndex { get; set; }
        public string Type { get { return types[typeIndex]; } }

        //Ordinal (st, nd, rd, th)
        public string Ordinal
        {
            get
            {
                int last = Number % 10;
                switch (last)
                {
                    case 1:
                        return "st";
                    case 2:
                        return "nd";
                    case 3:
                        return "rd";
                    default:
                        return "th";
                }

            }
        }

        //Number
        public int Number { get; set; }
         
        public Division()
        {
            Random r = new Random();
            Number = r.Next(999);                      
        }

        //Veterancy

    }
}
