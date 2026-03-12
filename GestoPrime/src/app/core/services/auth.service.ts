import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  // Vérifie la présence du token
   isLoggedIn(){
    let token = localStorage.getItem('token');
    if(token){
      return true;
    }else{
      return false;
    }
  }

  // Décode le JWT pour récupérer les données (Payload)
  getDataFromToken() {
    let token = localStorage.getItem('token');
    if (token) {
      // Le JWT est composé de 3 parties séparées par des points. 
      // La 2ème partie [1] contient les données utilisateur encodées en Base64.
      try {
        return JSON.parse(window.atob(token.split('.')[1]));
      } catch (e) {
        console.error("Erreur lors du décodage du token", e);
        return null;
      }
    }
    return null;
  }

  
}