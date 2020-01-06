//Arquivo de Rotas criado por Fernando Passaia
import {Routes} from '@angular/router' //importo o pacote de rotas
import { HomeComponent } from './home/home.component' //importo pra usar no path '' - homecomponent
//import { AboutComponent } from './about/about.component' Desativei por que será usado Módulos + LazyLoading
import { RestaurantsComponent } from './restaurants/restaurants.component';
import { RestaurantDetailComponent } from './restaurant-detail/restaurant-detail.component';
import { MenuComponent } from './restaurant-detail/menu/menu.component';
import { ReviewsComponent } from './restaurant-detail/reviews/reviews.component';
import { OrderComponent } from './order/order.component'

export const ROUTES: Routes = [
    //aqui eu vou informar meu array de rotas pro angular
    {path: '', component: HomeComponent}, //quando a rota estiver vazia, mando pro home
    {path: 'restaurants', component: RestaurantsComponent},    

    //agora eu tenho uma rota com id do restaurante, pra exibir o restaurante conforme seleção
    {path: 'restaurants/:id', component: RestaurantDetailComponent,
        children: [
            {path: '', redirectTo: 'menu', pathMatch: 'full'}, //aqui eu digo que quando entrar em restaurantes, o padrão "é o menú"
            {path: 'menu', component: MenuComponent},
            {path: 'reviews', component: ReviewsComponent}
        ]},
    //assim como no componente principal, onde eu tenho o ROUTE OUTLET e faço toda navegação dinâmica:
    //se eu quiser fazer uma sub-navegação (como nesse caso, ao selecionar o restaurante eu posso ver as notas OU o cardápio)
    //eu posso criar as rotas filhas (children) que também é do tipo route e também vai ter outro array de path e components 

    //NOTA: Os itens Comentados estão desativados por que eu criei Módulos + LazyLoading (Aula 75)
    //{path:'about', component: AboutComponent}, //quando ele encontrar o about, mandará para o componente about
    //{path:'order', component: OrderComponent},

    {path:'order', loadChildren: './order/order.module#OrderModule'},
    {path:'order-summary', loadChildren: './order/order.module#OrderModule'}, //na string eu passo o módulo: o diretório, o módulo + cerquilha e o nome do módulo
    {path:'about', loadChildren: './about/about.module#AboutModule'} //loadChildren habilita o LazyLoading (ver sobre Módulos no Angular) (quando /about for carregado, ele procurará o módulo)
]