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
  isCollapsed = true;
  focus;
  focus1;
  focus2;
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


}
