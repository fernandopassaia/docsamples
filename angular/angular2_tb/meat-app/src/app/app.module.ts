import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms' //Feito durante o refactory para ReactiveForms

//Fernando: Importo meu módulo de rotas
import {ROUTES} from './app.routes'

//NOTA: Os itens Comentados estão desativados por que eu criei Módulos + LazyLoading (Aula 75)
//import { RatingComponent } from './shared/rating/rating.component'; 
//import { AboutComponent } from './about/about.component'; 
//import { OrderComponent } from './order/order.component';
//import { InputComponent } from './shared/input/input.component'; 
//import { RadioComponent } from './shared/radio/radio.component'; 
//import { OrderItemsComponent } from './order/order-items/order-items.component'
//import { DeliveryCostsComponent } from './order/delivery-costs/delivery-costs.component'; 

//Aqui foi criado o CORE MÓDULE de Serviços na Pasta Core
//import { RestaurantsService } from './restaurants/restaurants.service';
//import { ShoppingCartService } from './restaurant-detail/shopping-cart/shopping-cart.service';
//import { OrderService } from './order/order.service';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { RestaurantsComponent } from './restaurants/restaurants.component';
import { RestaurantComponent } from './restaurants/restaurant/restaurant.component'
import { RestaurantDetailComponent } from './restaurant-detail/restaurant-detail.component';
import { MenuComponent } from './restaurant-detail/menu/menu.component';
import { ShoppingCartComponent } from './restaurant-detail/shopping-cart/shopping-cart.component';
import { MenuItemComponent } from './restaurant-detail/menu-item/menu-item.component';
import { ReviewsComponent } from './restaurant-detail/reviews/reviews.component'
import { OrderSummaryComponent } from './order-summary/order-summary.component';
import { SharedModule } from './shared/shared.module';
//import { CoreModule } from './core/core.module';



@NgModule({
  declarations: [
    //AboutComponent, Desativei por que será usado Módulos + LazyLoading
    //OrderComponent,
    //InputComponent, Desativei por que será usado Módulos + LazyLoading
    //RadioComponent, Desativei por que será usado Módulos + LazyLoading
    //OrderItemsComponent,
    //DeliveryCostsComponent,

    AppComponent,
    HeaderComponent,
    HomeComponent,    
    RestaurantsComponent,
    RestaurantComponent,
    RestaurantDetailComponent,
    MenuComponent,
    ShoppingCartComponent,
    MenuItemComponent,
    ReviewsComponent,    
    OrderSummaryComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    //SharedModule,
    SharedModule.forRoot(), //for root importará também os providers dentro dele (que são os serviços de restaurantes, order e tudo mais)
    //CoreModule, //criado um módulo de serviços (NOTA: Módulo comentado, explicação está lá no TS)
    //FormsModule, //será usado lá no form de order (nota, desativei por que esses módulos são importados pelo SHARED Module que tem os inputs e tal)
    //ReactiveFormsModule, //Feito durante o refactory para ReactiveForms (nota, desativei por que esses módulos são importados pelo SHARED Module que tem os inputs e tal)
    
    //Nota: Existe também uma opção chamada "Pre Loading Strategy" que permite que eu carregue a aplicação pro usuário, liberando ele pro uso, e após
    //isso, eu comece a carregar meus componentes "secundários" no BackGround. Isso também evitará que lá na frente quando o usuário tenta carregar
    //uma funcionalidade que ainda não está, ela já esteja disponível. Eu farei isso lá no app.module.ts no RouterModule (preloadingStrategy).
    //em resumo: se eu tirar o preLoading, ele carregará no modo Lazy Loading (quando o usuário requisitar). Se eu deixar o PreLoading
    //ai os módulos serão carregados em Background por uma Thread após a aplicação já estar disponível pro usuário com os módulos principais.
        
    //RouterModule.forRoot(ROUTES)
    RouterModule.forRoot(ROUTES, {preloadingStrategy: PreloadAllModules})
  ],
  //O LOCALE_ID informa pra aplicação que estou usando o formato BR HUE HUE
  //ao declarar o RestaurantsService no providers, eu torno ele visível em toda aplicação pra poder fazer a injeção de dependências
  //providers: [RestaurantsService, ShoppingCartService, OrderService, {provide: LOCALE_ID, useValue: 'pt-BR'}], //ShoppingCartService é onde irei armazenar meu carrinho

  //os serviços acima pararam de ser importados por que foi criado um módulo CORE na pasta Core com esses serviços e importados logo acima (CoreModule)
  providers: [{provide: LOCALE_ID, useValue: 'pt-BR'}], //ShoppingCartService é onde irei armazenar meu carrinho
  bootstrap: [AppComponent]
})

export class AppModule { }