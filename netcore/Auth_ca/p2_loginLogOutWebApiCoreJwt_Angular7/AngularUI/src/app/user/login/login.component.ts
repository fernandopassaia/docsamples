//This form is using the Approach of Template Driven. Because it's just a Two TextBox Forms - for sure
//will be better to use Reactive Forms. Anyway, I'll follow te Article. To use it, you'll need to imports
//"FormsModule" from 'angular/forms' in app.module.ts. (Take a look i call it #form and access with "form")

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formModel={
    UserName: '',
    Password: ''
  }

  constructor(private service:UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    //if the USER is already Autenticated, I'll take to the HOME Controller
    if(localStorage.getItem('token') != null){
      this.router.navigateByUrl('/home');
    }
  }
  
  onSubmit(form: NgForm){
    //form.value will send a JSON values from the textboxs on form
    //login will return a observable, so I'll subscribe to IT.
    //inside subscribe i can have to function: one for success, one for error
    console.log('entrou login');
    this.service.login(form.value).subscribe(
      (res: any) => {        
        localStorage.setItem('token', res.token); //I'll put the JWT into Browser LocalStorage (see pics on doc for info)
        this.router.navigateByUrl('/home');
      },
      err => {        
        if (err.status == 400)
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        else
          console.log(err); //if there`s another crazy error I'll print on console
      }
    );
  }

}
