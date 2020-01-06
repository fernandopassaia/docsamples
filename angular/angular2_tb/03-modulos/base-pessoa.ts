class Pessoa{
    //quando eu declaro algo no construtor, é como se eu criasse um atributo na classe
    constructor(public nome: string, endereco: string){

    }

    //agora eu declaro um método (note que não preciso chamar function na frente)
    clienteEfetuaLigacao(){
        console.log(`Eu ${this.nome} estou ligando pra alguém `)
    }
}

//agora vamos criar uma classe para pessoa física, só essa terá CPF e RG
interface pessoaFisica{
    cpf: number
    rg: number
    nivelExperiencia: number
}

//agora vamos criar uma classe para pessoa jurídica, só essa terá CNPJ
interface pessoaJuridica{
    cnpj: number
    ie: number
}

export{ Pessoa, pessoaFisica, pessoaJuridica}