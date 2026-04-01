namespace GestoPrime.model
{
    public class ControlePointage
    {
        public string? PERIODE { get; set; }
        public string? STATUT { get; set; }
        public string? UNITE_GESTIONNAIRE { get; set; }
        public string? MATCLE { get; set; } // Matricule dans SQL
        public string? NMPRES { get; set; } // Nom Prénom dans SQL
        public double? Nombre_Jour_Traville { get; set; }
        public double? Nombre_Jour_Conge { get; set; }

        public double? Cof_PRES { get; set; } 
    }
}
