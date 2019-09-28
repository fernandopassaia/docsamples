import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:51984/api';

  //I've created a group for the controls of the form
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    //and here another group for the Password
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/ApplicationUser/Register', body);
  }

  login(formData){
    return this.http.post(this.BaseURI + '/ApplicationUser/Login', formData); //return a observable (will need subscribe)
  }

  getUserProfile(){
    //note: this method was changed (last version is below) because now I'm using HttpInterceptor
    //to send the TOKEN in all request. Read Auth > Interceptor for information.        
    return this.http.get(this.BaseURI + '/UserProfile'); //return a observable
  }
  
  //método anterior, que pegava o TOKEN manualmente do localstorage (antes do interceptor)
  // getUserProfile(){
  //   //note: to USE this method and call this WebApi, i need to send the JWT
  //   var tokenHeader = new HttpHeaders({ 'Authorization':'Bearer ' + localStorage.getItem('token') });
  //   return this.http.get(this.BaseURI + '/UserProfile', {headers:tokenHeader}); //return a observable
  // }
}