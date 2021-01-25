
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { KekaSelectComponent } from "./keka-select.component";

@NgModule({
    imports:[CommonModule,
        ReactiveFormsModule
    ],
    declarations:[
        KekaSelectComponent,
    ],
    exports:[
        KekaSelectComponent
    ]
})
export class SharedModule{}