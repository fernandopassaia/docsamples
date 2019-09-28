const { deepEqual, ok } = require('assert'); //aqui eu posso importar apenas os métodos que vou usar //mocha - para mais documentacao veja o módulo anterior
const Database = require('./database'); //importo minha classe de banco de dados
const DEFAULT_ITEM_CADASTRAR = { nome: 'Flash', poder: 'speed', id: 1 }; //variavel global
const DEFAULT_ITEM_ATUALIZAR = {
  nome: 'Lanterna Verde',
  poder: 'Anel do poder',
  id: 2,
};



describe('Suite de Manipulação de de Herois', () => {

    before(async () => {
        //await Database.remover();
        await Database.cadastrar(DEFAULT_ITEM_CADASTRAR); //antes de tudo, se não houver nenhum item, ele vai cadastrar (iniciar a base)
        await Database.cadastrar(DEFAULT_ITEM_ATUALIZAR); //também cadastro o item que será ATUALIZADO pelo método atualizar lá embaixo
        //await Database.cadastrar(DEFAULT_ITEM_ATUALIZAR);
      });

    //it.only('deve pesquisar um herói, usando arquivos', async () => { NOTA: Se eu colocar ONLY na chamada de qualquer teste, ele roda SÓ esse...
    it('deve pesquisar um herói, usando arquivos', async () => { //objetivo
        //primeiro falo o que é esperado, segundo o processamento e terceiro a saída
        const expected = DEFAULT_ITEM_CADASTRAR        
        
        //nota sobre DESTRUCTOR em JavaScript. O Resultado acima retornará um array de resultados...
        //pra pegar um resultado específico dentro do array, eu poderia fazer o seguinte: const posicaoUm = resultado[0]
        //porém, eu posso fazer "const [resultado] = await". Quando eu coloco entre chavetas, ele entende que é o resultado na posição 0.
        //Se eu fizer const [resultado, posicao2, posicao3] ele irá entender e pegar os próximos resultados... legal né?
        const [resultado] = await Database.listar(expected.id) //chamo o meu banco de dados e pego posição 0 (ler destructor acima)        
        
        deepEqual(resultado, expected) //estou comparando se o meu "json" acima (default_item_cadastrar) é o mesmo que a classe
        //banco vai ler lá do Json... basicamente um teste... mas é legal...
    })

    //deve cadastrar um herói, usando arquivos', async () => { NOTA: Se eu colocar ONLY na chamada de qualquer teste, ele roda SÓ esse...
    it('deve cadastrar um herói, usando arquivos', async () => { //objetivo
        //primeiro falo o que é esperado, segundo o processamento e terceiro a saída
        const expected = DEFAULT_ITEM_CADASTRAR;
        await Database.cadastrar(DEFAULT_ITEM_CADASTRAR);//passo o expected pra cadastrar (nota, esse é um objeto lá em cima, poderia ser qquer um)
        //retorno os dados pra verificar se cadastrei
        const [realResult] = await Database.listar(expected.id);//mando o id pra verificar se ele está no arquivo
        //lembrando que as chaves [] indicam pra ele pegar apenas a primeira posição do array        
        deepEqual(realResult, expected);//verifico se o atual (que ele pegou no arquivo) é igual o lá de cima, se for, cadastrou!
    })

    //it.only('deve remover um heroi por id', async () => { NOTA: Se eu colocar ONLY na chamada de qualquer teste, ele roda SÓ esse...
    it('deve remover um heroi por id', async () => {
        const expected = true; //meu esperado é true, que ele remova
        const resultado = await Database.remover(DEFAULT_ITEM_CADASTRAR.id) //removo o primeiro, que eu criei automático lá no before
    })

    //it.only('deve atualizar um heroi pelo id', async () => { NOTA: Se eu colocar ONLY na chamada de qualquer teste, ele roda SÓ esse...
    it.only('deve atualizar um heroi pelo id', async() => {
                const expected = {
                    ...DEFAULT_ITEM_ATUALIZAR,
                    nome: 'Batman',
                    poder: 'Dinheiro'
                } //vou atualizar o DefaultItemAtualizar que está lá em cima, por batman e dinheiro

                const novoDado = {
                    nome: 'Batman',
                    poder: 'Dinheiro'
                }

                await Database.atualizar(DEFAULT_ITEM_ATUALIZAR.id, novoDado)
                const [resultado] = await Database.listar(DEFAULT_ITEM_ATUALIZAR.id) //depois de feito eu pego o dado pra ver se está igual
                deepEqual(resultado,expected)
    })
})