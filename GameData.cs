using System;
using System.Collections.Generic;
using System.Text;

namespace HeadQuarters
{
    public class GameData
    {
        //Game name
        string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        //Player name
        string playerName;
        public string PlayerName
        {
            get
            {
                return playerName;
            }

            set
            {
                playerName = value;
            }
        }

        //Player level
        double level;
        public int xp;

        public double Level
        {
            get
            {
                level = xp / 625;
                return (int)Math.Floor(level);
            }

        }
        
        //Player money
        int money;
        public int Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
            }

        }

        //Player rank
        string[] ranks = new string[]
        {
            "Officer Cadet",
            "Warrant Officer 1",
            "Chief Warrant Officer 2",
            "Chief Warrant Officer 3",
            "Chief Warrant Officer 4",
            "Chief Warrant Officer 5",
            "Second Lieutenant",
            "First Lieutenant",
            "Captain",
            "Major",
            "Lieutenant Colonel",
            "Colonel",
            "Brigadier General",
            "Major General",
            "Lieutenant General",
            "General",
            "General of the Army"
        };
                
        public string Rank
        {
            get
            {
                return ranks[(int)level];
            }

        }

        //Player Divisions
        public List<Division> divisionList = new List<Division>();
    }

}
