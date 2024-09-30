import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Drink } from '../models/drink';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-drinks-list',
  templateUrl: './drinks-list.component.html',
  styleUrls: ['./drinks-list.component.scss']
},
)
export class DrinksListComponent implements OnInit {


  drinks: Drink[] = [
    /* {id:1,name:'Coca Cola',price: 1.5},
    {id:2,name:'Pepsi',price: 1.5},
    {id:3,name:'Fanta',price: 1.5},
    {id:4,name:'Sprite',price: 1.5},
    {id:5,name:'7up',price: 1.5}, */

  ];

  constructor(public cart:CartService,
    private client:HttpClient
  ) { }

  ngOnInit(): void {
    this.client.get<Drink[]>(environment.apiEndpoint+'/drinks/all',).subscribe(
      (data) => {
        this.drinks = data;
      }
    );
  }

}
