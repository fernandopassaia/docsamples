//SPOT vai guardar a SALA que será alugada

const mongoose = require('mongoose');

const SpotSchema = new mongoose.Schema({
    thumbnail: String, //URL of the image that will be saved
    company: String,
    price: Number,
    techs: [String], //tecnologias que a empresa trabalha, C#, Java, Ruby, JavaScript, Angular...
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'User' //aqui eu digo que user é um objeto do tipo ID, e referencio a tabela USER
    }
}, {
    toJSON: {
        virtuals: true, //digo pra ele calcular os virtuals (que voltará a URL abaixo)
    },
});

//isso irá criar um campo VIRTUAL, ele não será salvo no BD, mas retornará com a imagem
SpotSchema.virtual('thumbnail_url').get(function () {
    return `http://localhost:3333/files/${this.thumbnail}` //nota: haverá uma ROTA pra retornar essa imagem
});

module.exports = mongoose.model('Spot', SpotSchema);