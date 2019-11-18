import { Component, OnInit, OnDestroy } from "@angular/core";
import noUiSlider from "nouislider";
import { PeticionesService } from 'src/app/peticiones.service';
import {Router, ActivatedRoute} from '@angular/router';

@Component({
  selector: "app-index",
  templateUrl: "index.component.html"
})
export class IndexComponent implements OnInit, OnDestroy {
  isCollapsed = true;
  usuario: any ={};
  ingeniero: any ={};
  arquitecto: any ={};
  admin: any ={};
  focus;
  focus1;
  focus2;
  date = new Date();
  pagination = 3;
  pagination1 = 1;
  constructor(private data:PeticionesService, private router:Router) {

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

  iniciar_sesion(){
    
    if (this.usuario.tipo=='Ingeniero(a)'){
      this.ingeniero.codigo_ingeniero= this.usuario.correo;
      this.ingeniero.contrasena=this.usuario.contrasena;
      console.log(this.ingeniero);
      this.data.loginIngeniero(this.ingeniero).subscribe(
        res => {
          console.log(res);
          this.ingeniero= res;
         // console.log('LA INFORMACION DE USUARIO ES'+ this.cliente[0].cedula)
         },
         error => {
           console.error(error);
           alert(error.error); 
         },
         () => this.navigate1());
         //this.navigate1();
    }else if (this.usuario.tipo=='Arquitecto(a)') {
      
      this.arquitecto.codigo_arquitecto= this.usuario.correo;
      this.arquitecto.contrasena=this.usuario.contrasena;
      console.log(this.arquitecto);
      this.data.loginArquitecto(this.arquitecto).subscribe(
        res => {
          console.log(res);
          this.arquitecto= res;
          //console.log('LA INFORMACION DE USUARIO ES'+ this.cliente[0].cedula)
         },
         error => {
           console.error(error);
           alert(error.error);

         },
         () => this.navigate2());
    } else {
      this.admin.usuario= this.usuario.correo;
      this.admin.contrasena=this.usuario.contrasena;
      console.log(this.admin);
      this.data.loginAdmin(this.admin).subscribe(
        res => {
          this.admin= res;
          //console.log('LA INFORMACION DE USUARIO ES'+ this.cliente[0].cedula)
         },
         error => {
           console.error(error);
           alert(error.error);

         },
         () => this.navigate3());
    }    
    }

    navigate1() {
      this.router.navigate(['ingarq', this.ingeniero[0].codigo_ingeniero], { queryParams: {tipo: 'Ingeniero'}})
    }
    navigate2() {
      this.router.navigate(['ingarq', this.arquitecto[0].codigo_arquitecto], { queryParams:{tipo: 'Arquitecto'}})
    }
    navigate3() {
      this.router.navigate(['administrador'], {queryParams: {idUser: this.admin.codigo}})
    }
    
}
