//nota, no momento estou manipulando json, eu poderia dar um "require" no json e manipular o arquivo sem precisar de readfile
//todavia, estamos simulando uma leitura de arquivos, como se fosse um TXT ou qualquer outra coisa... Poderia ler assim:
//const readFileAsync = require('./herois.json')

//vou importar o readFile do módulo fs (filesystem) para ler o json de heróis
//preciso converter o readFile para Promise - então importo do util o promisify
//agora eu usarei o promisify para 'converter' o readFile e writeFile para promises e trabalhar com async/await sem dor de cabeça
const { writeFile, readFile } = require('fs');
const { promisify } = require('util');
const [writeFileAsync, readFileAsync] = [
  promisify(writeFile),
  promisify(readFile),
];


//finalmente a primeira classe!
class Database {
    constructor() {
      this.FILENAME = 'heroes.json';
    }

    //método e função para obter dados
    async obterDadosArquivo(){
        //leio o arquivo
        const arquivo = await readFileAsync(this.FILENAME)
        return JSON.parse(arquivo.toString()) //converto o retorno pra json
    }
    
    async listar(id){
        //esse método vai chamar o obterDadosArquivo e depois separar por apenas nome ou algo assim (filtra)
        const dados = await this.obterDadosArquivo()
        
        //na linha abaixo eu posso fazer um filtro onde ID sempre for o que o usuário, porém irei fazer um filtro melhor
        //const dadosFiltrados = dados.filter(item => item.id === id) //eu quero que do item, você pegue o que o item.id foi igual ID (parametro)

        //filtro melhorado: quando o usuário NÃO enviar o ID, eu trarei tudo
        //o "id ?" significa pra o compilador: "Se o ID Foi passado, então você trará o que item.id for igual id passado"
        //o " : " quer dizer que "se ele não passou" eu mando true e trago a lista completa
        const dadosFiltrados = dados.filter(item => (id ? (item.id === id) : true))
        return dadosFiltrados
    }

    //método e função para cadastrar dados
    async escreveArquivo(dados){
        //vai receber informações e vai salvar informações no arquivo heroes.json
        //falo o arquivo onde vou salvar, e mando um stringify pra ele mandar um json pra salvar
        await writeFileAsync(this.FILENAME, JSON.stringify(dados));
        return true;
    }

    async cadastrar(heroi) {
        //obtenho o arquivo, modificado os dados, re-escrevo o arquivo
        const dados = await this.obterDadosArquivo() //pego os dados que já estão no arquivo (lista)
        const id = heroi.id <= 2 ? heroi.id : Date.now(); 
        //se o id foi menor ou igual a 2, usará o heroi.id, senão - id será data agora (temporário, quando fizer interface, ai faremos um ID normal)

        /** O herói virá da seguinte forma:
         * {
         * nome: Flash,
         * poder: Velocidade
         * }
         * 
         * ai eu vou criar um novo objeto, com o id:
         * {
         *  id: 123456789
         * }
         * 
         * Nosso objetivo é pegar o primeiro objeto e juntar com o segundo, e ai teremos um objeto assim:
         * 
         * {
         *  nome: Flash,
         *  poder: Velocidade,
         *  id: 123456789
         * }
         * 
         * Então utilizarei uma técnica pra CONCATENAR ESSES OBJETOS no Javascript:
         */

         const heroiComId = {
             id,
             ...heroi //ele irá concatenar o id que eu gerei lá em cima com o heroi (que veio como parametro)
         }
         const dadosFinal = [ //crio um novo array concatenando os dados que eu já tinha recebido, com o heroi com ID (novo heroi)
             ...dados,
             heroiComId
        ]
         const resultado = await this.escreveArquivo(dadosFinal) //passo o objeto final
         return resultado; //retorno pra ver se o teste vai passar...
    }

    async remover(id){
        if(!id){
            //se não houver NENHUM ID (se não for enviado): Ele vai remover todo mundo
            return await this.escreveArquivo([]) //escrevo o arquivo com um array vazio            
        }
        //carrego dados e removo o ID dentro da minha lista
        const dados = await this.obterDadosArquivo()
        const indice = dados.findIndex(item => item.id === parseInt(id)) //você vai me retornar o item.id que for igual o parametro que passei
        if(indice === -1){ //no javascript quando ele não encontra algo, retorna sempre -1
            throw Error('O seu usuário informado não existe')
        }
        dados.splice(indice, 1) //ai eu digo que apartir do meu índice (seja qual for) ele irá remover 1 único item
        return await this.escreveArquivo(dados) //o splice acima remove o item, ai eu mando escrever o arquivo de novo com o "dados" sem o item
    }


    async atualizar(id, atualizacoes) {
        //recebe o ID e as modificações que vem lá do teste (novoDado)
        const dados = await this.obterDadosArquivo();
        const indice = dados.findIndex(item => item.id === parseInt(id));//comparo se ID é válido
        if (indice === -1) {
          throw Error('heroi não existe!');//se o indice não existir
        }
    
        const atual = dados[indice];
        dados.splice(indice, 1);//splice remove da lista (a partir do índice, remova 1) (removo o item inteiro, pra depois inserir o atualizado)
    
        //workaround para remover valores undefined do objeto
        const objAtualizado = JSON.parse(JSON.stringify(atualizacoes));
        const dadoAtualizado = Object.assign({}, atual, objAtualizado);
    
        return await this.escreveArquivo([...dados, dadoAtualizado]);
      }
}

module.exports = new Database()