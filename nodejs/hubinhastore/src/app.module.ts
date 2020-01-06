import { Module } from '@nestjs/common';
import { BackofficeModule } from './Modules/backoffice/backoffice.module';
import { CustomerController } from './Modules//backoffice/controllers/customer.controller';
import { MongooseModule } from '@nestjs/mongoose';
import { AccountService } from './Modules/backoffice/services/account.service';
import { CustomerService } from './Modules/backoffice/services/customer.service';
import { AddressService } from './Modules/backoffice/services/address.service';

@Module({
  imports: [MongooseModule.forRoot('mongodb://hubinhapetstore:1234fd@ds038547.mlab.com:38547/hubinhapetstore'), //import of Mongoose
    BackofficeModule],
  controllers: [CustomerController],
  providers: [AccountService, AddressService, CustomerService],
})
export class AppModule {}