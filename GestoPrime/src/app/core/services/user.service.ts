import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  // Ajuste l'URL selon ton environnement (7079 pour .NET)
  private url = 'https://localhost:7079/api/Auth/login'; 

  constructor(private http: HttpClient) {}


  // Connexion
  signin(data: any) {
    return this.http.post(this.url , data);
  }



 
}