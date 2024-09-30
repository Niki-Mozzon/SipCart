import { HttpClient } from '@angular/common/http';
import { Component, EnvironmentInjector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Coupon } from '../models/coupon';
import { CheckoutService } from '../services/checkout.service';
import { CartService } from '../services/cart.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-coupon-modal',
  templateUrl: './coupon-modal.component.html',
  styleUrls: ['./coupon-modal.component.scss']
})
export class CouponModalComponent implements OnInit {
  validCoupon: boolean | null = null;
  coupon: Coupon | null = null;
  couponCode: string = '';

  constructor(private modalService: NgbModal,
    private router: Router,
    private httpClient: HttpClient,
    private cartService:CartService,
    private toastr: ToastrService,
    private env:EnvironmentInjector,
  private orderService:CheckoutService) { }

  ngOnInit(): void {
  }

  dismiss() {
    this.modalService.dismissAll();
  }

  async applyCoupon() {
    fetch(environment.apiEndpoint+'/coupon/coupon?code=' + this.couponCode).then(async (data) => {
      if (data.ok) {
        this.toastr.success('Coupon applied successfully');
        this.coupon = await data.json();
        this.validCoupon = true;
      }
      else {
        this.toastr.error('Invalid coupon code');
        this.validCoupon = false;
        this.couponCode = '';
      }
    }).catch((error) => {
      this.toastr.error('Invalid coupon code');
      this.validCoupon = false;
      this.couponCode = '';
    }
    );
  }

   checkout() {
     this.orderService.setRequest(this.cartService.getCart(), this.couponCode && this.validCoupon?this.couponCode:null);
    this.router.navigate(['/checkout'], {});
      this.dismiss();
  }

}
