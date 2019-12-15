const mongoose = require('mongoose');

const UserSchema = new mongoose.Schema({
    email: String,
});

//here I'll export it and mongo will know that should create
module.exports = mongoose.model('User', UserSchema);