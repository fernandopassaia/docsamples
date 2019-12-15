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
});

modules.exports = mongoose.model('Spot', SpotSchema);