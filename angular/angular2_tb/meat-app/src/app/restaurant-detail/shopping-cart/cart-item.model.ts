import { MenuItem } from "../menu-item/menu-item.model";

//o que o item do carrinho de compras tem a mais que um item do menu: a quantidade (por que pode ser + de 1 item)
//precisa ter um valorTotal que será a quantidade x o preço

export class CartItem {
    constructor(public menuItem: MenuItem, 
        public quantity: number = 1){ }//se eu não passar nada, o padrão é 1 (são publics por que serão alterados por outros componentes)

    //método pra totalizar
    value(): number{
        return this.menuItem.price * this.quantity //lembrando que como declarei no construtor, é como se fosse na classe
    }
}