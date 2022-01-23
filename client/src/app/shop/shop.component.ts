import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  productBrands: IProductBrand[];
  productTypes: IProductType[];

  productBrandIdSelected = 0;
  productTypeIdSelected = 0;
  sortSelected = 'name';
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.productBrandIdSelected, this.productTypeIdSelected, this.sortSelected).subscribe({
      next: response => { this.products = response.entities; },
      error: err => console.error(err)
    });
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe({
      next: response => { this.productBrands = [{id: 0, name: 'All'}, ...response]; },
      error: err => console.error(err)
    });
  }

  getProductTypes() {
    this.shopService.getProductTypes().subscribe({
      next: response => { this.productTypes = [{id: 0, name: 'All'}, ...response]; },
      error: err => console.error(err)
    });
  }

  onProductBrandSelected(productBrandId: number){
    this.productBrandIdSelected = productBrandId;
    this.getProducts();
  }

  onProductTypeSelected(productTypeId: number){
    this.productTypeIdSelected = productTypeId;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.sortSelected = sort;
    this.getProducts();
  }
}
