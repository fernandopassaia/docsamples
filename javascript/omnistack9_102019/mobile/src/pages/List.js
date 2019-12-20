import React, { useState, useEffect } from 'react';
import { View, Text, AsyncStorage, StyleSheet, Image } from 'react-native';
import logo from '../assets/logo.png';
import { SafeAreaView } from 'react-navigation';
import SpotList from '../components/SpotList';

export default function List() {
    const [techs, setTechs] = useState([]);

    // as tecnologias virão em formato de string separado por virgula, irei converter pra array
    // o map irá pegar cada item desse array e irá tirar o espaço após a virgula
    // useEffect(() = > {}, []) estrutura básica do useEffect, tem uma função (o que será feito) e um array de variáveis
    // se estiver vazio, ele só executará uma vez ao abrir a tela. Se tiver algo, ele iŕa re-executar caso a variável se atualize.

    //Poderia passar a propriedade assim: <SpotList tech="React" /> (mas fiz no FOR)

    useEffect(() => {
        AsyncStorage.getItem('techs').then(storagedTechs => {
            const techsArray = storagedTechs.split(',').map(tech => tech.trim());
            setTechs(techsArray);
        })
    }, []);

    return (
        <SafeAreaView>
            <Image style={styles.logo} source={logo} />
            {
                //vou fazer um FOR dentro do techs e passar um por um pro component SpotList
                techs.map(tech => <SpotList key={tech} tech={tech} />)
            }

        </SafeAreaView>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },

    logo: {
        height: 32,
        resizeMode: "contain",
        alignSelf: 'center',
        marginTop: 30
    },
});