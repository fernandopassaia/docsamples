import {Contract} from '../contract';
import { Flunt } from 'src/utils/flunt';
import { Injectable } from '@nestjs/common';
import { CreateCustomerDto } from '../../dtos/customer/create-customer.dto';


@Injectable() //now my class can be injected in others
export class CreateCustomerContract implements Contract {
    //now i will implement my interface "contract"
    errors: any[];
    
    //the model i will receive is not any, but Contract
    validate(model: CreateCustomerDto): boolean {
        //here i will use Flunt to do the validations
        const flunt = new Flunt();

        flunt.hasMinLen(model.name, 5, 'Nome Inválido');
        flunt.isEmail(model.email, 'Email inválido');
        flunt.isFixedLen(model.document, 11, 'CPF inválido');        
        flunt.isFixedLen(model.password, 6, 'PassWord invalido');        

        this.errors = flunt.errors; //get the "errors" on flunt after the validation
        return flunt.isValid(); // will return if has or not erros on list
    }
}