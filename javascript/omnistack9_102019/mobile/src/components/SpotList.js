// CRIEI UM COMPONENTE SEPARADO PARA AQUELA LISTA DE SPOTS QUE O USUÁRIO PODE SOLICITAR A RESERVA
// COMO O COMPONENTE IRÁ SE REPETIR NA TELA, EU VOU TER ELE EM UM COMPONENTE SEPARADO.

import React, { useState, useEffect } from 'react';
import { withNavigation } from 'react-navigation';
import { View, Text, StyleSheet, FlatList, Image, TouchableOpacity } from 'react-native';
import api from '../services/api';

// Nota: Props conterá uma referência para "tech" que é uma variável que estou passando da List.js
// Nota2: Como o SpotList não faz parte da minha rota, não consigo acessar o navigation por padrão (pra dar navigate)
// Então eu tenho que importar o withNavigation, e lá no fim do arquivo exportar minha classe e ai beleza!
function SpotList({ tech, navigation }) {
    //crio uma variável pra ARMAZENAR os Spots que depois usarei pra desenhar a tela
    const [spots, setSpots] = useState([]);

    useEffect(() => {
        async function loadSpots() {
            const response = await api.get('/spots', {
                //na API eu tenho um endpoint onde eu passo a tecnologia e ela me retorna os SPOTS
                params: { tech }
            })
            setSpots(response.data);
        }
        loadSpots(); //declarei uma função dentro do useEffect e agora chamo ela
        //NOTA: A Api será chamada pra cada technologia que eu tiver, pra desenhar cada componente
    }, []);

    //função pra navegar o usuário pra tela de reserva quando ele clicar num botão
    function handleNavigate(id) {
        navigation.navigate('Book', { id });
    }

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Empresas que usam <Text style={styles.bold}>{tech}</Text></Text>
            <FlatList
                style={styles.list}
                data={spots}
                keyExtractor={spot => spot._id}
                horizontal={true}
                showsHorizontalScrollIndicator={false}
                renderItem={({ item }) => (
                    <View style={styles.listItem}>
                        <Image style={styles.thumbnail} source={{ uri: item.thumbnail_url }} />
                        <Text style={styles.company}>{item.company}</Text>
                        <Text style={styles.price}>{item.price ? `R$${item.price}/dia` : 'GRATUITO'}</Text>
                        <TouchableOpacity onPress={() => handleNavigate(item._id)} style={styles.button}>
                            <Text style={styles.buttonText}>Solicitar Reserva</Text>
                        </TouchableOpacity>
                    </View>
                )}
            />
        </View>
    )
}
const styles = StyleSheet.create({
    container: {
        marginTop: 30,
    },

    title: {
        fontSize: 20,
        color: '#444',
        paddingHorizontal: 20,
        marginBottom: 15,
    },

    bold: {
        fontWeight: 'bold'
    },

    list: {
        paddingHorizontal: 20,
    },

    listItem: {
        marginRight: 15,
    },

    thumbnail: {
        width: 200,
        height: 120,
        resizeMode: 'cover',
        borderRadius: 2,
    },

    company: {
        fontSize: 24,
        fontWeight: 'bold',
        color: '#333',
        marginTop: 10,
    },

    price: {
        fontSize: 15,
        color: '#999',
        marginTop: 5
    },

    button: {
        height: 32,
        backgroundColor: '#f05a5b',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 2,
        marginTop: 15,
    },

    buttonText: {
        color: '#FFF',
        fontWeight: 'bold',
        fontSize: 15,
    },
});

export default withNavigation(SpotList);