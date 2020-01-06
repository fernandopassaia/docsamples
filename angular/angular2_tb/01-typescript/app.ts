let message: string = "Testando string"
console.log(message)

let episode: number = 4
console.log("This number is: " + 4)
episode = episode + 1
console.log("New number: " + episode)

let favoriteDroid //como não falei qual tipo é, ele vai colocar como any
favoriteDroid = 'Testando Variavel sem tipagem'
console.log(favoriteDroid)
favoriteDroid = 10 //aqui eu simplesmente mudo o tipo, e não dará nenhum warning
console.log(favoriteDroid)