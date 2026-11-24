using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

public record UtilisateurConnecte(int Id, string Prenom, string Nom, string Role);

namespace AP2_prepOrder
{
    internal class BDD
    {
        private const string ConnectionString =
        @"Server=localhost;Database=preporder;Trusted_Connection=True;TrustServerCertificate=True;";

        public async Task<UtilisateurConnecte?> Authentification(string identifiant, string motDePasse)
        {
            const string sql = @"SELECT Id, prenom, nom, role, motdepasse
                             FROM Utilisateur
                             WHERE identifiant = @identifiant";

            await using var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@identifiant", identifiant);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null; // identifiant inconnu

            string hash = reader.GetString(reader.GetOrdinal("motdepasse"));
            if (!BCrypt.Net.BCrypt.Verify(motDePasse, hash))
                return null; // mauvais mot de passe

            return new UtilisateurConnecte(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("prenom")),
                reader.GetString(reader.GetOrdinal("nom")),
                reader.GetString(reader.GetOrdinal("role")));
        }
    }
}
