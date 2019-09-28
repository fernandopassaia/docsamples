import { Injectable } from '@angular/core';
import { AngularFireDatabase, AngularFireList } from 'angularfire2/database';

import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  departmentList: AngularFireList<any>;
  array = []; //i put this Array because i'll convert the AngularFireList to an Array

  //Note: I've created a pre-list in FireBase (image 15 and 16) and here I'll just LOAD them...
  //I'll not create all operations (insert, update): Just load the list into the Combobox...

  constructor(private firebase: AngularFireDatabase) {
    this.departmentList = this.firebase.list('departments');
    //snapshotChanges will convert AngularFireList into a Observable... then i subscribe it to changes
    this.departmentList.snapshotChanges().subscribe(
      list => {
        this.array = list.map(item => {
          return {
            $key: item.key,
            ...item.payload.val()
          };
        });
      });
   }


   getDepartmentName($key) {
     return ""; //fix HERE!
    //  if ($key == "0")
    //    return "";
    //  else{
    //    console.log('entrei aqui: ', this.array);
    //    return _.find(this.array, (obj) => { return obj.$key == $key; })['name'];
    //  }
  }

}
