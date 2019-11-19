import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import { PeticionesService } from 'src/app/peticiones.service';
import {Router, ActivatedRoute} from '@angular/router';
@Component({
  selector: 'app-ing-arqpage',
  templateUrl: './ing-arqpage.component.html',
  styleUrls: ['./ing-arqpage.component.scss']
}) 
export class IngArqpageComponent implements OnInit {

  etapas:Array<any>=[];
  proyectos:Array<any>=[];
  isCollapsed = true;
  focus;
  focus1;
  focus2;
  trash:any = {};
  etapa:any = {};
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  tipo: any;
  constructor(private data:PeticionesService, private ruta:ActivatedRoute, ) {
    this.ruta.queryParams.subscribe(params =>{
      this.tipo= params['tipo'];
      console.log("EL TIPO ES" + this.tipo);
      this.getEtapas();
      this.getProyectos();
      }
      
   );
  }

  add_etapa(){
    this.etapa.nombre = (<HTMLInputElement>document.getElementById("etapa_nombre")).value;
    this.etapa.descripcion = (<HTMLInputElement>document.getElementById("etapa_descripcion")).value;
    (<HTMLInputElement>document.getElementById("etapa_descripcion")).value = "";
    (<HTMLInputElement>document.getElementById("etapa_nombre")).value = "";
    console.log(this.etapa);
      this.data.addEtapa(this.etapa).subscribe(
        res => {
          this.etapa= res;
          window.location.reload()
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
         
  }

  scrollToDownload(element: any) {
    element.scrollIntoView({ behavior: "smooth" });
  }

 

  ngOnInit() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("index-page");

    var slider = document.getElementById("sliderRegular");

    noUiSlider.create(slider, {
      start: 40,
      connect: false,
      range: {
        min: 0,
        max: 100
      }
    });

    var slider2 = document.getElementById("sliderDouble");

    noUiSlider.create(slider2, {
      start: [20, 60],
      connect: true,
      range: {
        min: 0,
        max: 100
      }
    });
  }
  ngOnDestroy() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.remove("index-page");
  }

  getEtapas(){
    this.data.getEtapa().subscribe(datos => {console.log(datos); this.etapas = datos});
    console.log(this.etapas);
    
  }

  getProyectos(){
    this.data.getProyecto().subscribe(datos => {console.log(datos); this.proyectos = datos});
    console.log(this.etapas);
    
  }

  etapaObraJSON(){

    var nombreP = (<HTMLInputElement>document.getElementById("proyecto_nombre")).value;
    var nombreE = (<HTMLInputElement>document.getElementById("etapa_anadir")).value;
    var fechaI = (<HTMLInputElement>document.getElementById("fecha_inicio")).value;
    var fechaF = (<HTMLInputElement>document.getElementById("fecha_fin")).value;
    var idP;
    var idE;
    
    for(var i = 0; i<this.proyectos.length; i++){
      if(this.proyectos[i]["nombre_obra"] == nombreP){
        idP = this.proyectos[i]["id"];
      }
    }

    for(var i = 0; i<this.etapas.length; i++){
      if(this.etapas[i]["nombre"] == nombreE){
        idE = this.etapas[i]["id"];
      }
    }

    var etapaobra ={};
    etapaobra["id_etapa"] = idE;
    etapaobra["id_obra"] = idP;
    etapaobra["fecha_inicio"] = fechaI;
    etapaobra["fecha_finalizacion"] = fechaF;
    console.log(etapaobra);
    this.addEtapaProyecto(etapaobra);
    
  }

  addEtapaProyecto(etapaObra:any){
    this.data.addEtapaObra(etapaObra).subscribe(
      res => {
        this.trash= res;
       },
       error => {
         console.error(error);
         alert(error.error);
       }
    );

    console.log(this.etapas);
  }

}
