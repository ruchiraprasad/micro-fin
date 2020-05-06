import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { UserComponent } from './user/user.component';
import { AuthGuard } from './shared/api-services/auth.guard';
import { HomeComponent } from './home/home.component';
import { CustomerComponent } from './customer/customer.component';


const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path : 'login', component: LoginComponent},
  {path : 'home', component: HomeComponent, canActivate: [AuthGuard]},
  {path : 'customer', component: CustomerComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
