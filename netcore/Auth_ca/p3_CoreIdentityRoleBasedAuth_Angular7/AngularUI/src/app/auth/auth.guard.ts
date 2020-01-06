import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../shared/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router, private service: UserService) {
  }
  
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      //basically here i just need to verify if user is authenticated or not
      //if user is not authenticated, we need to block it to access private routes
      if (localStorage.getItem('token') != null){
        let roles = next.data['permittedRoles'] as Array<string>;
        if(roles){
          if(this.service.roleMatch(roles)) return true; //se houver a role, eu retorno true
          else{
            this.router.navigate(['/forbidden']); //here i'll check if the user can have access...
            return false;
          }
        }
        return true;
      }
      else {
        this.router.navigate(['/user/login']);
        return false;
      }  
    }
  
}
