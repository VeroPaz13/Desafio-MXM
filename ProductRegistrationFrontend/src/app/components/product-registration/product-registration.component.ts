import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import { IProducts } from '../../interfaces/products';
import { CommonModule } from '@angular/common';
import { HttpService } from '../../http/http.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-registration',
  standalone: true,
  imports: [MatInputModule, MatButtonModule, FormsModule, ReactiveFormsModule, RouterLink, CommonModule],
  templateUrl: './product-registration.component.html',
  styleUrl: './product-registration.component.css'
})
export class ProductRegistrationComponent {
  formBuilder = inject(FormBuilder);
  httpService = inject(HttpService);
  router = inject(Router);
  route = inject(ActivatedRoute);

  productForm = this.formBuilder.group({
    id: [0, [Validators.required]],
    name:['', [Validators.required]],
    price:[0, [Validators.required]],
    quantity:[0, [Validators.required]]
  });

  productId!:number;
  isEdit = false;

  ngOnInit(){
    this.productId = this.route.snapshot.params['id'];
    if(this.productId){
      this.isEdit = true;
      this.httpService.getProduct(this.productId).subscribe(result =>{
        console.log(result);
        this.productForm.patchValue(result);
        
      });
    }
  }

  registration(){
    console.log(this.productForm.value);
    const product :IProducts = {
      id: this.productForm.value.id!,
      name: this.productForm.value.name!,
      price: this.productForm.value.price!,
      quantity: this.productForm.value.quantity!
    }
    if(this.isEdit){
      this.httpService.updateProduct(product).subscribe(() => {
        console.log("sucess");
        this.router.navigateByUrl("/productList");
      });
    }else{
      this.httpService.createProduct(product).subscribe(() => {
        console.log("sucess");
        this.router.navigateByUrl("/productList");
      });

    }

  }
  
}
