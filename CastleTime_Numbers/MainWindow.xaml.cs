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
using LiveCharts;
using LiveCharts.Wpf;

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
        int e_attack = 9;

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

        void Attack_Round()
        {
            Report();
            e_attack = (int)slider_enemy_attack.Value;
            int attack_value = Roll_Attack();
           

            if((attack_value - e_defense)>0) { e_hp = e_hp - (attack_value - e_defense); }


            int defense_value = Roll_Defense();
           
            if ( e_attack > defense_value) { p_hp = p_hp - Math.Abs(e_attack - defense_value); }


            Report();
        }



        public void Report()
        {
            Console.WriteLine();
            Console.WriteLine(" Player hp: " + p_hp);
            Console.WriteLine(" Enemy hp: " + e_hp);
            Console.WriteLine(" Enemy Attack: " + e_attack);
            Console.WriteLine(" Enemy Defense: " + e_defense);
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
                    case 1:
                    case 2:
                        defense_value = defense_value + 2; Console.WriteLine("Defense Die: 2"); break;
                    case 3:
                    case 4: 
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        defense_value = defense_value + 1; Console.WriteLine("Defense Die: 1"); break;
                    case 10:
                    case 11:
                    case 12:
                         Console.WriteLine("Defense Die: 0"); break;
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
    }
}
