import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private readonly baseUrl = 'https://localhost:7079/api';
  private readonly apiUrl = `${this.baseUrl}/Access`;
  private readonly importApiUrl = `${this.baseUrl}/Import`;
  private readonly droitsApiUrl = `${this.baseUrl}/DroitsPrimes`;
  private readonly tauxApiUrl = `${this.baseUrl}/TauxPrimes`;
  private readonly periodeApiUrl = `${this.baseUrl}/Periode`;
  private readonly majParamApiUrl = `${this.baseUrl}/Majparametres`;
  private readonly PointageApiUrl = `${this.baseUrl}/Pointage`;
  private readonly salarieApiUrl = `${this.baseUrl}/Salarie`;

  constructor(private http: HttpClient) {}

  // --- GESTION DES ACCÈS ---
  getAccessList(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createAccess(userData: { matricule: string, privilege: string, etablissement: string }): Observable<any> {
    return this.http.post(this.apiUrl, userData);
  }

  deleteAccess(matricule: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${matricule}`);
  }

  // --- IMPORT EXCEL ---
  importSalaries(formData: FormData): Observable<any> {
    return this.http.post(`${this.importApiUrl}/import-salaries`, formData);
  }

  importScore(formData: FormData): Observable<any> {
    return this.http.post(`${this.importApiUrl}/import-score`, formData);
  }

  // --- GESTION DES DROITS PRIMES (T_PARAM_UNITE_GESTIONNAIRE) ---
  getDroitsPrimes(search?: string): Observable<any> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    return this.http.get<any>(this.droitsApiUrl, { params });
  }

  lookupByMatricule(matricule: string): Observable<any> {
    return this.http.get<any>(`${this.droitsApiUrl}/lookup/${matricule}`);
  }

  updateDroits(payload: any): Observable<any> {
    return this.http.post(`${this.droitsApiUrl}/update`, payload);
  }

  // --- GESTION DES TAUX MENSUELS ---
  getTauxPrimes(searchTerm?: string): Observable<any> {
    let params = new HttpParams();
    if (searchTerm) params = params.set('searchTerm', searchTerm);
    return this.http.get<any>(this.tauxApiUrl, { params });
  }

  updateTauxPrime(id: number, payload: any): Observable<any> {
    const body = {
      id: id,
      unite_Gestionnaire: payload.unite_Gestionnaire, 
      nbrJourOuv: payload.nbrJourOuv,
      coefHygiene: payload.coefHygiene,
      coefProd: payload.coefProd,
      periode: payload.periode
    };
    return this.http.put(`${this.tauxApiUrl}/${id}`, body);
  }

  // --- LANCER PÉRIODE ---
  lancerNouvellePeriode(periodeCode: string): Observable<any> {
    return this.http.post(`${this.periodeApiUrl}/lancer-periode`, { periodeCode });
  }

  getPeriodes(): Observable<any[]> {
    return this.http.get<any[]>(this.periodeApiUrl);
  }

  // --- MISE À JOUR PARAMÈTRES (PROCÉDURE STOCKÉE) ---
  /**
   * Appelle l'exécution de la procédure stockée PS_SIRH_MAJ_PARAMETRES
   */
  /**
   * Lance l'exécution de la procédure stockée via le Backend
   */
  executerMajParametres(): Observable<any> {
    return this.http.post<any>(`${this.majParamApiUrl}/executer`, {});
  }



 getPointages(unit?: string, matricule?: string): Observable<any[]> {
    let params = new HttpParams();
    if (unit) params = params.set('unit', unit);
    if (matricule) params = params.set('matricule', matricule);

    // CORRECTION : Utiliser PointageApiUrl et non apiUrl
    return this.http.get<any[]>(this.PointageApiUrl, { params }).pipe(
      map(response => {
        // Sécurité NG0900 : On force le retour d'un tableau
        if (Array.isArray(response)) return response;
        if (response && (response as any).data) return (response as any).data;
        return [];
      })
    );
  }

  getSalaries(search?: string): Observable<any[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);

    return this.http.get<any[]>(this.salarieApiUrl, { params }).pipe(
      map(response => Array.isArray(response) ? response : []),
      catchError(err => {
        console.error('Erreur Consultation Salarié:', err);
        return throwError(() => err);
      })
    );
  }

}