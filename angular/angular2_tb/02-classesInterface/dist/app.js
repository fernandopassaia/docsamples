//Nota: Aqui está a criação da Classe, Interface e Extensão tudo dentro do mesmo arquivo.
//Na próxima aula (03-modulos) eu pegarei essa única classe bagunçada e irei criar módulos separados.
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Pessoa = /** @class */ (function () {
    //quando eu declaro algo no construtor, é como se eu criasse um atributo na classe
    function Pessoa(nome, endereco) {
        this.nome = nome;
    }
    //agora eu declaro um método (note que não preciso chamar function na frente)
    Pessoa.prototype.clienteEfetuaLigacao = function () {
        console.log("Eu " + this.nome + " estou ligando pra algu\u00E9m ");
    };
    return Pessoa;
}());
//agora eu declaro e chamo o método
var cliente = new Pessoa('Fernando Passaia', "Ezelino da Cunha Gloria");
cliente.clienteEfetuaLigacao();
//agora eu vou estender essa classe
var pessoaFisica = /** @class */ (function (_super) {
    __extends(pessoaFisica, _super);
    function pessoaFisica() {
        return _super.call(this, 'Fernando Passaia', 'Ezelino da Cunha Gloria') || this; //tenho que chamar o super e mandar a classe
    }
    //agora vou sobre-escrever o método com 50% de chances de ligar
    pessoaFisica.prototype.clienteEfetuaLigacao = function () {
        if (Math.random() >= 0.5) {
            _super.prototype.clienteEfetuaLigacao.call(this);
        }
        else {
            console.log('Não to afim de ligar pra porra nenhuma');
        }
    };
    return pessoaFisica;
}(Pessoa));
//faço um for pra chamar 10x, e ai é random, as vezes vai ligar, as vezes não
var fernandoPassaia = new pessoaFisica();
for (var i = 0; i <= 10; i++) {
    fernandoPassaia.clienteEfetuaLigacao();
}
//agora eu vou criar uma NOVA classe que implementará o Cliente Desenvolvedor
var pessoaFisicaDeTi = /** @class */ (function (_super) {
    __extends(pessoaFisicaDeTi, _super);
    function pessoaFisicaDeTi() {
        var _this = _super.call(this, 'Fernando Passaia Desenvolvedor', 'Ezelino da Cunha Gloria') //tenho que chamar o super e mandar a classe
         || this;
        _this.linguagemProgramacao = 4; //C# e TypeScript esse cara manja legal
        return _this;
    }
    //agora vou sobre-escrever o método com 50% de chances de ligar
    pessoaFisicaDeTi.prototype.clienteEfetuaLigacao = function () {
        if (Math.random() >= 0.5) {
            _super.prototype.clienteEfetuaLigacao.call(this);
        }
        else {
            console.log('Sou Programador: Não to afim de ligar pra porra nenhuma');
        }
    };
    return pessoaFisicaDeTi;
}(Pessoa));
//faço um for pra chamar 10x, e ai é random, as vezes vai ligar, as vezes não
var fernandoPassaiaProgramador = new pessoaFisicaDeTi();
for (var i = 0; i <= 10; i++) {
    fernandoPassaiaProgramador.clienteEfetuaLigacao();
}
//agora vou criar uma arrow function que dirá se o desenvolvedor é bom para o projeto
//(precisa saber pelo menos 4 linguagens) - ele dará false por que linguagemProgramacao = 2 na pessoaFisicaDeTi
//se eu aumentar e botar 4 por exemplo - ai começa a dar retorno de "Sim".
//lembrando: Lado esquerdo a declaração e lado direito a implementação (assim funciona arrow functions)
var goodForTheJob = function (profissional) { return profissional.linguagemProgramacao > 3; };
//chamada da função
console.log("O programador \u00E9 bom para o trabalho? " + (goodForTheJob(fernandoPassaiaProgramador) ? 'Sim' : 'Não'));
