"use strict";
exports.__esModule = true;
var base_pessoa_1 = require("./base-pessoa");
var profissionais_1 = require("./profissionais");
var _ = require("lodash"); //estou importando "tudo" - namespace import (completo)
console.log(_.pad("Typescript Examples", 40, "=")); //testando o lodash botando um cabeçalho
//agora eu declaro e chamo o método
var cliente = new base_pessoa_1.Pessoa('Fernando Passaia', "Ezelino da Cunha Gloria");
cliente.clienteEfetuaLigacao();
var fernandoPassaiaProgramador = new profissionais_1.profissional();
for (var i = 0; i <= 10; i++) {
    fernandoPassaiaProgramador.clienteEfetuaLigacao();
}
var goodForTheJob = function (profissional) { return profissional.nivelExperiencia > 5; };
//chamada da função
console.log("O programador \u00E9 bom para o trabalho? " + (goodForTheJob(fernandoPassaiaProgramador) ? 'Sim' : 'Não'));
