//exemplo sem a separação correta, tudo enfiado numa classe só, só teste, depois separei tudo e mostrei bonitinho usando o index.js
class NotImplementedException extends Error {
  constructor() {
    super('Not Implemented Exception'); //simulando uma interface, como o JS não segue totalmente orientação a objeto, caso alguém chame
  }                                     //essa classe e algum método que NÃO tenha sido implementado, ela irá disparar uma exceção
}

//interface - coloquei ICrud pra simular que é uma interface
class ICrud {
  create(item) {
    throw new NotImplementedException(); //só a assinatura (contrato): ainda não implementei (insert)
  }
  read(item) {
    throw new NotImplementedException(); //select
  }
  update(id, item) {
    throw new NotImplementedException(); //update
  }
  delete(id) {
    throw new NotImplementedException(); //delete
  }
}

class MongoDBStrategy extends ICrud { //MongoDB extends (herda) de iCrud e implementa os métodos (interface pra iCrud)
  constructor() {
    super(); //toda vez que extenso alguma classe, preciso chamar seu construtor - super chama construtor iCrud
  }
  create(item) {
    console.log('MongoDBStrategy. O item foi salvo no MongoDB.'); //implementei só o create, depois precisa do resto
  }
}

class PostgreSQLStrategy extends ICrud { //Postgres extends (herda) de iCrud e implementa os métodos (interface pra iCrud)
  constructor() {
    super(); //toda vez que extenso alguma classe, preciso chamar seu construtor
  }
  create(item) {
    console.log('PostgreSQLStrategy. O item foi salvo no PostgreSQL.');
  }
}


//objetivo: Pra estratégica que eu passar no construtor, preciso implementar os 4 métodos que herdo de iCrud
//classe abstrata que vai chamar os métodos de acordo com o que foi chamado no construtor
class ContextoStrategy extends ICrud { //implemento a interface do iCrud
  constructor(database) {
    super(); //toda vez que extenso alguma classe, preciso chamar seu construtor
    this._database = database;
  }
  create(item) {
    return this._database.create(item); //chamo os métodos
  }
  read(item) {
    return this._database.read(item);
  }
  update(id, item) {
    return this._database.update(id, item);
  }
  delete(id) {
    return this._database.delete(id, item);
  }
}

const contextMongo = new ContextoStrategy(new MongoDBStrategy()); //falo que ele vai receber o mongoDB
contextMongo.create(); //instancio o objeto e chamo o create (que vai printar no console)
const context = new ContextoStrategy(new PostgreSQLStrategy()); //falo que ele vai receber o postgres
context.create(); //instancio o objeto e chamo o create (que vai printar no console)

context.read(); //aqui eu gero uma exception que o método não foi implementado (gera exception, teste, só)
