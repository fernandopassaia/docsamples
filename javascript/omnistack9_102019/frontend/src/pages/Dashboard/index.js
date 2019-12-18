import React, { useEffect, useState } from 'react';
import api from '../../services/api';
import './styles.css';
export default function Dashboard() {

    const [spots, setSpots] = useState([]); //como spots é um array no banco, eu inicializo ele com vazio

    //UseEffect é usado pra executar uma função assim que o usuário acessar essa tela (seria como o ngOnInit)
    //UseEffect é uma função - recebe dois parametros, uma função (que escrevi com ArrowFunction) e o segundo é um array de dependências
    //ou seja: eu posso botar várias variáveis no array [], e quando alguma delas sofrer alterações, ele será executado novamente...
    //pode ser usado um "filter" por exemplo: toda vez que o usuário trocar o filter na interface, ele será executado novamente.
    //se eu deixar VAZIO [] esse useEffect será usado apenas na consturção do componente: ou seja, uma BUSCA inicial na API já resolve.

    //Construção Básica: useEffect(() => {}, []);
    useEffect(() => {
        //aqui farei a chamada da API pra carregar, como os parametros estão vazios [], chamará só uma vez na construção
        //note que eu crio uma função dentro da função chamada loadSpots, e logo depois chamo ela no fim
        async function loadSpots() {
            const user_id = localStorage.getItem('user');
            const response = await api.get('/dashboard', {
                headers: { user_id }
            });
            setSpots(response.data); //jogo os spots pra minha const
            console.log(response.data);
        }
        loadSpots();
    }, []);

    return (
        <>
            <ul className="spot-list">
                {spots.map(spot => ( //vou percorrer minha lista de spots e desenhar na tela
                    <li key={spot._id}>
                        <header style={{ backgroundImage: `url(${spot.thumbnail_url})` }} />
                        <strong>{spot.company}</strong>
                        <span>{spot.price ? `R$${spot.price}/dia` : 'GRATUITO'}</span>
                    </li>
                ))}
            </ul>
        </>
    )
}