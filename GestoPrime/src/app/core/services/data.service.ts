import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private readonly apiUrl = 'https://localhost:7079/api/Access';
  private readonly importApiUrl = 'https://localhost:7079/api/Import';
  private readonly droitsApiUrl = 'https://localhost:7079/api/DroitsPrimes';
  

  constructor(private http: HttpClient) {}

  // Récupère la liste de la vue V_SIRH_Param_Login
  getAccessList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Crée l'accès dans T_SIRH_USER
  // Note: Le backend forcera l'identifiant et le mpt sur le matricule
  createAccess(userData: { matricule: string, privilege: string, etablissement: string }): Observable<any> {
    return this.http.post(this.apiUrl, userData);
  }

  deleteAccess(matricule: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${matricule}`);
  }



importSalaries(formData: FormData) {
    return this.http.post(`${this.importApiUrl}/import-salaries`, formData);
  }

importScore(formData: FormData) {
  // Assurez-vous que l'URL correspond exactement à la route du contrôleur
  return this.http.post(`${this.importApiUrl}/import-score`, formData);
}



// --- NOUVEAU : Gestion des Droits Primes ---

  /** * Récupère la liste depuis la Vue V_CONSULTATION_DROITS_PRIMES
   * @param search Optionnel : filtre par unité gestionnaire
   */
  getDroitsPrimes(search?: string): Observable<any> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    
    return this.http.get<any>(this.droitsApiUrl, { params });
  }

  /**
   * Recherche automatique des infos par matricule (Lookup)
   */
  lookupByMatricule(matricule: string): Observable<any> {
    return this.http.get<any>(`${this.droitsApiUrl}/lookup/${matricule}`);
  }

  /**
   * Met à jour uniquement Droit_Hygiene et Droit_Prod dans T_EXP_UO_GESTIONNAIRE
   */
  updateDroits(payload: any): Observable<any> {
    return this.http.post(`${this.droitsApiUrl}/update`, payload);
  }


}
