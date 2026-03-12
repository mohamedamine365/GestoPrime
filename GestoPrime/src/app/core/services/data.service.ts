import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
private readonly apiUrl = 'https://localhost:7079/api/Access';

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
}
