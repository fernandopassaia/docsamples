import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { CustomValidator } from '../../validators/custom.validator';
import { DataService } from '../../services/data.service';
import { Ui } from '../../utils/ui';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  //Note: I can "inject" the Service here, OR in App.Module (if i do there, will be a Singleton available for ALL application)
  providers: [Ui, DataService]
})

export class LoginPageComponent implements OnInit {

  //aqui eu crio meu formulário em typescript
  public form: FormGroup;

  //injeto meu formbuilder no construtor via DI
  constructor(private fb: FormBuilder, private ui: Ui, private dataService: DataService) { 

    //agora vamos compor o nosso form (form é meu formulário, formbuilder irei usar pra construir o formulário)
    this.form = this.fb.group({//crio um novo grupo      
      email: ['', Validators.compose([//aqui eu coloco os campos... se tiver 50, 50!
        Validators.minLength(5),
        Validators.maxLength(160),
        Validators.required,
        CustomValidator.EmailValidator
      ])], 

      password: ['', Validators.compose([//inicia vazio '', e ai o array das minhas validações...
        Validators.minLength(6),
        Validators.maxLength(20),
        Validators.required
      ])] 
    });
  }

  ngOnInit() {
    //aqui só pra efeito de TESTES, irei chamar o meu serviço que carrega a
    //API do StarWars (gratuita, testes) e vou imprimir o resultado no console
    this.
    dataService
    .getStarWarsApi() //no subscribe eu sempre vou ter resultado OU erro, em ambos vou imprimir
    .subscribe(result => { 
      console.log(result);
    }, error => {
      console.log(error);
    });
  }

  showModal() {
    this.ui.setActive('modal');
  }

  hideModal() {
    this.ui.setInactive('modal');
  }

  submit(){
    //this.dataService.createUser(this.form.value); //just to test and print
  }
  
  // submit() {
  //   this.dataService
  //     .authenticate(this.form.value)
  //     .subscribe(result => {
  //       localStorage.setItem('mws.token', result.token);
  //       localStorage.setItem('mws.user', JSON.stringify(result.user));

  //       this.router.navigateByUrl('/home');
  //     }, error => {
  //       this.errors = JSON.parse(error._body).errors;
  //     });
  // }

  checkEmail(){
    this.ui.lock('emailControl');
    setTimeout(() => {
      this.ui.unlock('emailControl');
      console.log(this.form.controls['email'].value);
    },3000); //here i say that my "validationProcess" (while gif "loading" runs is 3seconds)
  }

  //ANTES do meu componente de travar 
  // checkEmail(){
  //   document.getElementById('emailControl').classList.add('is-loading') //when i press TAB, my textbox will run a "gif" loading
  //   this.form.controls['email'].disable; //i disable my control while it's beeing validate
  //   setTimeout(() => {
  //     this.form.controls['email'].enable; //after 3 seconds i open it again
  //     document.getElementById('emailControl').classList.remove('is-loading'); //remove the CSS Bulma class
  //     console.log(this.form.controls['email'].value);
  //   },3000); //here i say that my "validationProcess" (while gif "loading" runs is 3seconds)
  // }

}
