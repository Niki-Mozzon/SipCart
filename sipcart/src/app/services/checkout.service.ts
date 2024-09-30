import { Injectable } from '@angular/core';
import { Order } from '../models/order';
import { Product } from '../models/product';
import { HttpClient } from '@angular/common/http';
import { Cart } from '../models/cart';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  working:boolean = false;
  order? :Order;
  request? :any;
  constructor(private httpClient:HttpClient) { }

  setRequest(cart : Cart, couponCode:string|null){
    let products:Map<number,number>=new Map();
    cart.products.forEach(p => products.set(p.drink.id,p.quantity));
    this.request={Products:Object.fromEntries( products),couponCode:couponCode?couponCode:null};
  }
  
  getOrder(){
    this.working = true;
    return this.httpClient.post(environment.apiEndpoint+"/Order/checkout",this.request);
    
  }

  submitOrder(order: Order){
   return this.httpClient.post(environment.apiEndpoint+"/Order/purchase",order)
  }


}
