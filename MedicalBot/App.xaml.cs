using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MedicalBot.DialogManager;
using MedicalBot.InputProcessingEngine;
using MedicalBot.UI;

namespace MedicalBot
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            App app = new App();
            CSession session = new CSession();
            CBotClientManager botClientManager = new CBotClientManager(session);
            CDialogManager dialogManager = new CDialogManager(botClientManager);
            CMainWindowViewModel viewModel = new CMainWindowViewModel(dialogManager);
            MainWindow window = new MainWindow();
            window.DataContext = viewModel;
            app.Run(window);
        }
    }
}
