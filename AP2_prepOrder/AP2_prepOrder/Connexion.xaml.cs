using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

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
                else
                {
                    if (user.Role == "Responsable")
                    {
                        tbErreur.Visibility = Visibility.Visible;
                        tbErreur.Text = "Authentification validée ! Mode responsable";
                        return;
                    }
                    if (user.Role == "Préparateur")
                    {
                        tbErreur.Visibility = Visibility.Visible;
                        tbErreur.Text = "Authentification validée ! Mode préparateur";
                        return;
                    }
                    if (user.Role == "Cariste")
                    {
                        tbErreur.Visibility = Visibility.Visible;
                        tbErreur.Text = "Authentification validée ! Mode cariste";
                        return;
                    }
                }
            }
            catch (SqlException ex)
            {
                tbErreur.Visibility = Visibility.Visible;
                tbErreur.Text = $"Erreur SQL {ex.Number}";
            }
        }
    }
}
