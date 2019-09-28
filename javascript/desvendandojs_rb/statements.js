//for, if, else, continue, while, switch, break, throw, try e catch

//nós vamos criar uma função "toHackerCase" que pegará um texto normal e transformará
//alguns dos caracteres por números e sinais de pontuação

//string text = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
var text = "Anow Anow May May Habib Habib Riiiioooo";


var toHackerCase = function(textV){
    for(var i = 0; i < textV.lenght; i++){
        console.log(textV.charAt(i));
    }
};

toHackerCase(text);
console.log(text.length);