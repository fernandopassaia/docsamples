import { Model } from 'mongoose';
import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Customer } from 'src/modules/backoffice/models/customer.model';
import { Address } from 'src/modules/backoffice/models/address.model';
import { AddressType } from 'src/modules/backoffice/enums/address-type.enum';

@Injectable()
export class AddressService {
    constructor(@InjectModel('Customer') private readonly model: Model<Customer>) { }

    //to add an Address to the Client i the DOCUMENT, and the Adress (model)
    async create(document: string, data: Address, type: AddressType): Promise<Customer> {
        const options = { upsert: true }; //will look if there's a Address on client - if don't, it will create a NEW one. It's there's: will UPDATE.
        if (type == AddressType.Billing) {
            //instead of find item FIRST, and then Update, i do in the same call using "findOneAndUpdate"
            return await this.model.findOneAndUpdate({ document }, {
                $set: {
                    billingAddress: data, //i say that i will only update BILLING ADDRESS
                },
            }, options);
        } else {
            return await this.model.findOneAndUpdate({ document }, {
                $set: {
                    shippingAddress: data, //i say that i will only update SHIPPING ADDRESS ADDRESS
                },
            }, options);//will look if there's a Address on client - if don't, it will create a NEW one. It's there's: will UPDATE.
        }
    }
}
