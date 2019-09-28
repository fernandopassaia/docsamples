import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CartPageComponent } from './pages/cart-page/cart-page.component'
import { HomePageComponent } from './pages/home-page/home-page.component'
import { LoginPageComponent } from './pages/login-page/login-page.component'
import { SignupPageComponent } from './pages/signup-page/signup-page.component'


const routes: Routes = [
  { path: '', component: LoginPageComponent}, //empty route will always take to login
  { path: 'home', component: HomePageComponent},
  { path: 'signup', component: SignupPageComponent},
  { path: 'cart', component: CartPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule] //i export my routes
})
export class AppRoutingModule { }
