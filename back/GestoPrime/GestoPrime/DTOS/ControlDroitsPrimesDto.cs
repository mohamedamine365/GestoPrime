using System.ComponentModel.DataAnnotations;

namespace GestoPrime.DTOS
{
    public class ControlDroitsPrimesDto
        {
            public string Unite_Gestionnaire { get; set; }
            public string MAT_RESP { get; set; }
            public bool Droit_Hygiene { get; set; }
            public bool Droit_Prod { get; set; }
        }
    
}
