using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.Dtos
{
    public class ConsultationPlafondPrimeDto
    {
        public string? Periode { get; set; }
        public string? Statut { get; set; }
        public string? UniteGestionnaire { get; set; }
        public string? Matricule { get; set; }
        public string? NomPrenom { get; set; }
        public string? MatriculeResp { get; set; }
        public string? NomPrenomResp { get; set; }

        // Utilisez ces noms simples (sans [Column])
        public string? DateDebut { get; set; }
        public string? DateFin { get; set; }

        public string? CodeRubrique { get; set; }
        public decimal Montant { get; set; }
    }
}