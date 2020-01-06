"use strict";
exports.__esModule = true;
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
exports.Pessoa = Pessoa;
