import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: []
})
export class HomeComponent {

  constructor(private router: Router, private jwtHelper: JwtHelperService) {
  }

  isUserAuthenticated() {
    const token: string = localStorage.getItem('jwt');
    console.log(token);
    console.log(!this.jwtHelper.isTokenExpired(token));
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

  logOut(){
    localStorage.removeItem('jwt');
  }
}
