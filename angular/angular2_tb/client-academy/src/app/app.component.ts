import { Component } from '@angular/core';
import {Student} from './student/student.model'


@Component({
  selector: 'cli-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client-academy';

  //Exemplo 2: aqui eu crio 3 objetos de estudante para o meu primeiro exemplo na tela...
  fernando = {name: 'Fernando Passaia', programador: true, universidade: 'UMC'}
  jaqueline = {name: 'Jaqueline', programador: true, universidade: 'UBC'}
  hubinha = {name: 'Hubinha', programador: false} //hubinha não passei universidade

  //Exemplo 3: agora eu refatoro esse código fazendo um ARRAY de estudantes, ai sim mais Senioridade
  studentsArray: Student[] = [
    {name: 'Fernando Passaia no Array', programador: true, universidade: 'UMC'},
    {name: 'Jaqueline no Array', programador: true, universidade: 'UBC'},
    {name: 'Hubinha no Array', programador: false}, //hubinha não passei universidade
    {name: 'Outro no Array', programador: true, universidade: 'UCDB'}, //hubinha não passei universidade
    {name: 'Mais um no Array', programador: false}, //hubinha não passei universidade
    {name: 'KingKong do Array', programador: false}, //hubinha não passei universidade
    {name: 'Universitário no Array', programador: true, universidade: 'Uniacidez'} //hubinha não passei universidade
  ]
}
