using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GestoPrime.model
{
    [Keyless] // Indique que c'est une vue sans clé primaire
    public class ConsultationSalarie
    {
        public string? Periode { get; set; }
        public string? Statut { get; set; }

        [Column("UNITE_GETIONNAIRE")]
        public string? UniteGestionnaire { get; set; }

        [Column("MATRICULE")]
        public string? Matricule { get; set; }

        [Column("NOM_PRENOM")]
        public string? NomPrenom { get; set; }

        public string? Etablissement { get; set; }

        [Column("COD_UNITE")]
        public string? CodeUnite { get; set; }

        [Column("LIBELLE_UNITE")]
        public string? LibelleUnite { get; set; }

        [Column("MAT_RESP")]
        public string? MatriculeResponsable { get; set; }

        [Column("NOM_PRENOM_RESP")]
        public string? NomPrenomResponsable { get; set; }
    }
}