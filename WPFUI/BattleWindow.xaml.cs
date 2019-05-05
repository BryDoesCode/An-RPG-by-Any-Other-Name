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
using System.Windows.Shapes;
using ProjectMidTerm.ViewModels;
using ProjectMidTerm.EventArgs;
using ProjectMidTerm.Models.Creatures;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        public GameSession Session => DataContext as GameSession;

        public BattleWindow()
        {
            InitializeComponent();
            App._gameSession.OnMessageRaised += OnGameMessageRaised;
            App._gameSession.CreatureAppeared();
            //App._gameSession.SparringMatch();
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            App._gameSession.AttackEnabled = true;
            App._gameSession.CheckDeath();
            Close();            
        }

        private void OnClick_Attack(object sender, RoutedEventArgs e)
        {
            App._gameSession.AttackCurrentMonster();
        }

        private void OnClick_Defend(object sender, RoutedEventArgs e)
        {
            App._gameSession.DefendAgainstCurrentMonster();
        }

        public void OnClick_DisplayInventoryScreen(object sender, RoutedEventArgs e)
        {
            InventoryScreen inventoryScreen = new InventoryScreen();
            inventoryScreen.Owner = this;
            inventoryScreen.DataContext = this.DataContext;
            inventoryScreen.ShowDialog();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
    }
}
