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
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.Models.Creatures;


namespace WPFUI
{
    /// <summary>
    /// Interaction logic for LootScreen.xaml
    /// </summary>
    public partial class LootScreen : Window
    {
        public LootScreen()
        {
            InitializeComponent();
        }
        public GameSession Session => DataContext as GameSession;
        public PCharacter CurrentPlayer = GameSession.CurrentPlayer;
        
        private void OnClick_Loot(object sender, RoutedEventArgs e)
        {
            Item item = ((FrameworkElement)sender).DataContext as Item;

            if (item != null)
            {
                App._gameSession.LootItem(item);
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
