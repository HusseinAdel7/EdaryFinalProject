import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListMainAccounts } from './list-main-accounts/list-main-accounts';

const routes: Routes = [
  {
      path: '',
      pathMatch: 'full',
      component: ListMainAccounts,
  
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainAccountsRoutingModule { }
