using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    public class Pointage
    {
        [Column("UNITE_GESTIONNAIRE")]
        public string UNITE_GESTIONNAIRE { get; set; }

        public string Periode { get; set; }
        public string Statut { get; set; }
        public string MATCLE { get; set; }
        public string NMPRES { get; set; }

        // ✅ CORRECTION ICI : "Traville" (comme dans ton image)
        [Column("Nombre_Jour_Traville")]
        public decimal Nombre_Jour_Travaille { get; set; }

        [Column("Nombre_Jour_Conge")]
        public decimal Nombre_Jour_Conge { get; set; }

        [Column("Cof_PRES")]
        public decimal Cof_PRES { get; set; }
    }
}