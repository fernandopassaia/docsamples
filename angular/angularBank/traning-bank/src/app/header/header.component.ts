import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  formLogin;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    console.log('Component app-header inciado...');
    this.formLogin = this.fb.group({
      cpf: ['']
    });
  }

}
