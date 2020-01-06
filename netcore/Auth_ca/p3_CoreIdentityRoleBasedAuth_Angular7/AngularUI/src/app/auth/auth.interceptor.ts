//Nota: A Função desse arquivo é evitar que a CADA requisição eu tenha que ficar pegando
//o TOKEN manualmente, e enviando pra API. Veja método getUserProfile comentado no UserService.
//A função desse método é simplesmente "anexar" o TOKEN automáticamente a QUALQUER requisição.
//as requisições que não precisam de autenticação - irão ignorar. O restante, usará.

import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { Router } from "@angular/router";

@Injectable() //poderá ser injetada em outros lugares
//irá implementar a interface HttpInterceptor do Angular
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('token') != null) { //check if user is logged, if is, return token into headers
            const clonedReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
            });
            return next.handle(clonedReq).pipe(
                tap(
                    succ => { }, //on success i don't need to do anything
                    err => {
                        //401 means that the TOKEN is expired, so I'll REMOVE Token from 
                        //LocalStorage and send user to Login and get new token
                        if (err.status == 401){
                            localStorage.removeItem('token');
                            this.router.navigateByUrl('/user/login');
                        }
                        else if(err.status == 403)
                        this.router.navigateByUrl('/forbidden'); //se vier um 403, eu mando pro forbidden
                    }
                )
            )
        }
        else
            return next.handle(req.clone());
    }
}