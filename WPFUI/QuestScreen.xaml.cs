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
using ProjectMidTerm.Models.Creatures;
using ProjectMidTerm.EventArgs;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for QuestScreen.xaml
    /// </summary>
    public partial class QuestScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public PCharacter CurrentPlayer = GameSession.CurrentPlayer;

        public QuestScreen()
        {
            InitializeComponent();
        }
        
        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
