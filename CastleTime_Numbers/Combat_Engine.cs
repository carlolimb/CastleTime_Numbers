using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleTime_Numbers
{
    public static class Combat_Engine
    {
        public static (int, int) Roll_Attack(int num_attack_dice = 1, int e_attack = 1, int kick_value = 1)
        {
            int attack_value = 0; int d = 0;
            for (int i = 0; i < num_attack_dice; i++)
            {

                switch (Properties.Settings.Default.die)
                {
                    case 6:
                        d = Roll_6();
                        if (d <= Properties.Settings.Default.num_of_kicks)
                            //attack_value = attack_value + 3;
                            e_attack = e_attack + kick_value;
                        else
                        {
                            switch (d)
                            {
                                case 1:
                                case 2:
                                    attack_value = attack_value + 1; break;
                                case 3:
                                case 4:
                                case 5:
                                    attack_value = attack_value + 2; break;
                                case 6:
                                    attack_value = attack_value + 3; break;
                            }
                        }
                        break;
                    case 8:
                        d = Roll_8();
                        if (d <= Properties.Settings.Default.num_of_kicks)
                            //attack_value = attack_value + 3;
                            e_attack = e_attack + kick_value;
                        else
                        {
                            switch (d)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    attack_value = attack_value + 1; break;
                                case 5:
                                case 6:
                                case 7:
                                    attack_value = attack_value + 2; break;
                                case 8:
                                    attack_value = attack_value + 3; break;
                            }
                        }
                        break;

                    case 12:
                        d = Roll_12();
                        if (d <= Properties.Settings.Default.num_of_kicks)
                            //attack_value = attack_value + 3;
                            e_attack = e_attack + kick_value;
                        else
                        {
                            switch (d)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    attack_value = attack_value + 1; break;
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                    attack_value = attack_value + 2; break;
                                case 11:
                                case 12:
                                    attack_value = attack_value + 3; break;
                            }
                        }

                        break;
                }

            }

            return (attack_value, e_attack);
        }

        public static int Roll_Defense(int num_defense_dice = 1)
        {
            int defense_value = 0; int d = 0;
            for (int i = 0; i < num_defense_dice; i++)
            {
                switch (Properties.Settings.Default.die)
                {
                    case 6:
                        d = Roll_6();

                        switch (d)
                        {
                            case 1: break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                defense_value = defense_value + 1; break;
                            case 6:
                                defense_value = defense_value + 2; break;
                        }

                        break;
                    case 8:
                        d = Roll_8();

                        switch (d)
                        {
                            case 1:
                            case 2: break;
                            case 3:
                            case 4:
                            case 5:
                            case 6: defense_value = defense_value + 1; break;
                            case 7:
                            case 8: defense_value = defense_value + 2; break;
                        }

                        break;

                    case 12:
                        d = Roll_12();

                        switch (d)
                        {
                            case 1:
                            case 2:
                            case 3: break;
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10: defense_value = defense_value + 1; break;
                            case 11:
                            case 12: defense_value = defense_value + 2; break;
                        }


                        break;
                }

            }
            return defense_value;
        }
        public static int Roll_12()
        {
            return RandomNumber(0, 13);
        }
        public static int Roll_8()
        {
            return RandomNumber(0, 9);
        }
        public static int Roll_6()
        {
            return RandomNumber(0, 7);
        }

        //Function to get a random number 
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}

