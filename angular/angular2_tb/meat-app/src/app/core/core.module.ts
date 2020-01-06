//NOTA IMPORTANTE: Na aula 80 esse módulo foi DESATIVADO por que ele foi "exportado" pro Shared Módules, que lá no "SharedModule"
//está EXPORTANDO esses Providers junto com o Shared. Desse modo, essa classe deixou de fazer sentido, todavia, fica o código ai.


// import { NgModule } from "@angular/core";
// import { ShoppingCartService } from "app/restaurant-detail/shopping-cart/shopping-cart.service";
// import { RestaurantsService } from "app/restaurants/restaurants.service";
// import { OrderService } from "app/order/order.service";

// //Core Modules: Pelo que entendi são uma maneira de organizar serviços que podem ser usados por toda aplicação. Nesse caso específico, ele criou
// //um diretório chamado CORE, e dentro dele um CoreMódule. Serviços (OrderService, ShoppingCartService, RestaurantsServices) ficarão nesse core.

// @NgModule({
//     providers:[ShoppingCartService, RestaurantsService, OrderService]
// })

// export class CoreModule{}