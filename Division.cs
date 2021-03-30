using System;
using System.Collections.Generic;
using System.Linq;
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
                return Number.ToString() + TypeName.Substring(0, 3).ToUpper();
            }
        }

        //Type
        //string is key, int is value (TODO : make the switch)
        Dictionary<string, int> types = new Dictionary<string, int>()
        {
            {"Infantry", 0},
            {"Armored", 1},
            {"Cavalry", 2},
            {"Mountain", 3},
            {"Airborne", 4},
            {"Artillery", 5},
            {"Security", 6},
        };

        int typeIndex;
        public string TypeName
        {
            get
            {
                return types.FirstOrDefault(x => x.Value == typeIndex).Key;
            }
        }

        public int TypeIndex
        {
            set
            {
                typeIndex = value;
            }
        }


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
        static Random r = new Random();
        int number = r.Next(999);
        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        //Veterancy
        double level;
        public int Level
        {
            get
            {
                level = combatXp / 1000;
                return (int)Math.Floor(level);
            }
        }

        //Xp
        int combatXp;
        public int CombatXp
        {
            get
            {
                return combatXp;
            }

            set
            {
                combatXp = value;
            }
        }

    }
}
