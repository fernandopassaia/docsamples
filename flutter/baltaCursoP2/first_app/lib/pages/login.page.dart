import 'package:flutter/material.dart';

class LoginPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        color: Color(0xFFF5F5F5),
        padding: EdgeInsets.only(
          left: 20,
          top: 80,
          right: 20,
          bottom: 40,
        ),
        child: Column(
          children: <Widget>[
            Container(
              //color: Colors.white, //tirei por que senao ia conflitar com o boxshadow - toda vez que usamos um decoration, a cor vai pra dentro dele
              height: 450,
              decoration: BoxDecoration(
                color: Colors.white,
                boxShadow: [
                  new BoxShadow(
                    color: Colors.black12,
                    offset: new Offset(1, 2.0),
                    blurRadius: 5,
                    spreadRadius: 1,
                  )
                ], //boxShadow
              ),
              child: ListView(
                children: <Widget>[
                  Column(
                    children: <Widget>[
                      Text(
                        "Welcome",
                        style: TextStyle(
                          fontSize: 30,
                          fontWeight: FontWeight.w500,
                        ),
                      ),
                      Text("SignIn to continue"),
                    ],
                  ),
                  FlatButton(
                    child: Text("Teste"),
                    onPressed: () {},
                  )
                ],
              ), //child e filho do meu container ainda...
            ),
          ],
        ),
      ),
    );
  }
}
