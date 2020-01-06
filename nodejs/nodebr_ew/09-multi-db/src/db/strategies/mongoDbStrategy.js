const IDb = require('./base/interfaceDb'); //chamo a interface do Banco de Dados (pasta strategies/base)
class MongoDBStrategy extends IDb {
  constructor() {
    super(); //toda vez que derivo de uma classe, chamo o super - isso quer dizer: chame a classe iDb
  }
  create(item) {
    return 'MongoDB';
  }
}

module.exports = MongoDBStrategy;
