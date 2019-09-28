import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'mt-delivery-costs',
  templateUrl: './delivery-costs.component.html'
})
export class DeliveryCostsComponent implements OnInit {

  //no template eu tenho valor dos itens, frete e total
  //como vou precisar informar por um componente externo, sempre uso input
  @Input() delivery: number
  @Input() itemsValue: number

  constructor() { }

  ngOnInit() {
  }

  //retorno o total que é delivery (entrega)+itens
  total(): number {
    return this.delivery + this.itemsValue
  }
}