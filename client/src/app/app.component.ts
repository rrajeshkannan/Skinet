import { HttpClient, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';
import { IProductPagination } from './models/product-pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';
  products: IProduct[];

  constructor(private http: HttpClient){

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=50')
      .subscribe({
        next: (response: IProductPagination) => {
          console.log(response);
          this.products = response.entities;
        },
        error: (err) => console.error(err)
      });

  }
}