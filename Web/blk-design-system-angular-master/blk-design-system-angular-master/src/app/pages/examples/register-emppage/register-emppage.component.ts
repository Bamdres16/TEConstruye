import { Component, OnInit } from '@angular/core';
import { PeticionesService } from 'C:/Users/Franklin/Desktop/TEConstruye/Web/blk-design-system-angular-master/blk-design-system-angular-master/src/app/peticiones.service';

@Component({
  selector: 'app-register-emppage',
  templateUrl: './register-emppage.component.html',
  styleUrls: ['./register-emppage.component.scss']
})
export class RegisterEmppageComponent implements OnInit {
  empleado: any ={};
  constructor(private data:PeticionesService) { }

  ngOnInit() {
  }

  add_empleado(){
    console.log(this.empleado);
    this.data.addCliente(this.empleado).subscribe(
      res => {
        console.log(res);
        this.empleado= res;
    
       },
       error => {
         console.error(error);
         alert(error.error);
    
         
       }
    );
     
    }

}
