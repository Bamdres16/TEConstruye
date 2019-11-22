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
  //Se definen arrays y objetos a utilizar en las funciones 
  obra: any ={};
  pubProyecto: any ={};
  obras:Array<any>;
  empleados:Array<any>;
  provincias:Array<any>=[];
  cantones:Array<any>=[];
  distritos:Array<any>=[];
  etapas:Array<any>=[];
  etapasP:Array<any>=[];
  materiales:Array<any>=[];
  etapasPNames:Array<any>=[];
  proyectos:Array<any>=[];
  ubicacion:Array<any>=[];
  isCollapsed = true;
  focus;
  focus1;
  obj: Array<any>=[];
  compra: any ={};
  paqueteArq: Array<any>=[];
  paqueteIng: Array<any>=[];
  clientes: Array<any>=[];
  ingenieros:Array<any>=[];
  arquitectos:Array<any>=[];
  inmuebles: Array<any> = [

    {Tipo: 'Lote', cedulaAdmin:123},
    {Tipo: "Casa", cedulaAdmin:123},
    {Tipo: "Apartamento", cedulaAdmin:123},
   
  ];

  
  image: string | ArrayBuffer;

  focus2;
  trash:any = {};
  etapa:any = {};
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  tipo: any;

  //Constructor de la función, se hace una instancia de los servicios y rutas 
  constructor(private data:PeticionesService, private ruta:ActivatedRoute, ) {
    this.ruta.queryParams.subscribe(params =>{
       //Se cargan los parámetros de la vista anterior
      this.tipo= params['tipo'];
      //Se hacen los get para cargar las vistas
      this.getEtapas();
      this.getProyectos();
      this.getMateriales();
      this.getProvincias();
      this.getClientes();
      this.getIngenieros();
      this.getArquitectos();
      this.get_empleados();
      this.get_obras()

      }
 
   );
  }

  //FUNCIONES PARA LA APARIENCIA DE LA PÁGINA 

  
  scrollToDownload(element: any) {
    element.scrollIntoView({ behavior: "smooth" });
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


 
  //FUNCIONES DE INICIALIZACIÓN DE ANGULAR
  ngOnInit() {
    var body = document.getElementsByTagName("body")[0];
    body.classList.add("index-page");
    this.getEtapas();
    this.getProyectos();
    this.getMateriales();

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


  
  
  //FUNCIONES DE GET
  getEtapas(){
    this.data.getEtapa().subscribe(datos => {console.log(datos); this.etapas = datos});   
  }

  getProyectos(){
    this.data.getProyecto().subscribe(datos => {console.log(datos); this.proyectos = datos});
   
  }
  getProvincias(){
    this.data.getProvincias().subscribe(datos => { this.provincias= datos});
    console.log(this.provincias);
 
  }
  getCantones(provincia:string){
    console.log('Solictando this.provincias.............');
    this.data.getCantones(provincia).subscribe(datos => { this.cantones = datos});
    console.log(this.cantones);
  
  }
  getDistritos(provincia:string, canton:string){
    this.data.getDistritos(provincia,canton).subscribe(datos => { this.distritos= datos}); 
    console.log(this.distritos);
  }
  getClientes(){
    this.data.getClientes().subscribe(datos => { this.clientes= datos}); 
    console.log(this.clientes);
  }
  getIngenieros(){
    this.data.getIngenieros().subscribe(datos => { this.ingenieros= datos}); 
    console.log(this.ingenieros);
  }
  getArquitectos(){
    this.data.getArquitectos().subscribe(datos => { this.arquitectos= datos}); 
    console.log(this.arquitectos);
  }

  getEtapasProyecto(){

    var nombreP = (<HTMLInputElement>document.getElementById("proyecto_nombreGastos")).value;
        
    var idP;

    for(var i = 0; i<this.proyectos.length; i++){
      if(this.proyectos[i]["nombre_obra"] == nombreP){
        idP = this.proyectos[i]["id"];
      }
    }
    this.data.etapaProyecto(idP).subscribe(datos => {console.log(datos); this.etapasP = datos});
    console.log('LAs estapas'+this.etapasP);
    this.etapasPNames = [];
    for(var i = 0; i < this.etapasP.length; i++){
      var id = this.etapasP[i]["id_etapa"];

      for(var p = 0; p < this.etapas.length; p++){
        if(this.etapas[p]["id"] == id){
          this.etapasPNames.push(this.etapas[p]["nombre"]);
        }
      }
    }
    
  }

  getMateriales(){
    this.data.getMateriales().subscribe(datos => {console.log(datos); this.materiales = datos});
    console.log(this.materiales);
  }

  get_materiales(){

    this.data.getMateriales().subscribe(datos => this.materiales= datos);
    console.log( this.materiales);
  
  }
  
  get_obras(){
   
    this.data.getObras().subscribe(datos => this.obras= datos);
  
  }
  get_empleados(){
   this.data.getEmpleados().subscribe(datos => this.empleados= datos);
  console.log( this.empleados);
  }

  
 

  //FUNCIONES DE POST
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

  AsignarHoras(){
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
   
      this.data.AsignarHoras(this.pubProyecto).subscribe(
        res => { 
          this.pubProyecto= res;
         },
         error => {
           console.error(error);
           alert(error.error);
         }
      );
      console.log(this.pubProyecto);
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
    etapaobra["fecha_incio"] = fechaI;
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

  }

  

  addMatEtapa(){
    var nombreP = (<HTMLInputElement>document.getElementById("proyecto_nombreGastos")).value;
    var nombreE = (<HTMLInputElement>document.getElementById("etapa_anadirMat")).value;
    var mat = (<HTMLInputElement>document.getElementById("material_etapa")).value;
    var cantidad = (<HTMLInputElement>document.getElementById("cantidad_mats")).value;
    var idP;
    var idE;
    var CodigoMat;

    for(var i = 0; i < this.proyectos.length; i++){
      if(this.proyectos[i]["nombre_obra"] == nombreP){
        idP = this.proyectos[i]["id"];
      }
    }

    for(var i = 0; i < this.etapas.length; i++){
      if(this.etapas[i]["nombre"] == nombreE){
        idE = this.etapas[i]["id"];
      }
    }

    for(var i = 0; i < this.materiales.length; i++){
      if(this.materiales[i]["nombre"] == mat){
        CodigoMat = this.materiales[i]["codigo"];
      }
    }

    var matEtapa ={};
    matEtapa["id_etapa"] = idE;
    matEtapa["codigo_material"] = CodigoMat;
    matEtapa["id_obra"] = idP;
    matEtapa["cantidad"] = cantidad;

    this.anadirMaterialesEtapa(matEtapa);
    
  }

  anadirMaterialesEtapa(matEtapa:any){
    this.data.addMatEtapa(matEtapa).subscribe(
      res => {
        this.trash= res;
       },
       error => {
         console.error(error);
         alert(error.error);
       }
    );
  }


  agregarIng(codigo_ingeniero:any){
  for (var indice = 0; indice < this.ingenieros.length; indice++){
    if(this.ingenieros[indice].codigo_ingeniero == codigo_ingeniero){
      var id = this.ingenieros[indice]["id"];
      this.ingenieros.splice(indice, 1);
      this.paqueteIng.push(id); 
    }
  }
  console.log(this.paqueteIng);
}

agregarArq(codigo_arquitecto:any){
  for (var indice = 0; indice < this.arquitectos.length; indice++){
    if(this.arquitectos[indice].codigo_arquitecto == codigo_arquitecto){
      var id = this.arquitectos[indice]["id"];
      this.arquitectos.splice(indice, 1);
      this.paqueteArq.push(id); 
    }
  }
  console.log(this.paqueteArq);
}
addNuevaObra(){

  this.obra.nombre_obra= (<HTMLInputElement>document.getElementById("nombre_obra")).value;
  this.obra.cantidad_habitaciones = (<HTMLInputElement>document.getElementById("habitaciones_obra")).valueAsNumber;
  this.obra.cantidad_banos = (<HTMLInputElement>document.getElementById("banos_obra")).valueAsNumber;
  this.obra.cantidad_pisos= (<HTMLInputElement>document.getElementById("plantas_obra")).valueAsNumber;
  this.obra.area_construccion=(<HTMLInputElement>document.getElementById("cons_obra")).valueAsNumber;
  this.obra.area_lote=(<HTMLInputElement>document.getElementById("lote_obra")).valueAsNumber;
  var propietario=(<HTMLInputElement>document.getElementById("cliente_obra")).value;
  var ubicacion=(<HTMLInputElement>document.getElementById("ubicacion_obra")).value;
  this.obra.ingenieros = this.paqueteIng;
  this.obra.arquitectos = this.paqueteArq;
  for(var i = 0; i < this.clientes.length; i++){
    if(this.clientes[i]["cedula"] == propietario ){
      this.obra.ubicacion= this.clientes[i]["id"];
    }
  }
  for(var i = 0; i < this.distritos.length; i++){
    if(this.distritos[i]["distrito"] == ubicacion ){
      this.obra.propietario= this.clientes[i]["id"];
    }
  }
  console.log(this.obra);
  this.data.addObra(this.obra).subscribe(
    res => {
      this.trash= res;
     },
     error => {
       console.error(error);
       if(error.status != 500){
        alert(error.error);
       }
       
     }
  );
  window.location.reload();
}

}
