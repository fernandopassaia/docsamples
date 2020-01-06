class NotImplementedException extends Error {
  constructor() {
    super('Not Implemented Exception'); //toda vez que derivo de uma classe, chamo o super - isso quer dizer: chame a classe Error
  }
}
//interface
class IDb {
  create(item) {
    throw new NotImplementedException();
  }
  read(item) {
    throw new NotImplementedException();
  }
  update(id, item) {
    throw new NotImplementedException();
  }
  delete(id) {
    throw new NotImplementedException();
  }
  isConnected(id) {
    throw new NotImplementedException();
  }
}

module.exports = IDb; //exporto, é como se fosse um public
