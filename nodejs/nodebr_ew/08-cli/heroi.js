class Heroi {
  constructor({ id, nome, poder }) { //no construtor eu passo os 3 parametros, e ai devolvo a classe
    this.nome = nome;
    this.poder = poder;
    this.id = id || Date.now();
  }
}
module.exports = Heroi; //eu não dou new, por que vou retornar a classe preenchida... o new virá na instância, que passará os dados no construtor