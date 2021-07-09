using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for analysis_page.xaml
    /// </summary>
    public partial class analysis_page : Page
    {
        int num_players = 1;
        int num_simulations = 1000;

        int num_attack_dice = 1;
        int num_defense_dice = 1;

        int e_defense = 1;
        int default_e_attack = 2;
        int e_attack = 9;

        int default_p_hp = 5;
        int p_hp = 5;
        int e_hp = 5;

        int e_kickback = 2;

        int num_player_wins = 0;
        int num_enemy_wins = 0;



        public analysis_page()
        {
            InitializeComponent();
        }

        public void Engage_Level_Analysis()
        {
            SeriesCollection series = new SeriesCollection
            {
                new ColumnSeries
                {

                    //Values = new ChartValues<double> { 0}
                }
            };


            SeriesCollection rnd_series = new SeriesCollection
            {
                new ColumnSeries
                {

                    //Values = new ChartValues<double> { 0}
                }
            };


            for (int lvl = 1; lvl < 7; lvl++)
            {
                num_player_wins = 0; num_enemy_wins = 0;
                int rnd = 0;
                switch (lvl)
                {
                    case 1:
                        default_p_hp = 2; num_attack_dice = 1; num_defense_dice = 1;
                        break;
                    case 2:
                        default_p_hp = 4; num_attack_dice = 1; num_defense_dice = 2;
                        break;
                    case 3:
                        default_p_hp = 6; num_attack_dice = 2; num_defense_dice = 2;
                        break;
                    case 4:
                        default_p_hp = 8; num_attack_dice = 2; num_defense_dice = 3;
                        break;
                    case 5:
                        default_p_hp = 10; num_attack_dice = 3; num_defense_dice = 3;
                        break;
                    case 6:
                        default_p_hp = 12; num_attack_dice = 4; num_defense_dice = 3;
                        break;
                }

                for (int i = 1; i < num_simulations; i++)
                {
                    e_hp = (int)slider_enemy_hp.Value;
                    default_e_attack = (int)slider_enemy_attack.Value;
                    p_hp = default_p_hp;
                    
                    while (p_hp > 0 && e_hp > 0)
                    {
                        Attack_Round();
                        rnd++;
                    }

                    if (p_hp <= 0) { num_enemy_wins++; Console.WriteLine("Player losses"); }
                    else { num_player_wins++; Console.WriteLine("Player wins"); }

                }
                double chance = (double)num_player_wins / (double)num_simulations;
                Console.WriteLine("Player level " + lvl + " Player wins: "+ num_player_wins + " Chance: " + chance);

               

                if (series[0].Values == null) series[0].Values = new ChartValues<double> { chance };

                else series[0].Values.Add(chance);

                if (rnd_series[0].Values == null) rnd_series[0].Values = new ChartValues<double> { rnd/num_simulations };

                else rnd_series[0].Values.Add((double)rnd / num_simulations);


            }

            Analz_Chart.Series = series;
            Rounds_Chart.Series = rnd_series;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Engage_Level_Analysis();
        }

        private void slider_enemy_hp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_hp = (int)slider_enemy_hp.Value;
            txt_enemy_hp.Text = e_hp.ToString();
        }

        private void slider_enemy_attack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            default_e_attack = (int)slider_enemy_attack.Value;
            txt_enemy_attack.Text = default_e_attack.ToString();
        }

        private void slider_enemy_defense_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_defense = (int)slider_enemy_defense.Value;
            txt_enemy_defense.Text = e_defense.ToString();
        }

        private void slider_enemy_kickback_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_kickback = (int)slider_enemy_kickback.Value;
            txt_enemy_kickback.Text = e_kickback.ToString();
        }

        private void slider_num_players_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            num_players = (int)slider_num_players.Value;
            txt_num_players.Text = num_players.ToString();
        }

        void Attack_Round() 
        {
            e_attack = default_e_attack;

            Report();

            int attack_value = Roll_Attack();


            if ((attack_value - e_defense) > 0) { e_hp = e_hp - (attack_value - e_defense); }

            int defense_value = Roll_Defense();

            if (e_attack > defense_value) { p_hp = p_hp - Math.Abs(e_attack - defense_value); }


            Report();
        }



         public void Report()
        {
            //Console.WriteLine();
            //Console.WriteLine(" Player hp: " + p_hp);
            //Console.WriteLine(" Enemy hp: " + e_hp);
            //Console.WriteLine(" Enemy Attack: " + e_attack);
            //Console.WriteLine(" Enemy Defense: " + e_defense);
        }


         public int Roll_Attack()
        {
            int attack_value = 0;
            for (int i = 0; i < num_attack_dice*num_players; i++)
            {
                int d = Roll_12();
                switch (d)
                {
                    case 1:
                    case 2:
                        attack_value = attack_value + 3;
                        //Console.WriteLine("Attack Die: 3");
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        attack_value = attack_value + 2;
                        //Console.WriteLine("Attack Die: 2"); 
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        attack_value = attack_value + 1; 
                        //Console.WriteLine("Attack Die: 1");
                        break;
                    case 12:
                        e_attack = e_attack + e_kickback; break;
                }
            }

            return attack_value;

        }

         public int Roll_Defense()
        {
            int defense_value = 0;
            for (int i = 0; i < num_defense_dice * num_players; i++)
            {
                int d = Roll_12();
                switch (d)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        defense_value = defense_value + 2;
                        //Console.WriteLine("Defense Die: 2"); 
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        defense_value = defense_value + 1; 
                        //Console.WriteLine("Defense Die: 1");
                        break;
                    case 12:
                        //Console.WriteLine("Defense Die: 0"); 
                        break;
                }
            }
            return defense_value;
        }

         public int Roll_12()
        {
            return RandomNumber(0, 13);
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
