import 'package:flutter/material.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Alcool ou Gasolina',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.purple,
      ),
      home: HomePage(),
    );
  }
}

class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: Theme.of(context).primaryColor,
        body: ListView(
          children: <Widget>[
            SizedBox(
              height: 60,
            ),
            Image.asset(
              "assets/images/aog-white.png",
              height: 80,
            ),
            SizedBox(
              height: 10,
            ),
            Text(
              "Álcool ou Gasolina",
              style: TextStyle(
                color: Colors.white,
                fontSize: 25,
                fontFamily: "Big Shoulders Display",
              ),
              textAlign: TextAlign.center,
            ),
            SizedBox(
              height: 20,
            ),
          ],
        ));
  }
}
