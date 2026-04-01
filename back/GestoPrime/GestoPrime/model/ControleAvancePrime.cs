namespace GestoPrime.model
{
    namespace GestoPrime.Models
    {
        public class ControleAvancePrime
        {
            // On respecte la casse SQL pour faciliter le mapping Entity Framework
            public string? Periode { get; set; }
            public string? MATRICULE { get; set; }
            public string? NOM_PRENOM { get; set; }
            public string? STATUT_SALARIE { get; set; }
            public decimal? PLAFOND { get; set; }
            public double? Nombre_Jour_Traville { get; set; }
            public double? Nombre_Jour_Conge { get; set; }
            public double? Nombre_Jour_Conge_Non_Paye { get; set; }
        }
    }
}
