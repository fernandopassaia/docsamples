import { Component, OnInit, Input } from '@angular/core';
import { Restaurant } from './restaurant.model';

@Component({
  selector: 'mt-restaurant',
  templateUrl: './restaurant.component.html'
})
export class RestaurantComponent implements OnInit {

  //eu vou criar um objeto do tipo Restaurante, para que ele possa ser passado para meu componente
  //Depois eu irei importar o decorator Input, e marcar o restaurante: pra permitir que outros componentes possam passar o
  //restaurante pro meu componente restaurante, e assim ele possa exibir o restaurante
  @Input() restaurant: Restaurant
  constructor() { }

  ngOnInit() {
  }

}
