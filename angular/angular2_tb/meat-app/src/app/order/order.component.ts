import { Component, OnInit } from '@angular/core';
import { RadioOption } from 'app/shared/radio/radio-option.model';
import { OrderService } from './order.service';
import { CartItem } from 'app/restaurant-detail/shopping-cart/cart-item.model';
import { Order, OrderItem } from './order.model'; //meu modelo do fechamento da compra
import { Router } from '@angular/router';
import {FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms' //importei pro refatoramento com Reactive Forms

@Component({
  selector: 'mt-order',
  templateUrl: './order.component.html'  
})
export class OrderComponent implements OnInit {

  emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i

  numberPattern = /^[0-9]*$/

  orderForm: FormGroup //refatoração ReactiveForms: Crio um Atributo que vai representar nosso formulário
  
  //nota: numa aplicação REAL a taxa de entrega e as formas de pagamento deveriam vir de algum
  //backend. Mas nesse caso, irei basicamente chumbar no aplicativo, visto que temos um demo.
  delivery: number = 8

  //crio um array de opções de pagamento que será passado para o componente de Radio para mostrar as formas de pagamento
  paymentOptions: RadioOption[] = [
    {label: 'Dinheiro', value: 'MON'},
    {label: 'Cartão de Débito', value: 'DEB'},
    {label: 'Cartão de Crédito', value: 'CRE'}
  ]

  //agora eu injeto meu serviço de compra e exponho os métodos - injeto o serviço  
  constructor(private orderService: OrderService,
  //nota: injetei o "ROUTER" por que preciso que esse componente faça uma navegação via código,
  //no método check enviarei o usuário para página de Order-Summary (via código - rota no arquivo app.routes.ts)
  private router: Router, private formBuilder: FormBuilder) { } //recebo o FormBuilder por injeção de dep, para criar meu Reactive Form

  ngOnInit() {
    //nossa, eu irei estender um pouco o exemplo adicionando campos para nome, email e dados da pessoa, abaixo crio os componentes de reactive forms
    //note que meu componente INPUT (pasta shared) será refatorado pois ele inclui uma referência pra NgModel (Templates Forms). As estratégias de
    //Reactive Forms x Template Forms não posem ser mixadas, você usa uma ou outra. Ponto.
    this.orderForm = this.formBuilder.group({
      name: this.formBuilder.control('', [Validators.required, Validators.minLength(5)]),
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      emailConfirmation: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      address: this.formBuilder.control('', [Validators.required, Validators.minLength(5)]),
      number: this.formBuilder.control('', [Validators.required, Validators.pattern(this.numberPattern)]),
      optionalAddress: this.formBuilder.control(''),
      paymentOption: this.formBuilder.control('', [Validators.required])
    }, {validator: OrderComponent.equalsTo}) //chamo o método que valida se emails são iguais
  }

  //método que vai validar se os dois e-mails são iguais, retornar um booleano e uma chave (mensagem) contendo o erro
  static equalsTo(group: AbstractControl): {[key:string]: boolean} {
    const email = group.get('email')
    const emailConfirmation = group.get('emailConfirmation')
    if(!email || !emailConfirmation){
      return undefined
    }
    if(email.value !== emailConfirmation.value){
      return {emailsNotMatch:true}
    }
    return undefined
  }

  //já tenho o total no meu carrinho de compras, então só pego
  itemsValue(): number{
    return this.orderService.itemsValue()
  }

  //agora eu vou expor os meus serviços que foram injetados no construtor - esses eventos serão linkados na página
  //primeiro exponho os itens
  cartItems(){
    return this.orderService.cartItems()
  }

  increaseQty(item: CartItem){
    this.orderService.increaseQtd(item)
  }

  decreaseQty(item: CartItem){
    this.orderService.decreaseQtd(item)
  }

  remove(item: CartItem){
    this.orderService.remove(item)
  }

  //order que está vindo é lá do form (html) o form.value que envia exatamente o modelo
  checkOrder(order: Order){
    order.orderItems = this.cartItems()
      .map((item:CartItem)=> new OrderItem(item.quantity, item.menuItem.id)) 
      //transformo os itens que são cartItems pra orderItems (quantidade e id são os construtores do orderItem)
      //com o map eu converto o array de cartItems pra orderItems, pego os itens e atributo no onjeto de compra

      //agora eu vou criar um método no meu serviço que receba esse objeto Order e mande pro meu backEnd
      this.orderService.checkOrder(order)
      .subscribe( (orderId: string ) => {
        this.router.navigate(['/order-summary']) //envio pra minha página de sumário (resumo da compra)
        console.log(`Compra concluída com sucesso! Id: ${orderId}`)
        this.orderService.clear() //depois limpo os itens pra acabar com a compra já feita!
      }) //toda vez que tenho um http e um Observable, preciso me inscrever (a string virá lá do método http no serviço)

    console.log(order) //imprimo no console só pra ver como vem minha compra
  }
}
