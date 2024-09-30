import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CheckoutService } from '../services/checkout.service';
import { Order } from '../models/order';
import { ePaymentMethod } from '../models/ePaymentMethod';
import { CartService } from '../services/cart.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  paymentMethodEnum = ePaymentMethod;
  paymentMethod: ePaymentMethod = ePaymentMethod.CARD;
  orderId: number = 0;

  order!: Order;
  constructor(public checkoutService: CheckoutService, private cartService: CartService, private router: Router,private toastr: ToastrService) { }

  ngOnInit(): void {
    if (this.checkoutService.request) {
      this.checkoutService.getOrder().subscribe((data) => {
        this.order = data as Order;
      })
    }
  }
  
  submitOrder() {
    this.order.paymentMethod = this.paymentMethod;
    if (this.order.paymentMethod == ePaymentMethod.CASH && this.order.totalPrice>10) {
      this.toastr.error('Cash payment is available only for orders less or equal than 10€');
      return;
    }
    try {
      this.checkoutService.setRequest(this.cartService.getCart(), null);
      this.checkoutService.submitOrder(this.order).subscribe(data => {
        console.log(data);
        this.orderId = data as number;
        this.toastr.success('Purchase with id: '+this.orderId +' submitted successfully');
      });
      this.cartService.clearCart();
      this.goToHome();
    } 
    catch (e) {
      
      this.toastr.error('Something wrong happened, please try later..');
      console.log(e);
    }
  }

  goToHome() {
    this.router.navigate(['/']);
  }
}
