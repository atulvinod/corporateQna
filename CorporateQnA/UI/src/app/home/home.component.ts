import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, TemplateRef, OnInit } from '@angular/core';
import { faSearch, faPlus, faRedo, faThumbsUp, faThumbsDown, faExpand, faExpandAlt, faCompress, faCompressArrowsAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component
    ({
        selector: "app-home",
        templateUrl: "./home.component.html"
    })
export class HomeComponent implements OnInit{
   
    //Icons
    faSearch = faSearch;
    faPlus = faPlus;
    faRedo = faRedo;
    faExpandAlt = faExpandAlt
    faCompressAlt = faCompressAlt

    searchForm: FormGroup;
    newQuestionForm: FormGroup;
    
    toggleFlyoutEditor = false;
    modalRef: BsModalRef;

    categoryOptions: string[] = ["all", "asp.net", "java", "node.js", "dev ops", "ux design"]
    showOptions: string[] = ["all", "my questions", "my participation", "hot", "solved", "unsolved"]
    sortByOptions: string[] = ["all", "recent", "last 10 days", "last 30 days"]

    constructor(private modalService: BsModalService) {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            category: new FormControl("0"),
            show: new FormControl("0"),
            sortBy: new FormControl("0")
        })

        this.newQuestionForm = new FormGroup({
            title: new FormControl("",[Validators.required]),
            content : new FormControl("",[Validators.required]),
            questionCategory: new FormControl("")
        })
    }

    ngOnInit(){
        this.searchForm.get("category").valueChanges.subscribe(value=>{
            console.log(value);
        })
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template,{class:"custom-modal"});
    }
}