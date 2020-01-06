import { Controller, Get, Post, Put, Delete, Param, Body, UseInterceptors, HttpException, HttpStatus } from "@nestjs/common";
import { Result } from "../models/result.model";
import { ValidatorInterceptor } from "src/interceptors/validator.interceptor";
import { AccountService } from 'src/modules/backoffice/services/account.service';
import { CustomerService } from 'src/modules/backoffice/services/customer.service';
import { User } from "../models/user.model";
import { CreateCustomerDto } from "../dtos/customer/create-customer.dto";
import { Customer } from "../models/customer.model";
import { Address } from "../models/address.model";
import { CreateCustomerContract } from "../contracts/customer/create-customer.contract";
import { CreateAddressContract } from "../contracts/address/create-address.contract";
import { AddressService } from "../services/address.service";
import { AddressType } from "../enums/address-type.enum";

// localhost:3000/customers

@Controller('v1/customers') //in Rest, we should use Plural. Get/Post/Put/Delete is a Rest Pattern.
export class CustomerController{
    //Making the DI    
    constructor(private readonly customerService: CustomerService, private readonly accountService: AccountService, private readonly addressService: AddressService) {
        //note that i need to declare AccountService on Providers on BackOfficeModule
        //Nest will solve byitself the Dependencies, don't need to create a container
        //like i did in .Net with Unity...
    }
    
    @Get()
    get(){
        return new Result(null, true, [], null);
    }

    @Get()
    getById(@Param('document') document: string){
        return new Result(null, true, {}, null);
    }
    
    @Post()
    @UseInterceptors(new ValidatorInterceptor(new CreateCustomerContract)) //i call my interceptor witch will validate everything based on my Contract
    async post( @Body() model: CreateCustomerDto) //i`m making a parse to my model
    {
        let user;
        let customer; //created outside the try because maybe i'll use it on catch
        try{
            //Mongo don't have rollBack (or Mongoose still not) so i will make a try
            //and in case user don't create, i will not create the customer
            user = await this.accountService.create(new User(model.document, model.password, true));
            customer = new Customer(model.name, model.document, model.email, [], null, null, null, user);
            await this.customerService.create(customer);
            return new Result('Cliente criado com sucesso!', true, user, null);
        }
        catch(error){
            //user.Remove - should implement - Rollback manual            
            throw new HttpException(new Result('Não foi possível realizar seu cadastro', false, null, error), HttpStatus.BAD_REQUEST);
        }        
    }

    @Put(':document')
    put(@Param('document') document, @Body() body){
        return new Result('Cliente alterado com sucesso!', true, body, null);
    }

    @Delete(':document')
    delete(@Param('document') document){
        return new Result('Cliente removido com sucesso!', true, null, null);
    }
}