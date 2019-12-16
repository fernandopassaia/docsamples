//SPOT vai guardar a SALA que será alugada

const mongoose = require('mongoose');

const BookingSchema = new mongoose.Schema({
    date: String,
    approved: Boolean, // if null not decide, else - true or false    
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'User' // a reserva tem um usuário
    },
    spot: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Spot' // a reserva tem um lugar
    }
});

module.exports = mongoose.model('Booking', BookingSchema);