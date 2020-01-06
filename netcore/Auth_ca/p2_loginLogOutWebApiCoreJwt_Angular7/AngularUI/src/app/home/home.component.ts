import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {

  userDetails; //to store the user details received by the API on getUserProfile (ngOnInit)
  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      }
    )
  }

  onLogout() {
    localStorage.removeItem('token'); //on logout I'll remove the created token...
    this.router.navigate(['/user/login']);
  }
}
