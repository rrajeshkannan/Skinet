import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';
import { ShopParams } from '../shared/models/shop-params';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IProductBrand[];
  types: IProductType[];

  shopParams = new ShopParams();
  totalCount: number;

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => 
      {
        this.products = response.entities;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: err => console.error(err)
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => { this.brands = [{id: 0, name: 'All'}, ...response]; },
      error: err => console.error(err)
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => { this.types = [{id: 0, name: 'All'}, ...response]; },
      error: err => console.error(err)
    });
  }

  onBrandSelected(productBrandId: number){
    this.shopParams.brandId = productBrandId;
    this.getProducts();
  }

  onTypeSelected(productTypeId: number){
    this.shopParams.typeId = productTypeId;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any){
    this.shopParams.pageNumber = event.page;
    this.getProducts();
  }
}
