// npm i pg
// npm install --save sequelize pg-hstore pg

// const { Client } = require('pg');
// const client = new Client({
//   database: 'degfe1gjfh80m8',
//   host: 'ec2-54-163-246-5.compute-1.amazonaws.com',
//   port: 5432,
//   password: 'fea27e438e77e507f6a31e6d8bcc4d8642c88c78b2c7dcc0ec6351d513f43ca8',
//   user: 'vwgytcowhvcjug',
//   ssl: true,
// });
// (async () => {
//   const r = await client.connect();
//   console.log('conectado!');
//   const res = await client.query('SELECT * FROM TB_HEROIS');

//   console.log(res.rows); // Hello world!
//   await client.end();
// })();


//Na pasta do projeto (10-multi-db-postgres) eu instalei o Sequelize (ORM): npm install sequelize.
//Depois eu instalei os Drivers do Postgres: npm install pg-hstore pg

const Sequelize = require('sequelize');
const sequelize = new Sequelize(
  'heroes', //database
  'fernandopassaia', // user
  '@1234Fd@', //senha
  {
    host: 'localhost',
    dialect: 'postgres',
    // case sensitive
    quoteIdentifiers: false,
    // deprecation warning
    operatorsAliases: false

    // dialectOptions: {
    //   ssl: true,
    // },
  },
);


(async () => {
    //vou definir um modelo e falar como minha tabela vai se comportar
    const Herois = sequelize.define(
      'herois',//defino um modelo, a tabela heróis
      {//isso aqui é praticamente um MAP no EF
        id: {
          type: Sequelize.INTEGER,
          required: true,
          primaryKey: true,
          autoIncrement: true,
        },
        nome: {
          type: Sequelize.STRING,
          required: true,
        },
        poder: {
          type: Sequelize.STRING,
          required: true,
        },
      },
      {
        //opcoes para base existente - agora eu tenho que informar os dados da tabela existente, senão ele vai querer criar outra
        tableName: 'TB_HEROIS',
        freezeTableName: false,//não altere a tabela
        timestamps: false,//não crie os campos de novo
      },
    );
  
    // force: true will drop the table if it already exists
    await Herois.sync();//mandei sincronizar
    // Abaixo eu paro de ficar gravando o heroi john toda hora... se quiser gravar só descomentar
    // const result = await Herois.create({
    //   nome: 'John',
    //   poder: 'Hancock',
    // });
    console.log(
      'result',
      await Herois.findAll({ raw: true, attributes: ['nome', 'poder', 'id'] }),//agora vou tentar listar o que já tem lá... mando trazer só nome, poder e id
    );
  })();