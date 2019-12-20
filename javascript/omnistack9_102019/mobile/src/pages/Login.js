import React, { useState, useEffect } from 'react'; //funciona do mesmo jeito que no ReactWeb
import { View, KeyboardAvoidingView, AsyncStorage, Platform, Image, Text, TextInput, StyleSheet, TouchableOpacity } from 'react-native';
import api from '../services/api';
import logo from '../assets/logo.png';

// Existem duas maneiras de carregar ibagens:
// <Image source={logo} /> 
// <Image source={require("./images/logo.png")} />

//Keyboard type vai mostrar o teclado com ARROBA pra digitar email AutoCapitalize = false não vai tentar corrigir por que é um campo de email
//AutoCapitalize = words irá deixar cada letra inicial maíusculo. AutoCorrect desabilita correção automática (útil no campo email)

//KeyboardAvoidingView joga todo o layout pra CIMA quando você clica num campo e o teclado sobe (no IOS isso não é default, no Android sim)
//onChangeText={setEmail} é a mesma coisa que escrever onChangeText={text => setEmail(text)}

//navigation faz a mesma coisa que o history no ReactWeb - navega o usuário pra outra tela
export default function Login({ navigation }) {
    const [email, setEmail] = useState('');
    const [techs, setTechs] = useState('');

    // usarei o useEffect() pra executar uma ação assim que o usuário chegar na tela, ou quando algum estado mudar (doc melhor no ReactWeb)
    useEffect(() => {
        //nota: eu irei verificar se o cara já tem um usuário LOGADO no APP, se tiver, mandarei ele direto pro List
        AsyncStorage.getItem('user').then(user => {
            if (user) {
                navigation.navigate('List');
            }
        })
    }, []);


    async function handleSubmit() {
        // preciso pegar o email e as tecnologias
        const response = await api.post('/sessions', {
            email //envio o e-mail do usuário no corpo
        })

        const { _id } = response.data; //se der certo pego o id do usuário
        //console.log(_id); //irá printar o ID de usuário no console

        // Salvo no localStorage o ID do usuário e as tecnologias que ele tem interesse
        await AsyncStorage.setItem('user', _id);
        await AsyncStorage.setItem('techs', techs);

        navigation.navigate('List');
    }

    return (
        <KeyboardAvoidingView enabled={Platform.OS === 'ios'} behavior="padding" style={styles.container}>
            <Image source={logo} />

            <View style={styles.form}>
                <Text style={styles.label}>SEU EMAIL *</Text>
                <TextInput
                    style={styles.input}
                    placeholder="Seu email"
                    placeholderTextColor="#999"
                    keyboardType="email-address"
                    autoCapitalize="none"
                    autoCorrect={false}
                    value={email}
                    onChangeText={setEmail}
                />

                <Text style={styles.label}>TECNOLOGIAS *</Text>
                <TextInput
                    style={styles.input}
                    placeholder="Tecnologias de interesse"
                    placeholderTextColor="#999"
                    autoCapitalize="words"
                    autoCorrect={false}
                    value={techs}
                    onChangeText={setTechs}
                />

                <TouchableOpacity onPress={handleSubmit} style={styles.button}>
                    <Text style={styles.buttonText}>Encontrar spots</Text>
                </TouchableOpacity>
            </View>
        </KeyboardAvoidingView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    },

    form: {
        alignSelf: 'stretch',
        paddingHorizontal: 30,
        marginTop: 30,
    },

    label: {
        fontWeight: 'bold',
        color: '#444',
        marginBottom: 8,
    },

    input: {
        borderWidth: 1,
        borderColor: '#ddd',
        paddingHorizontal: 20,
        fontSize: 16,
        color: '#444',
        height: 44,
        marginBottom: 20,
        borderRadius: 2
    },

    button: {
        height: 42,
        backgroundColor: '#f05a5b',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 2,
    },

    buttonText: {
        color: '#FFF',
        fontWeight: 'bold',
        fontSize: 16,
    }
});