import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from './shopping-cart.service';
import {trigger, state, style, transition, animate, keyframes} from '@angular/animations'

@Component({
  selector: 'mt-shopping-cart',
  templateUrl: './shopping-cart.component.html'
})
export class ShoppingCartComponent implements OnInit {

  //injeto o Shopping Cart Service
  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit() {
  }

  //método para expor os itens
  items(): any[]{
    return this.shoppingCartService.items; //retorno os items
  }
  //método para expor o total dos itens

  //A Ordem é simples: O meu Componente HTML no botão "Limpar" chamará esse CLEAR. E esse clear, chamará o Clear lá do Serviço. Simples assim.
  clear(){
    this.shoppingCartService.clear()
  }

  //A Ordem aqui é: A Pessoa vai clicar no "X" do remove item, que enviará o Item pra cá, e esse mandará remover lá no meu serviço
  removeItem(item:any){
    this.shoppingCartService.removeItem(item)
  }

  //a ordem aqui é a seguinte: ao clicar no ADICIONAR produtos (do menú), ele irá avisar o componente de cima (Parent) e o componente de cima irá se
  //encarregar de jogar no meu componente de Cart. Note que parte dessa implementação estará no componente de MENU (menu.component.html - add #shoppingCart)
  addItem(item: any){
    this.shoppingCartService.addItem(item)
  }

  total(): number {
    return this.shoppingCartService.total(); //retorno o total
  }

}
