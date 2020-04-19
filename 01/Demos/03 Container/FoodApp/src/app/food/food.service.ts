import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { FoodItem } from './food.model';

@Injectable({
  providedIn: 'root'
})
export class FoodService {
  constructor() {}

  getFood(): Observable<FoodItem[]> {
    return of([
      { id: 1, name: 'Butter Chicken', price: 9, calories: 1200 },
      { id: 2, name: 'Curry Wurst', price: 2.7, calories: 730 },
      { id: 3, name: 'Blini with Salmon', price: 8.3, calories: 600 }
    ]);
  }
}
