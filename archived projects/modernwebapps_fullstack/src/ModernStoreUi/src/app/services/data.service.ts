import { Injectable } from '@angular/core';
//Note: On Angular 2 (Balta Course) it uses "Http from @angular/http". I had to UPDATE to Angular7, and it's imported on app.module.ts too!
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http'; 
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';

@Injectable()
export class DataService{
    private serviceUrl: string = 'https://localhost:44333/';

    constructor(private http:HttpClient) {}   

    //this is just a sample/test using the Public StarWars API. Here i will show
    //how to get Data from API using Angular. https://swapi.co/api/people
    getStarWarsApi(){
        return this.http
        .get('https://swapi.co/api/people'); //JSON is an assumed default and no longer needs to be explicitly parsed
        //.map((res: Response) => res.json());
    }

    createUser(data: any){
        return this.http
        .post(this.serviceUrl + 'v1/customers', data);
        //.map((res: Response) => )
    }
}