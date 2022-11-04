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
    /// Interaction logic for Simulations.xaml
    /// </summary>
    public partial class Simulations : Page
    {

        int num_attack_dice = 1;
        int num_defense_dice = 1;

        int e_defense = 1;
        int e_attack = 9;
        int default_e_attack = 2;

        int p_hp = 5;
        int e_hp = 5;

        int num_player_wins = 0;
        int num_enemy_wins = 0;

        public Simulations()
        {
            InitializeComponent();
        }


        void Enagage()
        {
            Console.WriteLine("Engage!");
            PieChart.Series[0].Values.Clear();
            PieChart.Series[1].Values.Clear();
            num_player_wins = 0;
            num_enemy_wins = 0;


            SeriesCollection series = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<double> { 0}
                }
            };
            series.Add(new ColumnSeries
            {

                Values = new ChartValues<double> { 0 }
            });


            for (int i = 0; i < 100; i++)
            {
                p_hp = (int)slider_player_hp.Value;
                e_hp = (int)slider_enemy_hp.Value;

                e_attack = default_e_attack;

                int rnd = 0;
                while (p_hp > 0 && e_hp > 0)
                {
                    Attack_Round();
                    rnd++;
                }

                if (p_hp <= 0) { num_enemy_wins++; series[1].Values.Add((double)rnd); series[0].Values.Add((double)0); }
                else { num_player_wins++; series[1].Values.Add((double)0); series[0].Values.Add((double)rnd); }
            }

            Rounds_Chart.Series = series;

            Console.WriteLine("Player Wins: " + num_player_wins);
            Console.WriteLine("Enemy Wins: " + num_enemy_wins);

            PieChart.Series[0].Values.Add((double)num_player_wins);
            PieChart.Series[1].Values.Add((double)num_enemy_wins);
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Enagage();
        }

        private void slider_player_hp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            p_hp = (int)slider_player_hp.Value;
            txt_player_hp.Text = p_hp.ToString();
        }

        private void slider_enemy_hp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_hp = (int)slider_enemy_hp.Value;
            txt_enemy_hp.Text = e_hp.ToString();
        }

        private void slider_enemy_attack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_attack = (int)slider_enemy_attack.Value;
            txt_enemy_attack.Text = e_attack.ToString();
        }

        private void slider_enemy_defense_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            e_defense = (int)slider_enemy_defense.Value;
            txt_enemy_defense.Text = e_defense.ToString();
        }

        private void slider_player_attackdice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            num_attack_dice = (int)slider_player_attackdice.Value;
            txt_player_attackdice.Text = num_attack_dice.ToString();
        }

        private void slider_player_defensedice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            num_defense_dice = (int)slider_player_defensedice.Value;
            txt_player_defensedice.Text = num_defense_dice.ToString();
        }


       void Attack_Round() {

            //Report();

            (int,int) x = Combat_Engine.Roll_Attack(num_attack_dice, e_attack);
            int attack_value = x.Item1;
            e_attack = x.Item2;


            if ((attack_value - e_defense) > 0) { e_hp = e_hp - (attack_value - e_defense); }


            int defense_value = Combat_Engine.Roll_Defense(num_defense_dice);
                
            if (e_attack > defense_value) { p_hp = p_hp - Math.Abs(e_attack - defense_value); }


            //Report();
        }


         public void Report()
        {
            Console.WriteLine();
            Console.WriteLine(" Player hp: " + p_hp);
            Console.WriteLine(" Enemy hp: " + e_hp);
            Console.WriteLine(" Enemy Attack: " + e_attack);
            Console.WriteLine(" Enemy Defense: " + e_defense);
        }


         



    }
}
