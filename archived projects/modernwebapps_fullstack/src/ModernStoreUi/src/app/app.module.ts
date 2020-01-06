import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

//Root
import { AppComponent } from './app.component';

//Shared
import { HeadbarComponent } from './components/shared/headbar/headbar.component';
import { SubMenuComponent } from './components/shared/sub-menu/sub-menu.component';
import { FooterComponent } from './components/shared/footer/footer.component';

//Components
import { ProductListComponent } from './components/product-list/product-list.component';

//Pages
import { HomePageComponent } from './pages/home-page/home-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SignupPageComponent } from './pages/signup-page/signup-page.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';

// Services SingleTon: CardService will be imported in the "MAIN" application Module, because
// we will always have just ONE card in all Pages. Card will never loose it's values, it needs
// to keep it data. So i will never make other instances. For the other, i'll do at the local .TS
// file (like login-page.component.ts).
import { CartService } from './services/cart.service';

@NgModule({
  declarations: [
    AppComponent,
    HeadbarComponent,
    SubMenuComponent,
    ProductListComponent,
    FooterComponent,
    HomePageComponent,
    LoginPageComponent,
    SignupPageComponent,
    CartPageComponent
  ],
  imports: [
    BrowserModule,
    // import HttpClientModule after BrowserModule. Will be used on Data.Service
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  //Note: The Services i inject here, will be a Singleton available for ALL app. If i'll use just in some pages,
  //i can inject this providers in the @Component Section on the Page.ts (like login-page.component.ts).
  //For example: The Card i will inject here, because it will be available for ALL application, will always appears.
  //IF i make a lot of "instances" for Card, i will lose it states and values... so: it's a singleton.
  providers: [CartService],
  bootstrap: [AppComponent]
})
export class AppModule { }
