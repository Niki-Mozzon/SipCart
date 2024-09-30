import { Injectable } from '@angular/core';
import { Cart } from '../models/cart';
import { Drink } from '../models/drink';
import { Product } from '../models/product';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private storage: StorageService) { }

  getEstimatedTotal(): number {
    let estimatedTotal = this.cart.products.reduce((acc, p) => acc + p.drink.price * p.quantity, 0);
    return Math.round(estimatedTotal * 100) / 100;
  }

  cart: Cart = { products: [] };


  updateQuantity(product: Product) {
    if (product.quantity == 0) {
      //remove from cart
      this.cart.products = this.cart.products?.filter(p => p.drink.id != product.drink.id);
    }
    else {
      //update quantity
      let item = this.cart.products?.find(p => p.drink.id == product.drink.id);
      if (item) {
        item.quantity = product.quantity;
      }
      else {
        this.cart.products?.push(product);
      }
    }
    this.storage.setData(this.cart);
  }






  getCart() {
    if (this.cart?.products?.length == 0) {  //if no products in cart
      let data:any =this.storage.getData();
      if (data.products) { //check if there are products in local storage
        this.cart = data; //get products from local storage
        return this.cart;
      }
      this.cart.products = [];
    }
    return this.cart;
  }


  clearCart() {
    this.cart.products = [];
    this.storage.setData(this.cart);
  }

  getProduct(product: Drink): { drink: Drink, quantity: number } | undefined {
    return this.getCart().products?.find(p => p.drink.id == product.id);
  }
}
