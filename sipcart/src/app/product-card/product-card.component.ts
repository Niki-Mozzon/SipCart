import { Component, Input, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Product } from '../models/product';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
incrementQuantity() {
this.product.quantity++;
this.cart.updateQuantity(this.product);
}
decrementQuantity() {
this.product.quantity--;
this.cart.updateQuantity(this.product);
}

  @Input() product!:Product;
  constructor(public cart:CartService) { }

  ngOnInit(): void {
    this.product= this.cart.getProduct(this.product.drink)?.quantity?this.cart.getProduct(this.product.drink)!:this.product;
  }

}
