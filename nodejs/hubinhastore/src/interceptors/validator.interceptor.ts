import { NestInterceptor, Injectable, ExecutionContext, HttpException, HttpStatus } from "@nestjs/common";
import { Observable } from "rxjs";
import { Result } from "src/Modules/backoffice/models/result.model";
import { Contract } from "src/Modules/backoffice/contracts/contract";

@Injectable()
export class ValidatorInterceptor implements NestInterceptor {
    //i need to pass my Contract, that i want to validate (Contract should implement Contract-Interface)
    constructor(public contract: Contract) {
        
    }

    intercept(context: ExecutionContext, call$: Observable<any>):Observable<any>
    //i will subscribe to hear the events. So all the times when there's a change,
    //it will be publish and everyone who is subscribed will receive it.
    {
        const body = context.switchToHttp().getRequest().body; //i get the body of requisition (json with fields)
        const valid = this.contract.validate(body); //to know if context is valid, i need my contract (generic contract - to all contracts - received over constructor DI)
        if(!valid){
            throw new HttpException(new Result('Ops, algo errado', false, null, this.contract.errors), HttpStatus.BAD_REQUEST);
        }
        
        return call$;
    }
}