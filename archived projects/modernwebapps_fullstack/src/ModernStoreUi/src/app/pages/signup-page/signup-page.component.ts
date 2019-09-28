import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidator } from '../../validators/custom.validator';
import { DataService } from '../../services/data.service';
import { Ui } from '../../utils/ui';

@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  providers: [Ui, DataService]
})

export class SignupPageComponent implements OnInit {
//aqui eu crio meu formulário em typescript
public form: FormGroup;
public errors: any[] = []; //will put the erros to show on DIV error on

//injeto meu formbuilder no construtor via DI
constructor(private fb: FormBuilder, private ui: Ui, private dataService: DataService) { 

  //agora vamos compor o nosso form (form é meu formulário, formbuilder irei usar pra construir o formulário)
  this.form = this.fb.group({//crio um novo grupo      
    firstName: ['Fernando', Validators.compose([//aqui eu coloco os campos... se tiver 50, 50!
      Validators.minLength(3),
      Validators.maxLength(60),
      Validators.required
    ])], 

    lastName: ['Passaia', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
      Validators.minLength(3),
      Validators.maxLength(60),
      Validators.required
    ])],

    email: ['fernandopassaia@futuradata.com.br', Validators.compose([//aqui eu coloco os campos... se tiver 50, 50!
      Validators.minLength(20),
      Validators.maxLength(60),
      Validators.required,
      CustomValidator.EmailValidator
    ])], 

      document: ['32533832880', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
      Validators.minLength(5),
      Validators.maxLength(20),
      Validators.required
    ])],

    username: ['fernandopassaia', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
      Validators.minLength(5),
      Validators.maxLength(20),
      Validators.required
    ])],

    password: ['1234fd', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
      Validators.minLength(6),
      Validators.maxLength(20),
      Validators.required
    ])],

    confirmPassword: ['1234fd', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
    Validators.minLength(6),
    Validators.maxLength(20),
    Validators.required
    ])]
  });
}

  ngOnInit() {
  }

  submit(){
    this.dataService.createUser(this.form.value).subscribe(result => {
      console.log(result);
    }, error => {      
      //this.errors = JSON.parse(error._body).errors; //this is BALTA code, but don't work anymore
      this.errors = error.error.errors; // Object.keys(error.error);
      console.log(this.errors);
      //console.log(error._body);
    });
  }

}
