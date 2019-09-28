import { Component, OnInit, Input } from '@angular/core'; //importei input pra permitir que minhas propriedades possam receber valores do parent
import {Student} from './student.model' //importo um modelo, dessa forma passarei uma forma mais complexa (array ou algo2)

@Component({
  selector: 'cli-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  //name: string = 'Fernando Passaia'
  //programador: boolean = true

  //com o Input é possível que esses atributos recebam valores do parent (lá da View)
  //tirei os valores padrões (inicialização) por que eles serão passados pelo template
  // @Input() name: string
  // @Input() programador: boolean

  //mudei os dois parametros acima pra um modelo mais complexo, como a interface
  @Input() student: Student

  constructor() { }

  ngOnInit() {
  }

  clicado(){
    console.log(`Botão clicado. Estudante: ${this.student.name}`)
  }
}