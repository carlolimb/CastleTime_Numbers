using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CastleTime_Numbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num_attack_dice = 1;
        int num_defense_dice = 1;

        int e_defense = 1;
        int e_attack = 2;

        int p_hp = 5;
        int e_hp = 5;

        int num_player_wins = 0;
        int num_enemy_wins = 0;

        public MainWindow()
        {
            InitializeComponent();

            Enagage();
        }


        void Enagage()
        {
            Console.WriteLine("Engage!");

            for (int i = 0; i < 100; i++)
            {
                p_hp = 5; e_hp = 5;


                while (p_hp > 0 && e_hp > 0)
                {
                    Attack_Round();
                }

                if (p_hp == 0) num_player_wins++;
                else num_enemy_wins++;

            }

            Console.WriteLine("Player Wins: " + num_player_wins);
            Console.WriteLine("Enemy Wins: " + num_enemy_wins);




        }

        void Attack_Round()
        {
            Report();
            e_attack = 2;
            int attack_value = Roll_Attack();
           

            if((attack_value - e_defense)>0) { e_hp = e_hp - (attack_value - e_defense); }


            int defense_value = Roll_Defense();
           
            if (( e_attack - defense_value) > 0) { p_hp = p_hp - (e_attack - defense_value); }

            Report();
        }



        public void Report()
        {
            Console.WriteLine();
            Console.WriteLine(" Player hp: " + p_hp);
            Console.WriteLine(" Enemy hp: " + e_hp);
        }


        public int Roll_Attack()
        {
            int attack_value = 0;
            for (int i = 0; i < num_attack_dice; i++)
            {
                int d = Roll_12();
                switch (d)
                {
                    case 1:
                        attack_value = attack_value + 3;
                        Console.WriteLine("Attack Die: 3");
                        break;
                    case 2:
                    case 3:
                    case 4:
                        attack_value = attack_value + 2; Console.WriteLine("Attack Die: 2"); break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        attack_value = attack_value + 1; Console.WriteLine("Attack Die: 1"); break;
                    case 12:
                        e_attack = e_attack + 2; break;
                }
            }

            return attack_value;

        }

        public int Roll_Defense()
        {
            int defense_value = 0;
            for (int i = 0; i < num_defense_dice; i++)
            {
                int d = Roll_12();
                switch (d)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        defense_value = defense_value + 2; Console.WriteLine("Defense Die: 2"); break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        defense_value = defense_value + 1; Console.WriteLine("Defense Die: 1"); break;
                }
            }
            return defense_value;
        }

        public int Roll_12()
        {
            return RandomNumber(1, 12);
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
