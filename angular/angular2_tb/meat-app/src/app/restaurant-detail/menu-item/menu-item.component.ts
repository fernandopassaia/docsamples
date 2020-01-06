import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core'; //eventEmiter e OutPut pro método de inserir produto
import {trigger, state, style, transition, animate} from '@angular/animations'
import { MenuItem } from './menu-item.model';

@Component({
  selector: 'mt-menu-item',
  templateUrl: './menu-item.component.html'
})
export class MenuItemComponent implements OnInit {

  //como vou precisar informar por um componente externo, sempre uso input
  @Input() menuItem: MenuItem //nota: sempre que houver uma propriedade que o PARENT irá passar pra mim, preciso do input
  @Output() add = new EventEmitter() //todos meus eventos são marcados como output

  constructor() { }
  ngOnInit() {
  }

  //método que será chamado ao clicar no botão de adicionar (ver componente - view) - aqui eu emito um aviso pro meu component PARENT que um menu item foi clicado.
  // Então ele poderá tomar uma ação e fazer alguma coisa. Meu componente interno (filho) não precisa se preocupar com isso - ele só precisa notificar o pai
  //(Restaurants.Service) e mandar o objeto clicado (menuItem). O SERVICE irá receber essa ação, junto com o objeto e tomar a providência de gravar isso.
  emitAddEvent(){
    this.add.emit(this.menuItem)
  }
}
