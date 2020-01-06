import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart.service'

@Component({
  selector: 'app-sub-menu',
  templateUrl: './sub-menu.component.html'
})
export class SubMenuComponent implements OnInit {
  public totalItems: number = 0;

  constructor(private cartService: CartService) {
    this.cartService.cartChange.subscribe((data) => {
      console.log(data);
      this.totalItems = data.length;  
    });
   }

  ngOnInit() {
  }

  addItem(){
    this.cartService.addItem({ title: 'teste'});
  }
}