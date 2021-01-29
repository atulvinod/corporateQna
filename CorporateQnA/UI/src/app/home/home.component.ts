import { QuestionService } from './../services/question.service';
import { QuestionModel } from './../../models/question.model';
import { AnswerModel } from './../../models/answer.model';
import { AnswerService } from './../services/answer.service';
import { CategoryModel } from './../../models/category.model';
import { CategoryService } from './../services/category.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, TemplateRef, OnInit } from '@angular/core';
import { faSearch, faPlus, faRedo, faThumbsUp, faThumbsDown, faExpand, faExpandAlt, faCompress, faCompressArrowsAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { QuestionDetailsModel } from 'src/models/question-details.model';

@Component
    ({
        selector: "app-home",
        templateUrl: "./home.component.html"
    })
export class HomeComponent implements OnInit {

    //Icons
    faSearch = faSearch;
    faPlus = faPlus;
    faRedo = faRedo;
    faExpandAlt = faExpandAlt
    faCompressAlt = faCompressAlt

    searchForm: FormGroup;
    newAnswer: FormGroup;
    newQuestionForm: FormGroup;

    toggleFlyoutEditor = false;
    modalRef: BsModalRef;

    // categoryOptions: string[] = ["all", "asp.net", "java", "node.js", "dev ops", "ux design"]
    categoryOptions: CategoryModel[] = []
    showOptions: string[] = ["all", "my questions", "my participation", "hot", "solved", "unsolved"]
    sortByOptions: string[] = ["all", "recent", "last 10 days", "last 30 days"]

    text: string = "";

    userData: any
    currentQuestion: QuestionDetailsModel;

    allQuestions:QuestionDetailsModel[] = []
    showQuestions: QuestionDetailsModel[] = []

    constructor(private modalService: BsModalService, private categoryService: CategoryService, private oidcService: OidcSecurityService, private answerService: AnswerService, private questionService: QuestionService) {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            category: new FormControl("0"),
            show: new FormControl("0"),
            sortBy: new FormControl("0")
        })

        this.newQuestionForm = new FormGroup({
            title: new FormControl("", [Validators.required]),
            content: new FormControl("", [Validators.required]),
            questionCategory: new FormControl("")
        })

        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required]),
        })

        // this.categoryOptions.push({ name: "All", id: "0", description: "none" });
        console.log("on init", this.categoryOptions);
    }

    ngOnInit() {
        this.searchForm.get("category").valueChanges.subscribe(value => {
            console.log(value);
        })

        this.categoryService.getCategories().subscribe(categories => {
            this.categoryOptions = [...this.categoryOptions, ...categories]
            console.log("Categories:", this.categoryOptions)
        })

        //TODO: check if logged in, if not, redirect to login screen
        this.oidcService.userData$.subscribe(value => {
            this.userData = value;
            console.log("userdata :", this.userData);
        })

        this.questionService.getQuestions().subscribe(value=>{
            this.allQuestions = [...value];
            this.showQuestions = [...value];
        });
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: "custom-modal" });
    }

    createQuestion() {
        let askedBy = this.userData['userId']
        let categoryId = this.newQuestionForm.get("questionCategory").value;
        let content = this.removeTags(this.newQuestionForm.get("content").value);
        let title = this.newQuestionForm.get("title").value;
        let question: QuestionModel = new QuestionModel({ askedBy, categoryId, content, title })
        console.log(question);
    }

    removeTags(str) {
        if ((str === null) || (str === ''))
            return false;
        else
            str = str.toString();

        // Regular expression to identify HTML tags in  
        // the input string. Replacing the identified  
        // HTML tag with a null string. 
        return str.replace(/(<([^>]+)>)/ig, '');
    }
}