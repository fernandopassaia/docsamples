"use strict";
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
exports.__esModule = true;
var base_pessoa_1 = require("./base-pessoa");
//como eu tenho uma única classe posso usar o export dessa forma
var profissional = /** @class */ (function (_super) {
    __extends(profissional, _super);
    function profissional() {
        var _this = _super.call(this, 'Fernando Passaia Desenvolvedor', 'Ezelino da Cunha Gloria') //tenho que chamar o super e mandar a classe
         || this;
        _this.rg = 349115084;
        _this.cpf = 32533832880;
        _this.nivelExperiencia = 5; //profissional mediano
        return _this;
    }
    //agora vou sobre-escrever o método com 50% de chances de ligar
    profissional.prototype.clienteEfetuaLigacao = function () {
        if (Math.random() >= 0.5) {
            _super.prototype.clienteEfetuaLigacao.call(this);
        }
        else {
            console.log('Sou Programador: Não to afim de ligar pra porra nenhuma');
        }
    };
    return profissional;
}(base_pessoa_1.Pessoa));
exports.profissional = profissional;
