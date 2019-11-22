import { PeticionesService } from 'src/app/peticiones.service';
import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import {Router, ActivatedRoute} from '@angular/router';


@Component({
  selector: 'app-loginpage',
  templateUrl: './loginpage.component.html',
  styleUrls: ['./loginpage.component.scss']
})
export class LoginpageComponent implements OnInit {
  isCollapsed = true;
  focus;
  focus1;
  focus2;
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  obj: any ={};
  compra: any ={};
  paquete: Array<any>=[];
  materiales: Array<any>=[];
  inmuebles: Array<any> = [

    {Tipo: 'Lote', cedulaAdmin:123},
    {Tipo: "Casa", cedulaAdmin:123},
    {Tipo: "Apartamento", cedulaAdmin:123},
   
  ];
  image: string | ArrayBuffer;
  constructor(private data:PeticionesService, private router:Router) {
    this.get_materiales();

  }
  scrollToDownload(element: any) {
    element.scrollIntoView({ behavior: "smooth" });
  }
  ngOnInit() {
    this.get_materiales();
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

     agregar(Tipo1:any){
      var cant = (<HTMLInputElement>document.getElementById('inp1')).value;
  
    for (var indice = 0; indice < this.inmuebles.length; indice++){
      if(this.inmuebles[indice].Tipo == Tipo1){
        this.inmuebles.splice(indice, 1);
        this.obj.nombre = this.inmuebles[indice].Tipo;
        this.obj.cantidad= cant;
        this.paquete.push(this.obj);
        this.obj={};
      }
    }
  }

  agregar_gastos(){
    this.compra.foto= this.image;
    this.compra.proveedor= (<HTMLInputElement>document.getElementById("proveedor")).value;
    this.compra.numero_factura=(<HTMLInputElement>document.getElementById("numero_factura")).value;
    this.compra.materiales= this.paquete; 
    console.log(this.compra);
  }

  changeListener($event) : void {
    this.readThis($event.target);
  }
  
  readThis(inputValue: any): void {
    var file:File = inputValue.files[0];
    var myReader:FileReader = new FileReader();

    myReader.onloadend = (e) => {
      this.image = myReader.result;
   
    }
    myReader.readAsDataURL(file);
  }

  get_materiales(){
 
    this.data.getMateriales().subscribe(datos => this.materiales= datos);
    console.log( this.materiales);
  
  }
  
}
