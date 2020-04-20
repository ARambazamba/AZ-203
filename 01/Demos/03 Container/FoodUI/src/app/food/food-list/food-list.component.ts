import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  SimpleChanges
} from '@angular/core';
import { FoodItem } from 'src/app/food/food.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-food-list',
  templateUrl: './food-list.component.html',
  styleUrls: ['./food-list.component.scss']
})
export class FoodListComponent implements OnInit {
  constructor() {}

  @Input()
  food: FoodItem[];
  @Output()
  editSelected: EventEmitter<FoodItem> = new EventEmitter();
  @Output()
  deleteSelected: EventEmitter<FoodItem> = new EventEmitter();

  ngOnInit() {}

  ngOnChanges(changes: SimpleChanges) {
    console.log(changes.food.currentValue);
    this.dataSource = new MatTableDataSource(changes.food.currentValue);
  }

  displayedColumns: string[] = [
    'id',
    'name',
    'price',
    'calories',
    'deleteItem',
    'editItem'
  ];
  dataSource: MatTableDataSource<FoodItem> = new MatTableDataSource([]);

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addFood() {
    console.log(this.getNextId());
    this.editSelected.emit({
      id: this.getNextId(),
      name: '',
      price: 0,
      calories: 0
    });
  }

  getNextId(): number {
    return this.food.reduce((acc, f) => (acc = acc > f.id ? acc : f.id), 0) + 1;
  }

  selectFood(p: FoodItem) {
    this.editSelected.emit(p);
  }

  deleteFood(p: FoodItem) {
    this.deleteSelected.emit(p);
  }
}
