const IDb = require('./interfaceDb'); //importo o arquivo de interface por que ele também segue
class ContextStrategy extends IDb {
  constructor(database) {
    super(); //toda vez que derivo de uma classe, chamo o super - isso quer dizer: chame a classe iDb
    this._database = database;
  }
  isConnected() {
    return this._database.isConnected();
  }
  create(item) {
    return this._database.create(item);
  }
  read(item) {
    return this._database.read(item);
  }
  update(id, item) {
    return this._database.update(id, item);
  }
  delete(id) {
    return this._database.delete(id);
  }
}

module.exports = ContextStrategy; //exporto, é como se fosse um public