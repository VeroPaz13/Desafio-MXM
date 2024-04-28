import { Component, inject } from '@angular/core';
import { IProducts } from '../../interfaces/products';
import { HttpService } from '../../http/http.service';

import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule, RouterLink, CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  productList:IProducts[]=[];
  apiUrl = "http://localhost:5134";

  router = inject(Router);
  httpService = inject(HttpService);
  displayedColumns: string[] = [
    'id',
    'name',
    'price',
    'quantity',
    'edit'
  ];
 
  ngOnInit(){
    this.httpService.getAllProduct().subscribe((result) => {
      this.productList = result;
      console.log(this.productList);
    });
  }
  edit(id:number){
    console.log(id);
    this.router.navigateByUrl("/products/" + id);
  }  
  delete(id:number){
    this.httpService.deleteProduct(id).subscribe(() => {
      console.log("Produto excluido!")
      this.router.navigateByUrl("/productList");
    });
  }

}

