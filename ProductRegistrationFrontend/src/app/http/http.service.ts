import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { IProducts } from '../interfaces/products';


@Injectable({
  providedIn: 'root'
})
export class HttpService {

  apiUrl = "http://localhost:5134";
  http = inject(HttpClient);
 
  constructor() { }

  getAllProduct(){
    return this.http.get<IProducts[]>(this.apiUrl + "/api/Products/");
  }  
  createProduct(product:IProducts){
    return this.http.post(this.apiUrl + "/api/Products/", product);
  }
  getProduct(productId:number){
    return this.http.get<IProducts>(this.apiUrl + "/api/Products/" + productId);
  }
  updateProduct(product:IProducts){
    return this.http.put<IProducts>(this.apiUrl + "/api/Products/", product);
  }
  deleteProduct(productId:number){
    return this.http.delete<IProducts>(this.apiUrl + "/api/Products/" + productId);
  }
}
