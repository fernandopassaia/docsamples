import { Component, OnInit } from '@angular/core';
import { RestaurantsService } from '../../restaurants/restaurants.service' //injeto o serviço
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from '../menu-item/menu-item.model';

@Component({
  selector: 'mt-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit {

  //preciso pegar o RestaurantService, o ActivatedRoute e chamar o Service de MenuRestaurant
  //Farei o Subscribe apenas utilizando o Pipe Async
  menu: Observable<MenuItem[]>

  constructor(private restaurantsService: RestaurantsService,
    private route: ActivatedRoute)  { } //importo por que preciso pegar o ID do Restaurante para carregar o menu

  ngOnInit() {
    this.menu = this.restaurantsService
      .menuOfRestaurant(this.route.parent.snapshot.params['id']) //parent por que vem do item de cima
  }

  addMenuItem(item: MenuItem){
    console.log(item) //imprime no console só pra ver o item que está chegando
  }
}
