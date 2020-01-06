import { Component, OnInit, Input, ContentChild, AfterContentInit } from '@angular/core';
import { NgModel, FormControlName } from '@angular/forms' //FormControlName para Refactoramento para Reactive Forms

@Component({
  selector: 'mt-input-container', //mudei o nome do meu componente
  templateUrl: './input.component.html'
})
export class InputComponent implements OnInit, AfterContentInit {

  input: any //essa variável irá armazenar o NOME do input lá no componente ao invés do iptAdress que estava antes
  @Input() label: string //pra poder passar o valor pra essa variável preciso decorar (decorate) com o input
  @Input() errorMessage: string 

  @ContentChild(NgModel) model: NgModel //o angular vai precisar injetar uma referência nessa diretiva pra mim
  @ContentChild(FormControlName) control: FormControlName //para puxar usando ReactiveForms

  constructor() { }

  ngOnInit() {
  }
  
  //esse método será chamado quando o conteúdo que for ficar no lugar de ngContent for definido
  //nota: Esse método foi "adaptado" pra responder ao model (Templates Forms) e ao control (Reactive Forms)
  ngAfterContentInit(){
    this.input = this.model || this.control
    //vou chegar se o componente informado existe a tag ngModel
    if(this.input === undefined){
      throw new Error('Esse componente precisa ser usado com uma diretiva ngModel ou formControlName')
    }
  }

  hasSuccess(): boolean{
    return this.input.valid && (this.input.dirty || this.input.touched) //usado no componente pra mostrar se teve sucesso
  }

  hasError(): boolean{
    return this.input.invalid && (this.input.dirty || this.input.touched) //usado no componente pra mostrar se teve erro
  }

}
