import React, { useState } from 'react';
import api from './services/api'
import './App.css';
import logo from './assets/logo.svg';
//import Routes from './routes';

// Note que sempre que eu fizer {} dentro do HTML eu estou chamando um Javascript (então {logo} chama o javascript do import ali)

function App() {
  //aqui é como se fosse o "FORM" do Angular - useState irá setar e getar o valor da variável
  const [email, setEmail] = useState(''); //O estado do input email será sempre refletido aqui

  //sim, no JS é possível escrever uma função dentro de outra função
  async function handleSubmit(event) {
    event.preventDefault();
    //console.log(email);
    const response = await api.post('/sessions', {
      //email : email - NOTA: Sempre que a chave for o mesmo que o valor, não preciso repetir
      email
    });
    console.log(response);
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