//Fernando: Criei minha própria classe pra criar arrays, pra filtrar, pra imprimir, pra manipular listas
//Os Exemplos do Erick Wendel não estavam muito claros pra mim, então vou eu mesmo criar na mão.

const nomes = ['Fernando', 'Jaqueline', 'Hubinha', 'Marina', 'Ana', 'Vitor', 'Fernanda']

const copiaComFor = []
const copiaComForEach = []

//varrendo com for
for(let i=0; i<nomes.length; i++){
    copiaComFor.push(nomes[i])
}

//varrendo com foreach
nomes.forEach(function(item){
    copiaComForEach.push(item)
})

console.log('copiado com for:', copiaComFor)
console.log('copiado com forEach:', copiaComForEach)
console.log(' ')

//escrevendo o indice no console
for(let i=0; i<10; i++){
    console.log('Posição: ', i)
}

const contador = 0;
for(let i=0; i<10; i++){
    console.log('Incrementando uma variavel: ', contador)
    contador = contador + 1;
}