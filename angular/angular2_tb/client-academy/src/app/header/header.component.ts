//Componentes:
//Componentes são pequenas partes reusáveis. São Classes que tem um ciclo de vida, possuem um template que define a
//aparência e um selector(TAG) para ser usado por outras partes na aplicação. São elementos personalizados.
//No AngularJS (Angular1) são Controllers e Scopes.

//Para ver a estrutura de um componente coloquei a imagem (3) na pasta, e também a imagem 4 que mostra a estrutura
//via código. Também coloquei o @NgModule na imagem 5 de como ele seria importado.

//Parar gerar um componente, na pasta do Aplicativo, usar o Terminal:

//ng generate componente nomeComponente (OU - forma abreviada):
//ng g c header --spec=false (spec = não vai gerar componentes de teste).

//Ele irá gerar 3 arquivos:
//header.components.ts que é a classe do componente.
//header.components.html que é o template do componente.
//header.component.css que é o estilo do componente.

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'cli-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}