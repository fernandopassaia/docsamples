import { ShoppingCartService } from "app/restaurant-detail/shopping-cart/shopping-cart.service";
import { Injectable } from "@angular/core";
import { CartItem } from "app/restaurant-detail/shopping-cart/cart-item.model";
import { Order, OrderItem } from "./order.model";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map'
import { Http, Headers, RequestOptions } from "@angular/http"; //meu post no terceiro parâmetro precisa de Headers e Request pra informar o tipo de parâmetro
import {MEAT_API} from '../app.api'

//pra passar os itens para o nosso componente de items, faremos com que o componente de compras tenha acesso a esse serviço
//chamado OrderService. E o OrderService vai acessar o serviço existente de Shopping-Cart nos vídeos passados - pra pegar os itens. 

@Injectable()
export class OrderService{
    //nota - eu injeto http por que ele será usado no POST lá embaixo no checkOrder
    constructor(private cartService: ShoppingCartService, private http: Http){}

    //o que vamos fazer é ter alguns métodos pra expor algumas coisas que já são fornecidas pelo shopping-cart-service
    //mas iremos criar específico pras compras (* em resumo: só to criando uma interface pra métodos/propriedades que já são do shopping-cart)

    //retorna o valor total do meu carrinho
    itemsValue(): number {
        return this.cartService.total()
    }

    //método pra expor os itens do carrinho
    cartItems(): CartItem[]{
        return this.cartService.items; 
    }

    //e agora terei métodos necessários pra responder os eventos que eu criei (que apenas chamarão os métodos do carrinho pra add e diminuir)
    increaseQtd(item: CartItem){
        this.cartService.increaseQty(item)
    }

    decreaseQtd(item: CartItem){
        this.cartService.decreaseQty(item)
    }

    remove(item: CartItem){
        this.cartService.removeItem(item)
    }

    //método chamado após a compra concluída pra limpar tudo
    clear(){
        this.cartService.clear()
    }

    //aqui é o método que eu irei enviar a Order pro meu HTTP Post - BackEnd
    //quando eu uso uma chamada HTTP já sei que irei usar o Observable, ele irá
    //retornar um Observable: Dessa forma meu componente pode se inscrever e obter a 
    //resposta nesse caso eu irei retornar uma STRING que será o ID da compra (show d+)
    checkOrder(order: Order): Observable<string>{

        //no terceiro parametro do post preciso informar o tipo de header (que é json)
        const header = new Headers()
        header.append('Content-Type', 'application/json')

        return this.http.post(`${MEAT_API}/orders`,
                JSON.stringify(order), new RequestOptions({headers: header}))
            .map(response => response.json()) 
            .map(order => order.id) //se eu quisesse pegar o ID da compra e retornar um Observable<string> com o ID gerado.
            //preciso mapear por que esse método retorna Observable(Response), eu quero algo inxuto, irei converter ele pra json da resposta

        //mando uma representação textual (json) do meu order (stringify)
        //nesse caso o texto do meu objeto order irá no corpo da minha requisição http
        //DICA IMPORTANTE: Http é um protocolo textual, eu sempre vou trabalhar com texto        
    }
}