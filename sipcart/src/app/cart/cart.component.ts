import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';
import { CartService } from '../services/cart.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CouponModalComponent } from '../coupon-modal/coupon-modal.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  cartDrinks!: Product[];
  constructor(public cart: CartService,
    private modalService: NgbModal) { }


  getTotal(): number {
    return this.cart.getEstimatedTotal()
  }


  openModal() {
    // Trigger the Bootstrap modal
    this.modalService.open(CouponModalComponent, { ariaLabelledBy: 'modal-basic-title' });
  }

  ngOnInit(): void {
    this.cartDrinks = this.cart.getCart().products;
  }

  getProducts(): Product[] {
    return this.cart.getCart().products;
  }

}
