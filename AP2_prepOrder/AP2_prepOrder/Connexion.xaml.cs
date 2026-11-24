using AP2_prepOrder.BDD;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AP2_prepOrder
{
    /// <summary>
    /// Interaction logic for Connexion.xaml
    /// </summary>
    public partial class Connexion : UserControl
    {
        private readonly BDD BDD = new();

        public Connexion()
        {
            InitializeComponent();
        }
        private async void btn_connexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await BDD.Authentification(tbIdentifiant.Text, tbMdp.Password);

                if (user is null)
                {
                    tbErreur.Visibility = Visibility.Visible;
                    tbErreur.Text = "Identifiant ou mot de passe incorrect.";
                    return;
                }

                //var pageConnexion = new Connexion();

                //MainWindow.MainContent.Content = pageConnexion;

                tbErreur.Visibility = Visibility.Visible;
                tbErreur.Text = "Authentification validée !";
            }
            catch (SqlException ex)
            {
                tbErreur.Visibility = Visibility.Visible;
                tbErreur.Text = $"Erreur SQL {ex.Number}";
            }
        }
    }
}
