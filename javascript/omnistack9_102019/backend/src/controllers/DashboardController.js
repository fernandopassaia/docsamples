const Spot = require('../models/Spot');

module.exports = {
    // irá listar as reservas feitas por um usuário
    async show(req, res) {
        const { user_id } = req.headers;
        const spots = await Spot.find({ user: user_id });
        return res.json(spots);
    }
}