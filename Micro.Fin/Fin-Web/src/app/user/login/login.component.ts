import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators} from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { AuthService } from '@app/shared/api-services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginFormGroup: FormGroup;

  constructor(private formBuilder: FormBuilder, private router : Router, private userService : UserService, private authService: AuthService) { 
    this.createLoginForm();
  }

  ngOnInit() {
    
  }

  createLoginForm() {
    this.loginFormGroup = this.formBuilder.group({
      'userName': ['', Validators.compose([Validators.required])],
      'password': ['', Validators.compose([Validators.required])]
    });
  }

  onSubmit() {
    if (this.loginFormGroup.valid && this.loginFormGroup.dirty) {
      console.log(this.loginFormGroup.value)
      this.userService.authenticateUser(this.loginFormGroup.value)
      .subscribe(login => {
        console.log(login['token']);
        this.authService.setToken(login['token']);
       
        this.router.navigate(['user']);
      });
    }
  }
}
