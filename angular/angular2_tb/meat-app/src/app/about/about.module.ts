import { NgModule } from '@angular/core';
import { AboutComponent } from './about.component';
import {RouterModule, Routes} from '@angular/router'

//como não existe mais no arquivos de rota uma indicação pro componente padrão (home) que ele irá criar
//dentro do módulo eu preciso informar pra ele qual é a ROTA padrão que ele irá carregar
const ROUTES: Routes = [
    {path: '', component: AboutComponent}
]

@NgModule({
    declarations: [AboutComponent],
    imports: [ RouterModule, RouterModule.forChild(ROUTES)],
    exports: [],
    providers: [],
})
export class AboutModule {}