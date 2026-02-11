import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListSupplies } from './list-supplies/list-supplies';

const routes: Routes = [
   {
        path: '',
        pathMatch: 'full',
        component: ListSupplies,
    
      }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SuppliersRoutingModule { }
