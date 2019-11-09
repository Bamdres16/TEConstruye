import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { Routes, RouterModule } from "@angular/router";

import { IndexComponent } from "./pages/index/index.component";
import { ProfilepageComponent } from "./pages/examples/profilepage/profilepage.component";
import { RegisterpageComponent } from "./pages/examples/registerpage/registerpage.component";
import { RegisterEmppageComponent } from "./pages/examples/register-emppage/register-emppage.component";
import { RegisterArqpageComponent } from "./pages/examples/register-arqpage/register-arqpage.component";
import { RegisterClipageComponent } from "./pages/examples/register-clipage/register-clipage.component";
import { LandingpageComponent } from "./pages/examples/landingpage/landingpage.component";
import { LoginpageComponent } from "./pages/examples/loginpage/loginpage.component";
import { AdminpageComponent } from "./pages/examples/adminpage/adminpage.component";
import { IngArqpageComponent } from "./pages/examples/ing-arqpage/ing-arqpage.component";


const routes: Routes = [
  { path: "", redirectTo: "inicio", pathMatch: "full" },
  { path: "inicio", component: IndexComponent },
  { path: "perfil", component: ProfilepageComponent },
  { path: "registroIng", component: RegisterpageComponent },
  { path: "landing", component: LandingpageComponent },
  { path: "ingresar", component: LoginpageComponent },
  { path: "registroArq", component: RegisterArqpageComponent},
  { path: "registroCli", component: RegisterClipageComponent },
  { path: "registroEmp", component: RegisterEmppageComponent},
  { path: "ingarq", component: IngArqpageComponent },
  { path: "administrador", component: AdminpageComponent}
  
];



@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes, {
      useHash: true
    })
  ],
  exports: []
})
export class AppRoutingModule {}
