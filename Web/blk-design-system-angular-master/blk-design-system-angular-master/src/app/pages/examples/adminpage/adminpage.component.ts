import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import { PeticionesService } from 'src/app/peticiones.service';

@Component({
  selector: 'app-adminpage',
  templateUrl: './adminpage.component.html',
  styleUrls: ['./adminpage.component.scss']
})
export class AdminpageComponent implements OnInit {

  isCollapsed = true;
  fases:Array<any>=['Trabajo preliminar','Cimientos', 'Paredes', 'Concreto reforzdo', 'Techos', 'Cielos', 'Repello','Entrepisos', 'Pisos', 'Enchapes'];
  focus;
  focus1;
  focus2;
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  etapa: any ={};
  material: any ={};
  add_etapa(){
    this.etapa.nombre = (<HTMLInputElement>document.getElementById("etapa_nombre")).value;
    this.etapa.descripcion = (<HTMLInputElement>document.getElementById("etapa_descripcion")).value;
    console.log(this.etapa);
      this.data.addEtapa(this.etapa).subscribe(
        res => {
          
          this.etapa= res;
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
  
  }
  add_material(){
    this.material.nombre = (<HTMLInputElement>document.getElementById("material_nombre")).value;
    this.material.precio_unitario = (<HTMLInputElement>document.getElementById("material_precio_unitario")).value;
    this.material.codigo = (<HTMLInputElement>document.getElementById("material_codigo")).value;
    console.log(this.material);
      this.data.addMaterial(this.material).subscribe(
        res => {
          
          this.etapa= res;
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
  
  }
  constructor(private data:PeticionesService) {}
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

}
