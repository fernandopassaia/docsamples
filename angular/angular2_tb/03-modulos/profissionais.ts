import{Pessoa,pessoaFisica} from './base-pessoa'

//como eu tenho uma única classe posso usar o export dessa forma
export class profissional extends Pessoa implements pessoaFisica {
    rg: number
    cpf: number
    nivelExperiencia: number
    constructor(){
        super('Fernando Passaia Desenvolvedor', 'Ezelino da Cunha Gloria') //tenho que chamar o super e mandar a classe
        this.rg = 349115084;
        this.cpf = 32533832880
        this.nivelExperiencia = 5 //profissional mediano
    }

    //agora vou sobre-escrever o método com 50% de chances de ligar
    clienteEfetuaLigacao(){
        if(Math.random() >= 0.5){
            super.clienteEfetuaLigacao()
        }else{console.log('Sou Programador: Não to afim de ligar pra porra nenhuma')}        
    }
}