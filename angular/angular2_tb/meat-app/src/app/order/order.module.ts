import {NgModule} from "@angular/core"
import {RouterModule, Routes} from '@angular/router'

import {SharedModule} from '../shared/shared.module'

import {OrderComponent} from './order.component'
import {OrderItemsComponent} from './order-items/order-items.component'
import {DeliveryCostsComponent} from './delivery-costs/delivery-costs.component'

//como não existe mais no arquivos de rota uma indicação pro componente padrão (home) que ele irá criar
//dentro do módulo eu preciso informar pra ele qual é a ROTA padrão que ele irá carregar
const ROUTES: Routes = [
  {path:'', component: OrderComponent}
]

@NgModule({
  declarations:[OrderComponent,OrderItemsComponent,DeliveryCostsComponent],
  imports: [SharedModule, RouterModule.forChild(ROUTES)] //como o módulo de ordem usa componentes do Shared (input, radiobutton) eu importo ele
  //Nota: veja que interessante: como Common, Forms, Reactive são importados no "Shared", não preciso reimportar aqui. Já estarão carregados.
})
export class OrderModule {}