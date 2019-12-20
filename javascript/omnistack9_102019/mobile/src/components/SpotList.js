// CRIEI UM COMPONENTE SEPARADO PARA AQUELA LISTA DE SPOTS QUE O USUÁRIO PODE SOLICITAR A RESERVA
// COMO O COMPONENTE IRÁ SE REPETIR NA TELA, EU VOU TER ELE EM UM COMPONENTE SEPARADO.

import React, { useEffect } from 'react';
import { View, Text } from 'react-native';
import api from '../services/api';

// Nota: Props conterá uma referência para "tech" que é uma variável que estou passando da List.js
export default function SpotList({ tech }) {
    useEffect(() => {
        async function loadSpots() {
            const response = await api.get('/spots', {
                //na API eu tenho um endpoint onde eu passo a tecnologia e ela me retorna os SPOTS
                params: { tech }
            })

            console.log(response.data);
        }

        loadSpots(); //declarei uma função dentro do useEffect e agora chamo ela
        //NOTA: A Api será chamada pra cada technologia que eu tiver, pra desenhar cada componente
    }, []);

    return <Text>{tech}</Text>
}