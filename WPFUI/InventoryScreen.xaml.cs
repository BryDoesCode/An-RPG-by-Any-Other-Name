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
using ProjectMidTerm.Models;
using ProjectMidTerm.ViewModels;
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.Models.Creatures;
using ProjectMidTerm.EventArgs;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for InventoryScreen.xaml
    /// </summary>
    public partial class InventoryScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public PCharacter CurrentPlayer = GameSession.CurrentPlayer;

        public InventoryScreen()
        {
            InitializeComponent();
            App._gameSession.OnMessageRaised += OnGameMessageRaised;
        }

        private void OnClick_Use(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;

            if (item != null)
            {
                App._gameSession.UseItem(item.Item);                            
            }
        }

        private void OnClick_Drop(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;

            if (item != null)
            {
                App._gameSession.DropItem(item.Item);
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            ItemMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            ItemMessages.ScrollToEnd();
        }
    }
}
