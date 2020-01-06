import { Component, OnInit } from '@angular/core';
import {Restaurant} from './restaurant/restaurant.model'
import { RestaurantsService } from './restaurants.service';

@Component({
  selector: 'mt-restaurants',
  templateUrl: './restaurants.component.html'  
})
export class RestaurantsComponent implements OnInit {

  restaurants: Restaurant[] //objeto que vai receber a lista de restaurante, ficará undefined e
  //receberá a injeção (de dependência) que virá do meu serviço
  
  //agora farei a injeção de dependência do meu serviço via construtor e ele fará a "instância" pra mim. Note que
  //esse componente teve que ser declarado no app.module.ts. NOTA IMPORTANTE: quando você declara no angular uma 
  //variável no construtor, é como se eu tivesse criado um atributo na classe - ele sozinho cria o atributo
  constructor(private restaurantService: RestaurantsService) { }  

  ngOnInit() {
    //toda vez que eu instancio minha classe, ele entrará no onInit (como se fosse um construtor)
    //e fará tudo que tiver aqui dentro - chamo o método que carrega os restaurantes
    //this.restaurants = this.restaurantService.restaurants() //chamada antiga quando retornava array

    this.restaurantService.restaurants()
      .subscribe(restaurants => this.restaurants = restaurants) //assino o método, e jogo o que eu receber na minha variável declarada lá em cima
      //esse é o famoso LISTENER em todo processo. Agora o procedimento (ordem) que tudo isso acontece é a seguinte:
  }

}
