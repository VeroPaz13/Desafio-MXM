import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductRegistrationComponent } from './components/product-registration/product-registration.component';

export const routes: Routes = [
    {
       path:"",
       component:ProductListComponent
    },
    {
       path:"productList",
       component:ProductListComponent
    },
    {
       path:"create-product",
       component:ProductRegistrationComponent
    
    },
    {
       path:"products/:id",
       component:ProductRegistrationComponent
    }
];
