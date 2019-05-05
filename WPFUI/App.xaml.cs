using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProjectMidTerm.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static GameSession _gameSession = new GameSession();


        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;

            CharacterCreator characterCreator = new CharacterCreator();
            StatScreen statScreen = new StatScreen();
            mainWindow.DataContext = _gameSession; 
            characterCreator.DataContext = _gameSession;
            characterCreator.ShowDialog();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
