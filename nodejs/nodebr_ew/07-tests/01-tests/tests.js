const assert = require('assert') //mocha - pacote pra testes
const {
    obterPessoas //método que eu quero do serviço
} = require('./service')

const nock = require('nock') //para testar a disponibilidade da webapi

describe('Star Wars Tests Fer', function () { //describe é interno do mocha
    //nock é um pacote para SIMULAR REQUISIÇÕES sem que o javascript tenha que ir de fato no WebApi services.js
    //o código abaixo diz que sempre que a URL (lá embaixo) for chamada, ele dará esse retorno abaixo (response) que é um JSON sem ir na WebApi
    this.beforeAll(() => {
        const response = {
            count: 1,
            next: null,
            previous: null,
            results: [{
                name: 'R2-D2',
                height: '96',
                mass: '32',
                hair_color: 'n/a',
                skin_color: 'white, blue',
                eye_color: 'red',
                birth_year: '33BBY',
                gender: 'n/a',
                homeworld: 'https://swapi.co/api/planets/8/',
                vehicles: [],
                starships: [],
                created: '2014-12-10T15:11:50.376000Z',
                edited: '2014-12-20T21:17:50.311000Z',
                url: 'https://swapi.co/api/people/3/'
            }]
        } //nota: esse resultado eu estou PRINTANDO no console (só pra testes e pra copiar) lá de dentro do service.js (eu removi os array pra não dar erro de sintaxe)

        //ai eu falo pro nock que toda vez que meu usuário tentar acessar a URL (people) com os parâmetros search com o tipo r2-d2 e o formato json
        //eu quero que ele responda com o status 200 e o response que eu criei acima: A FUNÇÃO disso é para que ele não precise ficar toda hora
        //batendo no WebApi do Star Wars - ou seja: caso a URL seja essa, ele vai sempre retornar o que está acima sem ir no Service
        nock('https://swapi.co/api/people')
            .get('/?search=r2-d2&format=json')
            .reply(200, response)
    })


    //it.only('deve buscar o r2d2 com o formato correto', async () => { NOTA: Se eu colocar ONLY na chamada de qualquer teste, ele roda SÓ esse...
    // O "=>" (arrow function) significa que é uma função. Então eu poderia ter escrito "async function()"
    it('deve buscar o r2d2 com o formato correto', async() => { //nome teste: ir no star wars e trazer um mapeamento diferente do default
        const expected = [{
            nome: 'R2-D2',
            peso: 96
        }] //esse é a maneira e dados que eu espero, que vem da API

        const nomeBase = 'r2-d2' //nome que vou procurar
        const resultado = await obterPessoas(nomeBase)

        //ai eu verifico se o valor que venho de dentro do resultado é o que estou esperando
        assert.deepEqual(resultado, expected) //vejo se o resultado é igual ao expected

    })
})