import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrinksListComponent } from './drinks-list/drinks-list.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';

const routes: Routes = [ { path: '', redirectTo: '/drinks', pathMatch: 'full' }, // Default route
  { path: 'drinks', component: DrinksListComponent }, // Route for the list of drinks
  { path: 'cart', component: CartComponent }, // Route for the cart
  { path: 'checkout', component: CheckoutComponent }, // Route for the cart
  { path: '**', redirectTo: '/drinks' } // Wildcard route for handling invalid paths
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
