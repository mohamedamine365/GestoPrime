import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BoardService {
private readonly apiUrl = 'https://localhost:7079/api/Dashboard';

  constructor(private http: HttpClient) {}

  getStatsBySite(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/stats-by-site`);
  }

  getStatsByMonth(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/stats-by-month`);
  }

  getStatsByDay(startDate: string, endDate: string): Observable<any[]> {
    const params = new HttpParams()
      .set('startDate', startDate)
      .set('endDate', endDate);
    return this.http.get<any[]>(`${this.apiUrl}/stats-by-day`, { params });
  }

}
