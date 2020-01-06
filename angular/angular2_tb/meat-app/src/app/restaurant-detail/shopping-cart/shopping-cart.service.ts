import {Injectable} from '@angular/core'
import { CartItem } from "./cart-item.model";
import { MenuItem } from "../menu-item/menu-item.model";

@Injectable()
export class ShoppingCartService {
    items: CartItem[] = [] //inicio o array de cartItem com vazio
    
    clear(){//método que irá limpar o carrinho
        this.items = [] //simples, só taco um array vazio
    }

    addItem(item: MenuItem){
        //note a implementação: se eu for adicionar um item e ele já existir, só vou aumentar a quantidade!
        //primeiro eu verifico se o item já não está dentro do carrinho de compras

        let foundItem = this.items.find((mItem) => mItem.menuItem.id == item.id)
        if(foundItem){
            this.increaseQty(foundItem)
        }else{
            this.items.push(new CartItem(item)) //se não encontrar, adiciono o item que enviou
        }
    }

    increaseQty(item: CartItem){
        item.quantity = item.quantity + 1
    }

    decreaseQty(item: CartItem){
        item.quantity = item.quantity - 1
        if(item.quantity === 0){ //se for 0 removo do carrinho
            this.removeItem(item)
        }
    }

    removeItem(item: CartItem){
        //uso o splice, falando que a partir do meu item ele irá remover 1
        this.items.splice(this.items.indexOf(item),1)
    }    

    total(): number{ //totaliza o carrinho
        return this.items
            .map(item => item.value()) //primeiro eu faço o MAP pra trocar o valor do item inteiro, e pego só o value
                .reduce((prev, value) => prev+value,0) //pego o valor anterior + o atual, e 0 é o valor inicial (R$ 0)
    }
}