const Spot = require('../models/Spot')

module.exports = {
    async store(req, res) {
        //console.log(req.body);
        //console.log(req.file);

        const { filename } = req.file; // pego o arquivo (ibagem)
        const { company, techs, price } = req.body; // pego esses campos do Body (XML)
        const { user_id } = req.headers; // pego do cabeçalho (mesmo lugar manda JWT)

        //envio o SPOT pro banco de dados...
        const spot = await Spot.create({
            user: user_id,
            thumbail: filename,
            company,
            //percorro o que vier na string separando por , (pra formar um array) e tiro os espaços de um por um com trim
            techs: techs.split(',').map(tech => tech.trim()),
            price
        })

        return res.json(spot);
    }
}