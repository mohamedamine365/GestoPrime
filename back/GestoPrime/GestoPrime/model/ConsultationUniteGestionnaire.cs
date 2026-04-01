using System.ComponentModel.DataAnnotations.Schema;

namespace GestoPrime.model
{
    [Table("V_CONSULTATION_UNITE_GESTIONNAIRE")]
    public class ConsultationUniteGestionnaire
    {
        [Column("UNITE_GESTIONNAIRE")]
        public string? UniteGestionnaire { get; set; }

        [Column("CODE_UNITE")] // Notez le nom exact dans la capture SQL: CODE_UNIT ou CODE_UNITE
        public string? CodeUnite { get; set; }

        [Column("LIBELLE_UNITE")]
        public string? LibelleUnite { get; set; }
    }
}
