import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccesService {
private apiUserUrl = 'https://localhost:7079/api/access';

  constructor(private http: HttpClient) {}

  // GET: Liste tous les accès
getUsers(filters: any): Observable<any> {
  let params = new HttpParams();
  
  if (filters.search) {
    params = params.set('search', filters.search);
  }
  
  params = params.set('page', filters.page.toString());
  params = params.set('pageSize', filters.pageSize.toString());

  return this.http.get<any>(this.apiUserUrl, { params });
}

 checkSalarie(matricule: string): Observable<any> {
  return this.http.get(`${this.apiUserUrl}/check/${matricule}`);
}

createAccess(userData: any): Observable<any> {
  return this.http.post(this.apiUserUrl, userData);
}

  deleteUser(matricule: string): Observable<any> {
  // On s'assure que le matricule est passé en paramètre de l'URL
  return this.http.delete<any>(`${this.apiUserUrl}/${matricule}`);
}
}
