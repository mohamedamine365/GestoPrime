using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    public class Pointage
    {
        public string? UNITE_GESTIONNAIRE { get; set; }
        public string? Periode { get; set; }
        public string? Statut { get; set; }
        public string? MATCLE { get; set; }
        public string? NMPRES { get; set; }
        public double? Nombre_Jour_Traville { get; set; } // Attention à l'orthographe SQL : "Traville"
        public double? Nombre_Jour_Conge { get; set; }
        public double? Cof_PRES { get; set; }
    }
}