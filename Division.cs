using System;
using System.Collections.Generic;
using System.Text;

namespace HeadQuarters
{
    public class Division
    {
        //Type
        string[] types = new string[]
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
        public string Ordinal { get; }
        
        //Number
        public int Number { get; set; }

        public Division()
        {
            Random r = new Random();
            Number = r.Next(999);

            int last = Number % 10;
            switch (last)
            {
                case 1:
                    Ordinal = "st";
                    break;
                case 2:
                    Ordinal = "nd";
                    break;
                case 3:
                    Ordinal = "rd";
                    break;
                default:
                    Ordinal = "th";
                    break;
            }
        }
    }
}
