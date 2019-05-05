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
using ProjectMidTerm.Models;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for CharacterCreator.xaml
    /// </summary>
    public partial class CharacterCreator : Window
    {
        public GameSession Session => DataContext as GameSession;

        public CharacterCreator()
        {
            InitializeComponent();
        }

        private void OnClick_StatChange(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Session.StatSelect(btn.Tag.ToString());
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            if (!Session.InvalidInput())
            {
                Session.CalculateRaceBonus();
                Close();
            }
            else
            {
                
            }
        }
        private void OnClick_Reset(object sender, RoutedEventArgs e)
        {
            Session.ResetValues();
        }
        private void OnClickReroll(object sender, RoutedEventArgs e)
        {
            Session.RerollStats();
        }
    }
}
