import { Component, OnInit } from '@angular/core';
import { FoodItem } from '../food.model';
import { FoodState } from '../store/reducers/food.reducer';
import { Store } from '@ngrx/store';
import { LoadFoods, SelectFood } from '../store/actions/food.actions';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { getAllFood, getSelected } from '../store/selectors/food.selectors';

@Component({
  selector: 'app-food-container',
  templateUrl: './food-container.component.html',
  styleUrls: ['./food-container.component.scss']
})
export class FoodContainerComponent implements OnInit {
  constructor(private store: Store<FoodState>) {}

  food$: Observable<Array<FoodItem>> = this.store
    .select(getAllFood)
    .pipe(tap(data => console.log('data received from store', data)));

  selected$: Observable<FoodItem> = this.store.select(getSelected);

  ngOnInit() {
    this.store.dispatch(new LoadFoods());
  }

  selectFood(f: FoodItem) {
    this.store.dispatch(new SelectFood(f));
  }

  deleteFood(f: FoodItem) {
    console.log('deleteing ', f);
    // this.food = this.food.filter(item => item.id != f.id);
  }

  foodSaved(f: FoodItem) {
    // this.food = this.food.filter(item => item.id != f.id);
    // this.food.push(f);
    // this.selected = null;
  }
}
