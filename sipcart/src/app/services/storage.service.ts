import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private storageKey = 'cart'; 
  

  constructor() { }

  // Set data in localStorage
  setData(data: any): void {
    localStorage.setItem(this.storageKey, JSON.stringify(data));
  }

  // Get data from localStorage
  getData(): any {
    const data = localStorage.getItem(this.storageKey);
    return data ? JSON.parse(data) : []; // Return null if no data found
  }

  /* // Remove data from localStorage
  removeData(): void {
    this.setData({products:[]});
  } */
}
