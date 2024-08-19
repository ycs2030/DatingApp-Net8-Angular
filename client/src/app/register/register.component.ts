import { Component, EventEmitter, inject, input, Input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  private accountService = inject(AccountService);

  // // this with angular 17 only because
  // @Input() usersFromHomeComponent: any;
  // this with angular 18 only because data from the parent component is not available in the child component
 // usersFromHomeComponent = input.required<any>();

  // // this old way from child to parent
  // @Output() cancelRegister = new EventEmitter();

  // new way from child to parent angular 18
  cancelRegister = output<boolean>();



  model: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next:(response:any) => {
        console.log(response);
        this.cancel();
      },
      error: (error:any) => {
        console.log(error);
      },
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
