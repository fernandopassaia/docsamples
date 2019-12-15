const multer = require('multer');
const path = require('path');

// Nota: O Path.Resolve resolve a questão de / ou \ nos sistemas operacionais (no Windows é de um jeito, no Linux de outro)
// Ele irá pegar o diretório atual (dirname) e descer dois diretórios com .. .. e depois achar a pasta uploads

module.exports = {
    storage: multer.diskStorage({
        destination: path.resolve(__dirname, '..', '..', 'uploads'),
        filename: (req, file, cb) => {
            //file é o nome do arquivo, como ele será formado - cb é o call-back, é o retorno da função
            //isso irá gerar algo como "nomeimagem1576412339.jpg"
            const ext = path.extname(file.originalname);
            const name = path.basename(file.originalname, ext);

            cb(null, `${name}-${Date.now()}${ext}`) //vai pegar o nome do arquivo + o timestamp
        },
    }),
};