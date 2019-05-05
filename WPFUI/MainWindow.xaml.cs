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
using ProjectMidTerm.ViewModels;
using ProjectMidTerm.Models;
using ProjectMidTerm.EventArgs;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameSession Session => DataContext as GameSession;

        public MainWindow()
        {
            InitializeComponent();
            App._gameSession.OnMessageRaised += OnGameMessageRaised;
            App._gameSession.RaiseGameStartMessage();
        }

        public void OnClick_DisplayInventoryScreen(object sender, RoutedEventArgs e)
        {
            InventoryScreen inventoryScreen = new InventoryScreen();
            inventoryScreen.Owner = this;
            inventoryScreen.DataContext = Session;
            inventoryScreen.ShowDialog();
        }

        public void OnClick_DisplayBattleScreen(object sender, RoutedEventArgs e)
        {
            
            BattleWindow battleScreen = new BattleWindow();
            battleScreen.Owner = this;
            battleScreen.DataContext = this.DataContext;
            battleScreen.ShowDialog();            
        }

        public void OnClick_DisplayStatScreen(object sender, RoutedEventArgs e)
        {
            StatScreen statscreen = new StatScreen();
            statscreen.Owner = this;
            statscreen.DataContext = Session;
            statscreen.ShowDialog();
        }

        public void OnClick_DisplayQuestScreen(object sender, RoutedEventArgs e)
        {
            QuestScreen questscreen = new QuestScreen();
            questscreen.Owner = this;
            questscreen.DataContext = Session;
            questscreen.ShowDialog();
        }

        public void OnClick_DisplayLootScreen(object sender, RoutedEventArgs e)
        {
            LootScreen lootscreen = new LootScreen();
            lootscreen.Owner = this;
            lootscreen.DataContext = Session;
            lootscreen.ShowDialog();
        }

        public void OnClick_Rest(object sender, RoutedEventArgs e)
        {
            Session.Rest();
        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            Session.MoveNorth();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            Session.MoveEast();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            Session.MoveSouth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            Session.MoveWest();
        }

        private void OnClick_TurnLeft(object sender, RoutedEventArgs e)
        {
            Session.TurnLeft();
        }

        private void OnClick_TurnRight(object sender, RoutedEventArgs e)
        {
            Session.TurnRight();
        }

        private void OnClick_MoveForward(object sender, RoutedEventArgs e)
        {
            Session.MoveForward();
        }

        private void Rectangle_MouseDown_VOID(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(-1, 0);
        }
        private void Rectangle_MouseDown_Forest(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(0, 0);
        }
        private void Rectangle_MouseDown_Inferno(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(1, -1);
        }
        private void Rectangle_MouseDown_Field(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(1, 0);
        }
        private void Rectangle_MouseDown_Lake(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(0, 1);
        }
        private void Rectangle_MouseDown_Horizon(object sender, RoutedEventArgs e)
        {
            Session.TeleportTo(1, 1);
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            WorldMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            WorldMessages.ScrollToEnd();
        }


    }
}
