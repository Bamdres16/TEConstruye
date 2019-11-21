import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import { PeticionesService } from 'src/app/peticiones.service';
import {Router, ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-adminpage',
  templateUrl: './adminpage.component.html',
  styleUrls: ['./adminpage.component.scss']
})
export class AdminpageComponent implements OnInit {

  isCollapsed = true;
  fases:Array<any>=['Trabajo preliminar','Cimientos', 'Paredes', 'Concreto reforzdo', 'Techos', 'Cielos', 'Repello','Entrepisos', 'Pisos', 'Enchapes'];
  focus;
  editField: string;
  campo: string;
  materiales: Array<any>;
  public valorBusqueda:string;
  inmuebles: Array<any> = [

    {Tipo: 'Lote', cedulaAdmin:123},
    {Tipo: "Casa", cedulaAdmin:123},
    {Tipo: "Apartamento", cedulaAdmin:123},
   
  ];
  inmuebleNuevo:Array<any>= [{Tipo:'Default', cedulaAdmin: 0}];
  focus1;

  focus2;
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  etapa: any ={};
  publicar: any ={};
  material: any ={};
  pubProyecto: any ={};
  obras:Array<any>;
  empleados:Array<any>;
  presupuesto: any ={};
  presupuesto2: any ={};
  pres:Array<any>;
  num:number;
  obj: any ={};
  compra: any ={};
  paquete: Array<any>=[]; 
  image: string | ArrayBuffer;
 
  get_obras(){
 
    this.data.getObras().subscribe(datos => this.obras= datos);

}
get_empleados(){
 
  this.data.getEmpleados().subscribe(datos => this.empleados= datos);
  console.log( this.empleados);

}

get_presupuesto(){
  var proyecto = (<HTMLInputElement>document.getElementById("proy4")).value;
  for(var i = 0; i<this.obras.length; i++){
    if(this.obras[i]["nombre_obra"] == proyecto){
      this.presupuesto.id_obra = this.obras[i]["id"]*1;
    }
  }
  this.data.getPresupuesto(this.presupuesto).subscribe(datos => this.presupuesto2= datos);
  console.log( this.presupuesto2.Total);
  document.getElementById("label1").innerHTML=this.presupuesto2.Total;
 

}
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
  add_empProy(){
    var proyecto = (<HTMLInputElement>document.getElementById("proy1")).value;
    var empleado = (<HTMLInputElement>document.getElementById("emp1")).value;
    this.pubProyecto.horas_laboradas=(<HTMLInputElement>document.getElementById("horas3")).valueAsNumber;
    this.pubProyecto.semana=(<HTMLInputElement>document.getElementById("semanas3")).valueAsNumber;
    for(var i = 0; i<this.obras.length; i++){
      if(this.obras[i]["nombre_obra"] == proyecto){
       this.pubProyecto.id_obra = this.obras[i]["id"]*1;
      }
    }
    for(var i = 0; i<this.empleados.length; i++){
     if(this.empleados[i]["cedula"] == empleado){
       this.pubProyecto.id_empleado = this.empleados[i]["id"]*1;
     }
   }
   
      this.data.addPemp(this.pubProyecto).subscribe(
        res => { 
          this.etapa= res;
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
      console.log(this.pubProyecto);
  }

  add_publicarProy(){
    this.pubProyecto.proyecto = (<HTMLInputElement>document.getElementById("proy2")).value;
    console.log(this.pubProyecto);
      this.data.addPProyecto(this.pubProyecto).subscribe(
        res => { 
          this.etapa= res;
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
  
  }
  constructor(private data:PeticionesService) {
  
  }
  scrollToDownload(element: any) {
    element.scrollIntoView({ behavior: "smooth" });
   
  }
  ngOnInit() {
    this.get_obras();
    this.get_empleados();
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


  eliminar(Tipo1:any){
    
    for (var indice = 0; indice < this.inmuebles.length; indice++){
      if(this.inmuebles[indice].Tipo == Tipo1){
        this.inmuebles.splice(indice, 1);
      }
    }
    
  }

  cambiarValor(event:any){
    this.editField = event.target.textContent;
  }

  actualizarLista( Tipo1:any, propiedad: any, event: any){

    const editField = event.target.textContent;
    console.log(editField);

    for (var indice = 0; indice < this.inmuebles.length; indice++){
      if(this.inmuebles[indice].Tipo == Tipo1){
        this.inmuebles[indice][propiedad] = editField;
        
      }
      this.editField=null;
    }
      
  }

 

  imprimirLista(){
    for (var indice = 0; indice < this.inmuebles.length; indice++){
      if(this.inmuebles[indice].Tipo == "Lote"){
        
        console.log(this.inmuebles[indice]);
      }
      console.log(this.inmuebles[indice]);
    }
  }


  buscar(tipo1:any,id1:any){
    this.valorBusqueda = (<HTMLInputElement>document.getElementById(tipo1)).value;
    const elemento = (<HTMLInputElement>document.getElementById(id1));

    console.log(this.valorBusqueda);
    if(elemento==null){
      (<HTMLInputElement>document.getElementById(tipo1)).scrollIntoView({behavior: 'smooth'});
    }
    else{
      (<HTMLInputElement>document.getElementById(id1)).scrollIntoView({behavior: 'smooth'});
    }
    return this.valorBusqueda;

  }
  

  get_materiales(){
 
    this.data.getMateriales().subscribe(datos => this.materiales= datos);
    console.log( this.materiales);
  
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
  this.data.addCompra(this.compra).subscribe(
    res => { 
      this.etapa= res;
     },
     error => {
       console.error(error);
       alert(error.error);
     }
  );
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

}
