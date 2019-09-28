//Nota: Aqui está a criação da Classe, Interface e Extensão tudo dentro do mesmo arquivo.
//Na próxima aula (03-modulos) eu pegarei essa única classe bagunçada e irei criar módulos separados.

class Pessoa{
    //quando eu declaro algo no construtor, é como se eu criasse um atributo na classe
    constructor(public nome: string, endereco: string){

    }

    //agora eu declaro um método (note que não preciso chamar function na frente)
    clienteEfetuaLigacao(){
        console.log(`Eu ${this.nome} estou ligando pra alguém `)
    }
}

//agora eu declaro e chamo o método
let cliente = new Pessoa('Fernando Passaia', "Ezelino da Cunha Gloria")
cliente.clienteEfetuaLigacao()

//agora eu vou estender essa classe
class pessoaFisica extends Pessoa{
    constructor(){
        super('Fernando Passaia', 'Ezelino da Cunha Gloria') //tenho que chamar o super e mandar a classe
    }

    //agora vou sobre-escrever o método com 50% de chances de ligar
    clienteEfetuaLigacao(){
        if(Math.random() >= 0.5){
            super.clienteEfetuaLigacao()
        }else{console.log('Não to afim de ligar pra porra nenhuma')}        
    }
}

//faço um for pra chamar 10x, e ai é random, as vezes vai ligar, as vezes não
let fernandoPassaia = new pessoaFisica()
for(let i = 0; i <=10; i++)
{
    fernandoPassaia.clienteEfetuaLigacao()
}

//agora vamos implementar uma interface para policial, apenas alguns clientes serão desenvolvedores
//então resta me dizer lá na classe se ela implementará a interface de cliente desenvolvedor
interface clienteDesenvolvedor{
    linguagemProgramacao: number //número de linguagens que esse cliente sabe...
    anosFaculdade?: number //aqui eu digo quantos anos de facul tem, é opcional (?)
}


//agora eu vou criar uma NOVA classe que implementará o Cliente Desenvolvedor
class pessoaFisicaDeTi extends Pessoa implements clienteDesenvolvedor {
    linguagemProgramacao: number
    constructor(){
        super('Fernando Passaia Desenvolvedor', 'Ezelino da Cunha Gloria') //tenho que chamar o super e mandar a classe
        this.linguagemProgramacao = 4; //C# e TypeScript esse cara manja legal
    }

    //agora vou sobre-escrever o método com 50% de chances de ligar
    clienteEfetuaLigacao(){
        if(Math.random() >= 0.5){
            super.clienteEfetuaLigacao()
        }else{console.log('Sou Programador: Não to afim de ligar pra porra nenhuma')}        
    }
}

//faço um for pra chamar 10x, e ai é random, as vezes vai ligar, as vezes não
let fernandoPassaiaProgramador = new pessoaFisicaDeTi()
for(let i = 0; i <=10; i++)
{
    fernandoPassaiaProgramador.clienteEfetuaLigacao()
}

//agora vou criar uma arrow function que dirá se o desenvolvedor é bom para o projeto
//(precisa saber pelo menos 4 linguagens) - ele dará false por que linguagemProgramacao = 2 na pessoaFisicaDeTi
//se eu aumentar e botar 4 por exemplo - ai começa a dar retorno de "Sim".
//lembrando: Lado esquerdo a declaração e lado direito a implementação (assim funciona arrow functions)
let goodForTheJob = ( profissional: pessoaFisicaDeTi ) => profissional.linguagemProgramacao > 3

//chamada da função
console.log(`O programador é bom para o trabalho? ${goodForTheJob (fernandoPassaiaProgramador) ? 'Sim': 'Não'}`)