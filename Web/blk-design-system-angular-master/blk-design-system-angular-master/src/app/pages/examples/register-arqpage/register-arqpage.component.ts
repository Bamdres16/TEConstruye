import { Component, OnInit } from '@angular/core';
import { PeticionesService } from 'C:/Users/Franklin/Desktop/TEConstruye/Web/blk-design-system-angular-master/blk-design-system-angular-master/src/app/peticiones.service';

@Component({
  selector: 'app-register-arqpage',
  templateUrl: './register-arqpage.component.html',
  styleUrls: ['./register-arqpage.component.scss']
})
export class RegisterArqpageComponent implements OnInit {

  especialidades:Array<any>=[];
  arquitecto: any ={};

  constructor(private data:PeticionesService) {
    this.especialidades=[];
    this.get_especialidades();
   
   }

  ngOnInit() {
    
  }
  get_especialidades(){
 
      this.data.getEspecialidades().subscribe(datos => this.especialidades= datos);
      console.log(this.especialidades);

 }

 add_arquitecto(){
  console.log(this.arquitecto);
  this.data.addCliente(this.arquitecto).subscribe(
    res => {
      console.log(res);
      this.arquitecto= res;

     },
     error => {
       console.error(error);
       alert(error.error);

       
     }
  );
   
  }

}
