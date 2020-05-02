import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

//https://medium.com/@amcdnl/authentication-in-angular-jwt-c1067495c5e0
@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router){}

    canActivate() {
        if (!this.authService.isTokenExpired()) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}
