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

            SeriesCollection Power_series = new SeriesCollection
            {
                new ColumnSeries
                {

                    //Values = new ChartValues<double> { 0}
                }
            };


            for (int lvl = 1; lvl < 9; lvl++)
            {
                num_player_wins = 0; num_enemy_wins = 0;
                int rnd = 0;
                switch (lvl)
                {
                    case 1:
                        default_p_hp = Convert.ToInt16(tb_php1.Text); 
                        num_attack_dice = Convert.ToInt16(tb_patk1.Text); 
                        num_defense_dice = Convert.ToInt16(tb_pdef1.Text);
                        break;
                    case 2:
                        default_p_hp = Convert.ToInt16(tb_php2.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk2.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef2.Text);
                        break;
                    case 3:
                        default_p_hp = Convert.ToInt16(tb_php3.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk3.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef3.Text);
                        break;
                    case 4:
                        default_p_hp = Convert.ToInt16(tb_php4.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk4.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef4.Text);
                        break;
                    case 5:
                        default_p_hp = Convert.ToInt16(tb_php5.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk5.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef5.Text);
                        break;
                    case 6:
                        default_p_hp = Convert.ToInt16(tb_php6.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk6.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef6.Text);
                        break;
                    case 7:
                        default_p_hp = Convert.ToInt16(tb_php7.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk7.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef7.Text);
                        break;
                    case 8:
                        default_p_hp = Convert.ToInt16(tb_php8.Text);
                        num_attack_dice = Convert.ToInt16(tb_patk8.Text);
                        num_defense_dice = Convert.ToInt16(tb_pdef8.Text);
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
                Console.WriteLine("Player level+++ " + lvl + " Player wins: " + num_player_wins + " Chance: " + chance);

                if (series[0].Values == null) series[0].Values = new ChartValues<double> { chance };

                else series[0].Values.Add(chance);

                if (rnd_series[0].Values == null) rnd_series[0].Values = new ChartValues<double> { rnd / num_simulations };

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

            (int, int) x = Combat_Engine.Roll_Attack(num_attack_dice, e_attack,e_kickback);
            int attack_value = x.Item1;
            e_attack = x.Item2;


            if ((attack_value - e_defense) > 0) { e_hp = e_hp - (attack_value - e_defense); }

            int defense_value = Combat_Engine.Roll_Defense(num_defense_dice);

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

        private void week_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            if (e.Source.Equals(slider_week1_hp))
            {
                tb_php1.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week1_atk))
            {
                tb_patk1.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week1_def))
            {
                tb_pdef1.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week2_hp))
            {
                tb_php2.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week2_atk))
            {
                tb_patk2.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week2_def))
            {
                tb_pdef2.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week3_hp))
            {
                tb_php3.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week3_atk))
            {
                tb_patk3.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week3_def))
            {
                tb_pdef3.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week4_hp))
            {
                tb_php4.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week4_atk))
            {
                tb_patk4.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week4_def))
            {
                tb_pdef4.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week5_hp))
            {
                tb_php5.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week5_atk))
            {
                tb_patk5.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week5_def))
            {
                tb_pdef5.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week6_hp))
            {
                tb_php6.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week6_atk))
            {
                tb_patk6.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week6_def))
            {
                tb_pdef6.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week7_hp))
            {
                tb_php7.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week7_atk))
            {
                tb_patk7.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week7_def))
            {
                tb_pdef7.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week8_hp))
            {
                tb_php8.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week8_atk))
            {
                tb_patk8.Text = ((int)e.NewValue).ToString();

            }
            if (e.Source.Equals(slider_week8_def))
            {
                tb_pdef8.Text = ((int)e.NewValue).ToString();

            }

        }
    }
}
