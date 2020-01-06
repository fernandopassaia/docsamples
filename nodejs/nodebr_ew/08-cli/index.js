//aqui eu vou criar um console que irá interagir com o usuário e alterar informações no DB (heroes.json)

const Commander = require('commander')
const DataBase = require('./database')
const Heroi = require('./heroi')

async function main(){
        Commander
        .version('v1')        

        //preciso cadastrar o usuário, para isso pego alguns parametros
        .option('-n, --nome [value]', 'adicionar nome') //-n ou --nome é o parametro que o usuário vai usar: node index.js -n Cornholio -p Voar
        .option('-p, --poder [value]', 'adicionar poder')//se eu chamar index.js --help ele mostrará as opções de acordo com o que coloquei aqui
        //CRUD
        .option('-c, --cadastrar', 'cadastrar Heroi')
        .option('-r, --listar [value]', 'listar herois pelo id')
        .option('-u, --atualizar [value]', 'atualizar heroi pelo id')
        .option('-d, --remover [value]', 'remover heroi pelo id')

        .parse(process.argv)

        const heroi = new Heroi(Commander) //dentro do construtor ele irá separar as tags e mandar apenas o heroi

        try{

            /**
             * node cli.js --cadastrar params...
             * node cli.js -c -n Hulk -p Forca
             */
            if(Commander.cadastrar){
                delete heroi.id //como se eu excluisse a chave no heroi, pra evitar pau lá no método cadastrar
                const resultado = await DataBase.cadastrar(heroi)
                if(!resultado){ //cadastrar retorna true ou false
                    console.error("Deu ruim")
                    return;
                }
                console.log('Cadastrado com sucesso!')
            }    
            
            /**
             * node cli.js --listar
             * node cli.js -r
             * node cli.js -r 1
             */
            if (commander.listar) {
                const id = commander.listar;
                const result = await Database.listar(id);
                console.log(result);
                return;
            }
        
            /**
             * node cli.js --atualizar
             * node cli.js -u 1 -n papa
             * node cli.js -u 1 -n thor -p trovao
             */
            if (commander.atualizar) {
                // const id = commander.atualizar;
                // console.log('id', id);
                // await Database.atualizar(id, heroi);
                // console.log('item atualizado com sucesso!');
                // return;

                const idParaAtualizar = parseInt(Commander.atualizar)
                //remover todas as chaves que estiverem com undefined | null
                const dado = JSON.stringify(heroi)
                const heroiAtualizar = JSON.parse(dado)
                const resultado = await DataBase.atualizar(idParaAtualizar,heroiAtualizar)
                if(!resultado){
                    console.error('Nao foi possível atualizar')
                    return
                }
                console.log('Atualizado com sucesso!')
            }
            /**
             * node cli.js --remover
             * node cli.js -d 1
             */
            if (commander.remover) {
                const id = commander.remover;
                await Database.remover(id);
                console.log('item removido com sucesso!');
                return;
            }
    }
    catch{
        console.error('DEU RUIM CARAI', error)
    }
}

main()