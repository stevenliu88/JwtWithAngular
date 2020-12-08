import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: []
})
export class CustomersComponent implements OnInit {
    customers: any;

    constructor(private http: HttpClient) {}
    ngOnInit() {
      this.http.get("https://localhost:5001/api/customers")
      .subscribe(response => {
        this.customers = response;
      },
        error => {
          console.log(error);
        }
      );
    }
  }
