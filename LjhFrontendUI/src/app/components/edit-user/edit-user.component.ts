import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { ToastersService } from 'src/app/services/toasters.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatCheckboxModule
  ],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.scss',
})
export class EditUserComponent {
  editForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastersService: ToastersService,
    private router: Router
  ) {}

  ngOnInit() {
    this.editForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: [{ value: '', disabled: true }, [Validators.required, Validators.email]],
      phoneNumber: [''],
      zipCode: [''],
      profilePicture: ['']
    });

    const email = localStorage.getItem('localStorage_email');
    if (email) {
      this.userService.getByEmail(email).subscribe(
        (response: any) => {
          this.editForm.patchValue({
            id: response.id,
            firstName: response.firstName,
            lastName: response.lastName,
            email: response.email,
            phoneNumber: response.phoneNumber,
            zipCode: response.zipCode,
            profilePicture: response.profilePicture
          });
        },
        (error: any) => {
          this.toastersService.handleError(error);
        }
      );
    }
  }

  editUser() {
    if (this.editForm.invalid) {
      this.toastersService.showError('Please fill in all required fields');
      return;
    }

    const rawValue = this.editForm.getRawValue(); // gets all values, including disabled
    const dto = {
      id: rawValue.id,
      firstName: rawValue.firstName,
      lastName: rawValue.lastName,
      email: rawValue.email,
      phoneNumber: rawValue.phoneNumber,
      zipCode: rawValue.zipCode,
      profilePicture: rawValue.profilePicture
    };

    this.userService.updateUser(dto).subscribe(
      (response: any) => {
        this.toastersService.showSuccess('User edited successfully');
        this.router.navigate(['/']);
      },
      (error: any) => {
        this.toastersService.handleError(error);
      }
    );
  }
}
