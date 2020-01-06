import { Component, OnInit } from '@angular/core';
import { RestaurantsService } from '../../restaurants/restaurants.service' //injeto o serviço
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'mt-reviews',
  templateUrl: './reviews.component.html'
})
export class ReviewsComponent implements OnInit {

  //injeto o serviço de restaurantes
  constructor(private restaurantsService: RestaurantsService,
      private route: ActivatedRoute)  { } //importo por que preciso pegar o ID

  //nessa classe será feito um POUCO diferente. Ao invés de usar o Subscribe, ele usará o PipeAsync.
  reviews: Observable<any>

  ngOnInit() {
    //nota sobre o ActivatedRoute: Nesse caso eu não estou pegando o ID do componente (por que estamos no filho, no Review)
    //então eu preciso pegar o parametro do PARENT - do componente acima (restaurante), por isso também é um pouco diferente
    this.reviews = this.restaurantsService
       .reviewsOfRestaurant(this.route.parent.snapshot.params['id'])
  }

}