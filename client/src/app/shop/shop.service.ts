import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductPagination } from '../shared/models/product-pagination';
import { IProductType } from '../shared/models/product-type';
import { ShopParams } from '../shared/models/shop-params';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId != 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId != 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    
    params = params.append('sort', shopParams.sort);

    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());


    return this.http.get<IProductPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getBrands(){
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes(){
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

}