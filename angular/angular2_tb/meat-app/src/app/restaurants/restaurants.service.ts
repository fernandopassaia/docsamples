import {Injectable} from '@angular/core' //como essa classe vai receber uma injeção 
import {Http} from '@angular/http' //como essa classe vai receber uma injeção 
import {Observable} from 'rxjs/Observable' //pra tratar o retorno do get (imagem 37 na pasta doc)
import 'rxjs/add/operator/map'
import 'rxjs/add/operator/catch'
import { Restaurant } from "./restaurant/restaurant.model";
import { MenuItem } from 'app/restaurant-detail/menu-item/menu-item.model';
import {MEAT_API} from '../app.api' //contém o endereço da minha Rest API
import {ErrorHandler} from '../app.error-handler' //contém o endereço da minha Rest API


//#region Doc
//aqui aula 40 - começa a ficar mais legal. Irei criar uma classe de Serviço de Restaurante, essa classe será responsável
//por acessar minha API e trazer os serviços de volta. Essa classe será injetada nas outras para fazer a coisa funcionar.
//na aula 41 faremos uma simulação de dados, e nas próximas aulas iremos pro HTTP pegando dados reais.

//Esse serviço será usado no restaurants.component.ts (injetado) e para isso será declarado no app.module.ts

//nota: você não precisa colocar o decorator @Injectable se sua classe não for receber um serviço HTTP por injeção.
//mas como iremos receber outro serviço - preciso marcar com @Injectable (e pra isso importei o decorator lá em cima)
//#endregion

@Injectable()
export class RestaurantsService{

    //e aqui finalmente faço a injeção do módulo de http
    constructor(private http: Http){ }

    //#region Doc
    //vou criar um array simulando restaurantes, já que AINDA não farei o backend (temporário)
    //NOTA: comentado por que AGORA virá de um serviço REST e usará HTTP
    // rests: Restaurant[] = [{
    //     id: "bread-bakery",
    //     name: "Bread & Bakery",
    //     category: "Bakery",
    //     deliveryEstimate: "25m",
    //     rating: 4.9,
    //     imagePath: "assets/img/restaurants/breadbakery.png"        
    // },
    // {
    //     id: "burger-house",
    //     name: "Burger House",
    //     category: "Hamburgers",
    //     deliveryEstimate: "100m",
    //     rating: 3.5,
    //     imagePath: "assets/img/restaurants/burgerhouse.png"
    // }] 

    //método que obtem a listagem de restaurantes (comentado, retornava o array acima - antigo)
    //restaurants(): Restaurant[]{
    //return this.rests; NOTA: comentado por que AGORA virá de um serviço REST e usará HTTP
    
    
    //AGORA eu uso a classe http (que tem get, post, put, delete, etc) pra pegar os dados da API
    //Nota: O jeito "Padrão" de devolver seria esse - pela lógica: return this.http.get(`${MEAT_API}/restaurants`) 
    //mas como eu disse na imagem 37 (pasta doc), o http.get retorna um Observable, então precisarei adaptar um pouco.
    
    //preciso pra converter o response (resposta crua - com corpo, status, se deu erro): e eu não preciso de tudo isso,
    //só preciso do objeto json, e pra isso usarei o MAP pra transformar o response em um Array de Restaurantes.
    //além disso preciso fazer um map do response para json de restaurante (ler na importação do map acima)        
    //#endregion

    //obtem todos os restaurantes
    restaurants(): Observable<Restaurant[]> {
        return this.http.get(`${MEAT_API}/restaurants`)
            .map(response => response.json())
            .catch(ErrorHandler.handleError) //chamo meu componente que trata os erros e envio o erro pra ele
            //agora possíveis simulações (posso jogar acima pra testar)
            //return this.http.get(`${MEAT_API}/restaurants1`) //URL restaurante1 não existe
    }

    //obtem restaurante por id - será chamado no componente de detalhe do restaurante
    restaurantById(id: string): Observable<Restaurant>{
        return this.http.get(`${MEAT_API}/restaurants/${id}`)
            .map(response => response.json()) //como teremos um observable de response, irei mapear para objeto json
            .catch(ErrorHandler.handleError)     
    }

    //obtem os reviews de um restaurante por ID
    reviewsOfRestaurant(id: string): Observable<any>{
        return this.http.get(`${MEAT_API}/restaurants/${id}/reviews`)
            .map(response => response.json()) //como teremos um observable de response, irei mapear para objeto json
            .catch(ErrorHandler.handleError)     
    }

    //recebo um MenuItem[] vindo lá do component de MenuItem, quando alguém clica em Adicionar ele irá disparar um evento para o SERVICE
    //(que é o pai, PARENT) avisando que foi clicado e passando o objeto (menuItem) para que eu faça a adição. Show demais!    
    menuOfRestaurant(id: string): Observable<MenuItem[]>{
        return this.http.get(`${MEAT_API}/restaurants/${id}/menu`)
          .map(response => response.json())
          .catch(ErrorHandler.handleError)
    }
}