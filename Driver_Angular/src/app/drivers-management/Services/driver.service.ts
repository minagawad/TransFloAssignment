import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { IDriver } from '../Models/idriver';
import { Observable } from 'rxjs';
import { IPaginationModel } from '../Models/i-pagination-model';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  baseUrl: string = "";
  constructor(
    private http: HttpClient) {

   this.baseUrl = environment.basurl
  }

  getDrivers(pagination: IPaginationModel): Observable<any> {
    const url = `${this.baseUrl}/drivers?PageNumber=${pagination.pageNmuber}&PageSize=${pagination.pageSize}`;
    return this.http.get<any>(url);
  }
}
