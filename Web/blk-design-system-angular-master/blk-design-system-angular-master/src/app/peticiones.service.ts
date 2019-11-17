import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {headers: new HttpHeaders({
  'Content-Type': 'application/json'
})};


@Injectable({
  providedIn: 'root'
})
export class PeticionesService {

  ID= 'https://teconstruyeapi.azurewebsites.net/api'
 

  constructor(private _http: HttpClient) { }

  getEspecialidades(){
    
    
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/Especialidad');
  }
  addCliente(cliente:void){
    return this._http.post<any>(this.ID+'/Clientes',cliente)
 }
 addEtapa(etapa:void){
  return this._http.post<any>(this.ID+'/Etapas',etapa)
}


loginIngeniero(ingeniero:void){
  return this._http.post<any>('http://teconstruyeapi.azurewebsites.net/api/Ingenieros/login',ingeniero)
}
loginAdmin(admin:void){
  return this._http.post<any>(this.ID+'/Admin',admin)
}
loginArquitecto(arquitecto:void){
  return this._http.post<any>(this.ID+'/Arquitectos/login',arquitecto)
}

}