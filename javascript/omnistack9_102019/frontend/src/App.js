import React from 'react';
import './App.css';
import logo from './assets/logo.svg';
//import Routes from './routes';

// Note que sempre que eu fizer {} dentro do HTML eu estou chamando um Javascript (então {logo} chama o javascript do import ali)

function App() {
  return (
    <div className="container">

      <img src={logo} alt="AirCnC" />

      <div className="content">

      </div>
    </div>
  );
}

export default App;