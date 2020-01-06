import{Pessoa,pessoaFisica} from './base-pessoa'
import { profissional } from "./profissionais";
import * as _ from 'lodash' //estou importando "tudo" - namespace import (completo)

console.log(_.pad("Typescript Examples", 40, "=")) //testando o lodash botando um cabeçalho

//agora eu declaro e chamo o método
let cliente = new Pessoa('Fernando Passaia', "Ezelino da Cunha Gloria")
cliente.clienteEfetuaLigacao()

let fernandoPassaiaProgramador = new profissional()
for(let i = 0; i <=10; i++)
{
    fernandoPassaiaProgramador.clienteEfetuaLigacao()
}

let goodForTheJob = ( profissional: pessoaFisica ) => profissional.nivelExperiencia > 5
//chamada da função
console.log(`O programador é bom para o trabalho? ${goodForTheJob (fernandoPassaiaProgramador) ? 'Sim': 'Não'}`)