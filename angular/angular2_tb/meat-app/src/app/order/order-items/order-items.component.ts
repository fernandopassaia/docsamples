import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CartItem } from 'app/restaurant-detail/shopping-cart/cart-item.model';

@Component({
  selector: 'mt-order-items',
  templateUrl: './order-items.component.html'
})
export class OrderItemsComponent implements OnInit {

  //como vou precisar informar por um componente externo, sempre uso input
  @Input() items: CartItem[] //TODA vez que uma propriedade for informada pelo PARENT, eu preciso botar o input pra permitir o acesso de fora
  
  @Output() increaseQty = new EventEmitter<CartItem>()
  @Output() decreaseQty = new EventEmitter<CartItem>()
  @Output() remove = new EventEmitter<CartItem>()
  
  constructor() { }

  ngOnInit() {
  }

  //Nota: Nós temos 3 eventos nos itens, Adicionar Quantidade, Remover Quantidade e Remover o Item do carrinho
  //abaixo eu faço os métodos, no component (html) eu linko eles, e acima eu declaro como output

  emitIncreaseQty(item: CartItem){
    this.increaseQty.emit(item)
  }

  emitDecreaseQty(item: CartItem){
    this.decreaseQty.emit(item)
  }

  emitRemove(item: CartItem){
    this.remove.emit(item)
  }
}