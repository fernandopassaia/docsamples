import {NgModule, ModuleWithProviders} from '@angular/core'
import {CommonModule} from '@angular/common'
import {FormsModule, ReactiveFormsModule} from '@angular/forms'

import {InputComponent} from './input/input.component'
import {RadioComponent} from './radio/radio.component'
import {RatingComponent} from './rating/rating.component'

import {OrderService} from '../order/order.service'
import {ShoppingCartService} from '../restaurant-detail/shopping-cart/shopping-cart.service'
import {RestaurantsService} from '../restaurants/restaurants.service';
//import { SnackbarComponent } from './messages/snackbar/snackbar.component';
//import {NotificationService} from './messages/notification.service'

@NgModule({
  declarations: [InputComponent, RadioComponent, RatingComponent],
  //como meus componentes de input, radio e rating dependem de outras coisas (como forms, ngIf, ngFor) eu importo
  //veja que interessante: como eu importei Common, Forms, Reactive aqui - não preciso reimportar nos outros módulos que usam esse
  imports: [CommonModule, FormsModule, ReactiveFormsModule], 
  exports: [InputComponent, RadioComponent,RatingComponent, CommonModule,FormsModule, ReactiveFormsModule ]
})

//na classe de módulos eu chamei o "forRoot" para que ele expotasse junto os providers, que são os serviços do meu sistema.
//antes esses serviços estavam no core > Core.Module. Ler a documentação lá do por que foi desativado.
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers:[ShoppingCartService, RestaurantsService, OrderService]
    }
  }
}