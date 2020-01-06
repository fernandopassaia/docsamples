const Spot = require('../models/Spot');
const User = require('../models/User');

module.exports = {
    // retorna os spots baseados em uma tecnologia específica
    async index(req, res) {
        const { tech } = req.query; //retiro a query da query
        // mesmo tech sendo uma string e techs um array, o mongo entende a query
        const spots = await Spot.find({ techs: tech });
        return res.json(spots);
    },

    // grava um new spot
    async store(req, res) {
        //console.log(req.body);
        //console.log(req.file);

        const { filename } = req.file; // pego o arquivo (ibagem)
        const { company, techs, price } = req.body; // pego esses campos do Body (XML)
        const { user_id } = req.headers; // pego do cabeçalho (mesmo lugar manda JWT)

        const user = await User.findById(user_id);
        if (!user) {
            return res.status(400).json({ error: 'User does not exists.' });
        }

        //envio o SPOT pro banco de dados...
        const spot = await Spot.create({
            user: user_id,
            thumbnail: filename,
            company,
            //percorro o que vier na string separando por , (pra formar um array) e tiro os espaços de um por um com trim
            techs: techs.split(',').map(tech => tech.trim()),
            price
        })

        return res.json(spot);
    }
}