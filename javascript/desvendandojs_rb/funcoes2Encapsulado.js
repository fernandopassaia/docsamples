//agora eu vou re-escrever o funções 2 encapsulando em uma function, pra evitar a alteração de variável global.
//como o JS não tem "private" nem "public", é preciso encapsular você mesmo pra evitar que uma variável global
//seja alterada de qualquer forma quebrando o código (como fiz no final do funcoes2.js).
//Lembrando que função é um objeto que contém um código executável:

var counter = function(){
    var value = 0; //note que agora o value está encapsulado dentro da função que está no counter
    var add = function(){
        return ++value;
    }
};


//agora se eu tentar chamar abaixo de qualquer forma eu não terei resposta alguma
//console.log(counter.value); //undefined (nota, comentei as duas linhas por que dava erro no compilador e parada, descomentar pra ver)
//console.log(counter.add()); //pior ainda, quebra o código

//Agora pra acessar o método counter eu terei que usar de Factory Function, conforme abaixo
//ou seja, eu estou dando um "return" e dessa forma expondo o resultado, ai sim vai funcionar!

var createCounter = function(){
    var value = 0; //note que agora o value está encapsulado dentro da função que está no counter
    return {
        add: function() {
            return ++value; //agora eu estou expondo ele num retorno
        }
    };
};

var counter = createCounter(); 
console.log(counter.value); //dará undefined por que não tem valor
console.log(counter.add()); //1
console.log(counter.add()); //2
console.log(counter.add()); //3
console.log(counter.add()); //4
console.log(counter.add()); //5


//uma VARIAÇÃO da maneira acima poderia ser o "Constructor Function" conforme abaixo:
var Counter = function(){
    var value = 0;
    this.add = function(){ //note que ao dizer "this" eu estou tornando isso público e expondo
        return ++value;
    }    
};

var counter2 = new Counter();
console.log(counter2.value); //dará undefined por que não tem valor
console.log(counter2.add()); //1
console.log(counter2.add()); //2
console.log(counter2.add()); //3
console.log(counter2.add()); //4
console.log(counter2.add()); //5


//Agora usando o Module Pattern, note que eu re-escrevi o Factory Function, mas a função se auto-chama no final dela.
//eu ainda criei um segundo método dentro dele que é o Reset que voltará o valor pra 0...

var createCounterModulePattern = (function(){
    var value = 0; //note que agora o value está encapsulado dentro da função que está no counter
    return {
        add: function() {
            return ++value; //agora eu estou expondo ele num retorno
        },
        reset: function() {
            value = 0;
        }
    };
})(); //aqui ela está se auto-chamando

console.log(createCounterModulePattern.value); //aqui eu só chamo pra mostrar que o "value" não é exibido, não é público
console.log(createCounterModulePattern.add()); //1
console.log(createCounterModulePattern.add()); //2
console.log(createCounterModulePattern.add()); //3
console.log(createCounterModulePattern.add()); //4
createCounterModulePattern.reset(); //chamo a função que reseta o contador
console.log(createCounterModulePattern.add()); //1



//Agora usando Revealing Module Pattern, que seria uma pequena alteração
var createCounterModulePatternRevealing = (function(){
    var _value = 0; //note que agora o value está encapsulado dentro da função que está no counter
    var _add = function() {
        return ++_value; //agora eu estou expondo ele num retorno
    };
    var _reset = function() {
        _value = 0; //tudo isso - value, add e reset agora são PRIVADOS - dica de convenção: use underline em coisas que são privadas
    };
    return {
        //agora o que eu quero REVELAR, quero que seja público, eu farei
        add: _add,
        reset: _reset //aqui eu digo os dois que eu quero revelar... tornar públicos
    };
})(); //aqui ela está se auto-chamando

console.log(createCounterModulePatternRevealing.value); //aqui eu só chamo pra mostrar que o "value" não é exibido, não é público
console.log(createCounterModulePatternRevealing.add()); //1
console.log(createCounterModulePatternRevealing.add()); //2
console.log(createCounterModulePatternRevealing.add()); //3
console.log(createCounterModulePatternRevealing.add()); //4
createCounterModulePatternRevealing.reset(); //chamo a função que reseta o contador
console.log(createCounterModulePatternRevealing.add()); //1