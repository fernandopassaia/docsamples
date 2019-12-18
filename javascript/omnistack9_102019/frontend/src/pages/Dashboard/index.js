import React, { useEffect } from 'react';
import api from '../../services/api';

export default function Dashboard() {

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

            console.log(response.data);
        }

        loadSpots();
    }, [])

    return <div />
}