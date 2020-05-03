import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './user/login/login.component';
import { UserComponent } from './user/user.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from './user/user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseApiService } from './shared/api-services/base-api-service';
import { AuthInterceptor } from './shared/api-services/auth.interceptor';
import { AuthGuard } from './shared/api-services/auth.guard';
import { AuthService } from './shared/api-services/auth.service';
import { HomeComponent } from './home/home.component';
import { NgxNavbarModule } from 'ngx-bootstrap-navbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SplitterContainerComponent } from './shared/splitter/splitter-container/splitter-container.component';
import { NavigationBarComponent } from './shared/navigation-bar/navigation-bar.component';
import { LoanListComponent } from './home/loan-list/loan-list.component';
import { LoanDetailComponent } from './home/loan-detail/loan-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    HomeComponent,
    SplitterContainerComponent,
    NavigationBarComponent,
    LoanListComponent,
    LoanDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxNavbarModule
  ],
  entryComponents: [
    LoanListComponent,
    LoanDetailComponent
  ],
  providers: [BaseApiService, UserService, AuthGuard, AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
