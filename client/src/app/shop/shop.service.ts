import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductPagination } from '../shared/models/product-pagination';
import { IProductType } from '../shared/models/product-type';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(productBrandId?: number, productTypeId?: number, sort?: string) {
    let params = new HttpParams();

    if (productBrandId) {
      params = params.append('brandId', productBrandId.toString());
    }

    if (productTypeId) {
      params = params.append('typeId', productTypeId.toString());
    }

    if (sort) {
      params = params.append('sort', sort);
      
    }

    return this.http.get<IProductPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getProductBrands(){
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getProductTypes(){
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

}