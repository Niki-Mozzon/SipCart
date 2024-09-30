import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { DrinksListComponent } from './drinks-list/drinks-list.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { CouponModalComponent } from './coupon-modal/coupon-modal.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    DrinksListComponent,
    ProductCardComponent,
    HeaderComponent,
    CartComponent,
    CheckoutComponent,
    CouponModalComponent
  ],
  imports: [
    RouterOutlet, 
    RouterLink, 
    RouterLinkActive,
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbToastModule,
    ToastrModule.forRoot({
      timeOut: 3000,  // Duration of the toast message
      positionClass: 'toast-top-right',  // Position of the toast
      preventDuplicates: true,  // Prevent duplicate toasts
      easing: 'ease-in',  // Use custom animation easing
      tapToDismiss: true,  // Dismiss the toast on tap
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
