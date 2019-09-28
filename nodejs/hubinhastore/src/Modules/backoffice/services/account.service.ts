import { Model } from 'mongoose';
import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { User } from 'src/modules/backoffice/models/user.model';

@Injectable()
export class AccountService {
    //i will receive 'user' in constructor by injection and create the variable model for it
    //note: i've made this because i don't want to get it from the body on every method
    constructor(@InjectModel('User') private readonly model: Model<User>) { }
    //here is the thing: InjectModel cames from Mongoose, and it will create a kind
    //of "database object" that will be able to save the user right to DB - easy this way

    async create(data: User): Promise<User> {
        const user = new this.model(data); //i create a new model based on data received
        return await user.save(); //await say to method "wait" the user operation complets, when done
        //will return the user... i need the user ID to create a Customer... look at that!
    }
}
