import { Component, OnInit } from '@angular/core';
import { RestaurantsService } from '../restaurants/restaurants.service' //importo meu serviço pra injetar
import { Restaurant } from 'app/restaurants/restaurant/restaurant.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'mt-restaurant-detail',
  templateUrl: './restaurant-detail.component.html'
})
export class RestaurantDetailComponent implements OnInit {

  restaurant: Restaurant //crio uma propriedade do tipo restaurante pra representar meu objeto
  //injeto meu serviço de restaurantes
  
  //injeto o serviço de restaurante e o activated route: ele me permite pegar a rota que foi ativada e os parametros (como o id)
  constructor(private restaurantsService: RestaurantsService, private route: ActivatedRoute) { }

  //no init (que é o método ideal para inicialização de um componente) eu farei a consulta por ID (que está no meu service)
  ngOnInit() {
    //e ai eu me inscrevo (Listener) no método - no subscribe eu recebo o restaurante e atributo na propriedade local
    this.restaurantsService.restaurantById(this.route.snapshot.params['id']) //através de SNAPSHOT - pego o ID que veio no activatedRoute (injetado) pra passar pro meu serviço
      .subscribe(restaurant => this.restaurant = restaurant)
  }

}
