using System.Text.Json.Serialization;

public class TauxPrimeDto
{
    public int Id { get; set; }

    [JsonPropertyName("unite_Gestionnaire")]
    public string? Unite_Gestionnaire { get; set; }

    [JsonPropertyName("nbrJourOuv")]
    public double NbrJourOuv { get; set; }

    [JsonPropertyName("coefHygiene")]
    public double CoefHygiene { get; set; }

    [JsonPropertyName("coefProd")]
    public double CoefProd { get; set; }

    [JsonPropertyName("periode")]
    public string? Periode { get; set; }
}