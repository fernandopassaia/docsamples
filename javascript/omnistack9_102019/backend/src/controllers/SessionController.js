// SessionController é pra controlar a cessão do usuário. Como o usuário loga apenas
// com o e-mail, eu não tratarei esse controller como usuário, mas sim como sessão
// index, show, store, update, destroy
// index lista todos
// show lista só um
// store cria
// update atualiza
// destroy deleta

const User = require('../models/User')

module.exports = {

    async store(req, res) {
        //const email = req.body.email;
        const { email } = req.body; // nota: esse é um recursos chamado "desestruturação", o Javascript entenderá que deve procurar "email" dentro do body

        //crio um objeto do tipo usuário, passando o e-mail (note que mandei o banco de dados esperar)
        const user = await User.create({ email });
        return res.json(user);
    }

}