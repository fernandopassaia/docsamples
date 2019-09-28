import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router) {
  }
  
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      //basically here i just need to verify if user is authenticated or not
      //if user is not authenticated, we need to block it to access private routes
      if(localStorage.getItem('token') != null){
        return true;
      }else{
        this.router.navigate(['user/login']);
        return false;
      }
  }
  
}
