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
import { HomeService } from './home/home.service';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { LoanListItemComponent } from './home/loan-list/loan-list-item/loan-list-item.component';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { AddRowDirective } from './shared/directives/add-row.directive';
import { MessagesModule}  from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { MessageService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule, BsDropdownModule, TypeaheadModule } from 'ngx-bootstrap';
import { CustomerComponent } from './customer/customer.component';
import { CustomerService } from './customer/customer.service';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserComponent,
    HomeComponent,
    SplitterContainerComponent,
    NavigationBarComponent,
    LoanListComponent,
    LoanDetailComponent,
    LoanListItemComponent,
    AddRowDirective,
    CustomerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxNavbarModule,
    VirtualScrollerModule,
    TableModule,
    DropdownModule,
    CalendarModule,
    InputMaskModule,
    ButtonModule,
    MessagesModule,
    MessageModule,
    FormsModule,
    NgSelectModule,
    BsDatepickerModule.forRoot(), 
    BsDropdownModule.forRoot(),
    TypeaheadModule.forRoot(),
    DialogModule,
    CheckboxModule
  ],
  entryComponents: [
    LoanListComponent,
    LoanDetailComponent
  ],
  providers: [BaseApiService, UserService, AuthGuard, AuthService, HomeService, MessageService, CustomerService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
