import { FormGroup } from '@angular/forms';
import { Component, Input, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Editor, Toolbar } from 'ngx-editor';

@Component
    ({
        selector: "app-editor",
        templateUrl: "./editor.component.html"
    })
export class EidtorAComponent implements OnInit, OnDestroy {

    editor: Editor;
    toolbar: Toolbar = [
        [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
        ['bold', 'italic'],
        ['underline', 'strike'],
        ['code', 'blockquote'],
        ['ordered_list', 'bullet_list'],
        ['link'],
    ];
    html: '';
    text
    @Input() formGroup:FormGroup
    @Input() controlName: string
    
    constructor() { }

    ngOnInit(): void {
        this.editor = new Editor();
    }

    ngOnDestroy(): void {
        this.editor.destroy();
    }
}