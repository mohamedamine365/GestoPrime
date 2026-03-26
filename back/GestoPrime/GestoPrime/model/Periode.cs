using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("T_INT_PERIODE")]
    public class Periode
    {
        [Key]
        public int id { get; set; }

        [Column("Periode")] // <--- Force le mapping vers la colonne 'Periode' de la DB
        public string Periode_Val { get; set; }

        public string Statut { get; set; }
        public string Utilisateur { get; set; }
        public DateTime Date_Mvt { get; set; }
    }
}