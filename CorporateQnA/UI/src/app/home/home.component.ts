import { SearchFilterModel } from './../../models/search-filter.model';
import { QuestionActivityEnum } from './../../models/enum/question-activity.enum';
import { QuestionService } from './../services/question.service';
import { QuestionModel } from './../../models/question.model';
import { AnswerModel } from './../../models/answer.model';
import { AnswerService } from './../services/answer.service';
import { CategoryModel } from './../../models/category.model';
import { CategoryService } from './../services/category.service';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Component, TemplateRef, OnInit } from '@angular/core';
import { faSearch, faPlus, faRedo, faThumbsUp, faThumbsDown, faExpand, faExpandAlt, faCompress, faCompressArrowsAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { QuestionDetailsModel } from 'src/models/question-details.model';
import { QuestionActivityModel } from 'src/models/question-activity.model';
import { debounceTime } from 'rxjs/operators';

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

    //Form groups
    searchForm: FormGroup;
    newAnswer: FormGroup;
    newQuestionForm: FormGroup;

    //Modal controls
    toggleFlyoutEditor = false;
    modalRef: BsModalRef;

    //Current User
    user: any

    //Models
    currentQuestion: QuestionDetailsModel;
    categoryOptions: CategoryModel[] = []
    allQuestions: QuestionDetailsModel[] = []

    constructor(private modalService: BsModalService, private categoryService: CategoryService, private oidcService: OidcSecurityService, private answerService: AnswerService, private questionService: QuestionService) {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            categoryId: new FormControl(0),
            show: new FormControl(0),
            sortBy: new FormControl(0)
        })

        this.newQuestionForm = new FormGroup({
            title: new FormControl("", [Validators.required]),
            content: new FormControl("", [Validators.required, this.editorValidator()]),
            questionCategory: new FormControl("0", [Validators.required, this.categoryIdValidator()])
        })

        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required]),
        })
    }

    ngOnInit() {
        this.categoryService.getCategories().subscribe(categories => {
            this.categoryOptions = categories
        })

        this.oidcService.userData$.subscribe(user => {
            this.user = user;
        })

        this.questionService.getQuestions().subscribe(questions => {
            this.allQuestions = [...questions];
        });

        this.searchForm.valueChanges.pipe(debounceTime(420)).subscribe((filter: SearchFilterModel) => {

            filter.userId = this.user['userId']
            filter.categoryId = Number(filter.categoryId);
            filter.show = Number(filter.show);
            filter.sortBy = Number(filter.sortBy)

            this.questionService.searchQuestion(filter).subscribe(questions => {
                this.allQuestions = questions;
                this.currentQuestion = null;
            })
        })
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: "custom-modal" });
    }

    createQuestion() {
        let askedBy = this.user['userId']
        let categoryId = this.newQuestionForm.get("questionCategory").value;
        let content = this.removeTags(this.newQuestionForm.get("content").value);
        let title = this.newQuestionForm.get("title").value;

        let question: QuestionModel = new QuestionModel({ askedBy, categoryId, content, title })

        this.questionService.createQuestion(question).subscribe(value => {
            let questionData = new QuestionDetailsModel({
                questionId: value,
                userName: this.user['userName'],
                questionTitle: title,
                content,
                askedBy,
                categoryId,
                likeCount: 0,
                viewCount: 0,
                resolved: 0,
                answerCount: 0
            })

            this.allQuestions.push(questionData)
            this.modalRef.hide();
        })
    }

    viewQuestion(question: QuestionDetailsModel) {

        this.currentQuestion = question;

        //for view activity
        let act = new QuestionActivityModel({
            userId: this.user['userId'],
            questionId: question.questionId,
            activityType: QuestionActivityEnum.View
        })

        //send view request
        this.questionService.createQuestionActivity(act).subscribe(response => {
            if (response != 0) {
                this.currentQuestion.viewCount++;
            }
        })

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

    resetSearch() {
        this.searchForm.get("searchInput").patchValue("")
        this.searchForm.get("categoryId").patchValue(0)
        this.searchForm.get("show").patchValue(0)
        this.searchForm.get("sortBy").patchValue(0)
    }

    resetNewQuestion(){
        this.newQuestionForm.reset();
        this.newQuestionForm.get("content").patchValue("")
        this.newQuestionForm.get("questionCategory").patchValue(0);
        this.modalRef.hide()
    }

    categoryIdValidator(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null => {
            return control.value == 0 ? { "categoryId": "invalid category id" } : null;
        };
    }

    editorValidator(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null => {
            console.log("validator ", this.removeTags(control.value))
            let empty = this.removeTags(control.value).length == 0

            return empty ? { "empty": "Empty content" } : null;
        };
    }
}
