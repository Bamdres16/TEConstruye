import { Component, OnInit } from '@angular/core';
import { PeticionesService } from 'src/app/peticiones.service';


@Component({
  selector: 'app-register-clipage',
  templateUrl: './register-clipage.component.html',
  styleUrls: ['./register-clipage.component.scss']
})
export class RegisterClipageComponent implements OnInit {
  cliente: any ={};
  constructor(private data:PeticionesService) { }

  ngOnInit() {
  }
  add_cliente(){
    console.log(this.cliente);
  this.data.addCliente(this.cliente).subscribe(
    res => {
      console.log(res);
      this.cliente= res;

     },
     error => {
       console.error(error);
       alert(error.error);

       
     }
  );
   
  }

}
