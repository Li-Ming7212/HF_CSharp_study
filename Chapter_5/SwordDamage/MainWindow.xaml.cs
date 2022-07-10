﻿using System;
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

namespace SwordDamage {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        Random random = new Random();
        SwordDamage_WPF swordDamage;
        public MainWindow() {
            InitializeComponent();
            swordDamage = new SwordDamage_WPF(random.Next(1, 7) + random.Next(1, 7)
                                          + random.Next(1, 7));
            DisplayDamage();
        }
        public void RollDice() {
            swordDamage.Roll = random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
            DisplayDamage();
        }
        void DisplayDamage() {
            damage.Text = $"Rolled {swordDamage.Roll} for {swordDamage.Damage} HP";
        }

        private void flaming_Checked(object sender, RoutedEventArgs e) {
            swordDamage.Flaming = true;
            DisplayDamage();
        }

        private void flaming_Unchecked(object sender, RoutedEventArgs e) {
            swordDamage.Flaming = false;
            DisplayDamage();
        }

        private void magic_Checked(object sender, RoutedEventArgs e) {
            swordDamage.Magic = true;
            DisplayDamage();
        }

        private void magic_Unchecked(object sender, RoutedEventArgs e) {
            swordDamage.Magic = false;
            DisplayDamage();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            RollDice();
        }
    }
}
