import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';

// Services SingleTon: CardService will be imported in the "MAIN" application Module, because
// we will always have just ONE card in all Pages. Card will never loose it's values, it needs
// to keep it data. So i will never make other instances. For the other, i'll do at the local .TS
// file (like login-page.component.ts).

@Injectable()
export class CartService{
    public items: any[] = []; //my array of item
    cartChange: Observable<any>; //the change by itself (when something change, i do "next" and it "tells" to ever Observer - "hey, i'm updated")
    cartChangeObserver: Observer<any>; //here people will subscribe to hear when it changes

    constructor(){
        this.cartChange = new Observable((observer: Observer<any>) => {
            this.cartChangeObserver = observer; //all the time cart changes, i will pass to it
        });
    }

    addItem(item){
        this.items.push(item);
        this.cartChangeObserver.next(this.items);
    }
}