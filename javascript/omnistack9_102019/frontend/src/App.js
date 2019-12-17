import React, { useState } from 'react';
import api from './services/api'
import './App.css';
import logo from './assets/logo.svg';
//import Routes from './routes';

// Note que sempre que eu fizer {} dentro do HTML eu estou chamando um Javascript (então {logo} chama o javascript do import ali)

function App() {
  //aqui é como se fosse o "FORM" do Angular - useState irá setar e getar o valor da variável
  const [email, setEmail] = useState('');

  //sim, no JS é possível escrever uma função dentro de outra função
  function handleSubmit(event) {
    event.preventDefault();
    console.log(email);
  }

  return (
    <div className="container">

      <img src={logo} alt="AirCnC" />

      <div className="content">
        <p>
          Ofereça <strong>spots</strong> para programadores e encontre <strong>talentos</strong> para sua empresa.
        </p>

        <form onSubmit={handleSubmit}>
          <label htmlFor="email">EMAIL +</label>
          <input
            type="email"
            id="email"
            placeholder="Seu melhor email"
            value={email}
            onChange={event => setEmail(event.target.value)} //arrow function pra SETAR o valor de e-mail
          />

          <button className="btn" type="submit">Entrar</button>
        </form>
      </div>
    </div>
  );
}

export default App;