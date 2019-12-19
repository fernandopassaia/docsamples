import React from 'react';
import { View, Image, Text, StyleSheet } from 'react-native';
import logo from '../assets/logo.png';

// Existem duas maneiras de carregar ibagens:
// <Image source={logo} /> 
// <Image source={require("./images/logo.png")} />

export default function Login() {
    return (
        <View style={StyleSheet.container}>
            <Image source={logo} />
            <Text style={styles.label}>SEU EMAIL *</Text>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center'
    }
});