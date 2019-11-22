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
  getPresupuesto(Obj:any){
    return this._http.get<void[]>(this.ID+'/Proyecto/Presupuesto/'+ Obj.id_obra);
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
addCompra(cliente:void){
  return this._http.post<any>(this.ID+'/Empleados',cliente)
}
 addEtapa(etapa:void){
  return this._http.post<any>(this.ID+'/Etapas',etapa)
  }
  addMaterial(material:void){
    return this._http.post<any>(this.ID+'/Material',material)
    }

  addPProyecto(obj:void){
      return this._http.post<any>(this.ID+'/Material',obj)
      }
  addPemp(obj:void){
      return this._http.post<any>(this.ID+'/Proyecto/asignarhoras',obj)
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

  getObras(){
    return this._http.get<void[]>(this.ID + '/Obras');
  }
  getEmpleados(){
    return this._http.get<void[]>(this.ID + '/Empleados');
  }
  getProyecto(proyecto:void){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/obras');
  }

  addEtapaObra(etapa_obra:any){
    return this._http.put<any>("http://teconstruyeapi.azurewebsites.net/api/Proyecto/asignaretapa", etapa_obra,httpOptions);
  }

  etapaProyecto(idP:any){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/Proyecto/etapas/' + idP);
    
  }

  getMateriales(){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/Material');
  }


  addMatEtapa(matEtapa:any){
    return this._http.put<any>("http://teconstruyeapi.azurewebsites.net/api/Proyecto/asignarmaterial", matEtapa,httpOptions);
  }

  getPresupuestoEtapas(){
    return this._http.get<void[]>('http://teconstruyeapi.azurewebsites.net/api/Proyecto/Presupuesto');
  }

  getPlanillas(){
    return this._http.get<void[]>("http://teconstruyeapi.azurewebsites.net/api/Reporte/Planilla");
  }
}
