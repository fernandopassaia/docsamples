import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { RadioOption } from './radio-option.model';

//CONTROL VALUE ACCESSOR: Na Aula 64 eu vou implementar a Interface ControlValueAccessor. Essa interface dará
//poderes ao meu componente (order) para conseguir acessar as diretivas de Formulário NgModel ou de Reactive Forms.
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms'

@Component({
  selector: 'mt-radio',
  templateUrl: './radio.component.html',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    //registro meu componente como ValueAcessor para ser acessado pelas diretivas ngModel e outras do Reactive Forms (no html do order associei o meu radio a esse)
    useExisting: forwardRef(() => RadioComponent), 
    multi:true
  }]
})
export class RadioComponent implements OnInit, ControlValueAccessor {

  //nota: esse RadioOption[] que são as opções de pagamento serão passados pelo componente principal (order.component.ts)
  @Input() options: RadioOption[] //irei armazenar as opções de pagamento, como elas vem de fora, irei marcar com input
  value: any
  OnChange: any

  constructor() { }  

  ngOnInit() {
  }

  setValue(value: any){
    this.value = value
    this.OnChange(this.value)
  }


  //métodos relativos a implementação da interface ControlValueAccessor (ver img51)
  
    writeValue(obj: any): void{
      //esse método é chamdo pelas diretivas quando elas querem passar um valor para o seu componente
      this.value = obj //o valor recebe o objeto passado no parâmetro
    }
    
    registerOnChange(fn: any): void{
      //eu tenho que chamar essa função toda vez que o parametro interno do componente mudar
      //dessa forma eu aviso as direitivas que usam meu componente - que o valor do componente mudou.
      this.OnChange = fn
    }
    
    registerOnTouched(fn: any): void{ }//esse método só se eu quiser registrar que o usuário entrou no meu componente
    
    // * @param isDisabled     
    setDisabledState?(isDisabled: boolean): void{ }




}
