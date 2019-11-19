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
 addArquitecto(cliente:void){
  return this._http.post<any>(this.ID+'/Arquitectos',cliente)
}
addIngeniero(cliente:void){
  return this._http.post<any>(this.ID+'/Ingenieros',cliente)
}
addEmpleado(cliente:void){
  return this._http.post<any>(this.ID+'/Empleados',cliente)
}
 addEtapa(etapa:void){
  return this._http.post<any>(this.ID+'/Etapas',etapa)
  }
  addMaterial(material:void){
    return this._http.post<any>(this.ID+'/Material',material)
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

  getEtapa(etapa:void){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/Etapas');
  }

  getProyecto(proyecto:void){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/obras');
  }

  addEtapaObra(etapa_obra:any){
    return this._http.put<any>("http://teconstruyeapi.azurewebsites.net/api/Proyecto/asignaretapa", etapa_obra,httpOptions);
  }
}
