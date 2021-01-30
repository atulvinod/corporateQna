import { SearchFilterModel } from './../../models/search-filter.model';
import { QuestionActivityEnum } from './../../models/enum/question-activity.enum';
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

    searchForm: FormGroup;
    newAnswer: FormGroup;
    newQuestionForm: FormGroup;

    toggleFlyoutEditor = false;
    modalRef: BsModalRef;

    categoryOptions: CategoryModel[] = []

    user: any
    currentQuestion: QuestionDetailsModel;

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
            content: new FormControl("", [Validators.required]),
            questionCategory: new FormControl("0")
        })

        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required]),
        })
    }

    ngOnInit() {
        this.categoryService.getCategories().subscribe(categories => {
            this.categoryOptions = [...this.categoryOptions, ...categories]
            console.log("Categories:", this.categoryOptions)
        })

        //TODO: check if logged in, if not, redirect to login screen
        this.oidcService.userData$.subscribe(value => {
            this.user = value;
            console.log("userdata :", this.user);
        })

        this.questionService.getQuestions().subscribe(value => {
            this.allQuestions = [...value];
        });

        this.searchForm.valueChanges.pipe(debounceTime(420)).subscribe((value:SearchFilterModel) => { 
            value.userId = this.user['userId']
            value.categoryId = Number(value.categoryId);
            value.show = Number(value.show);
            value.sortBy = Number(value.sortBy)
            console.log("Search filter ",value);
            this.questionService.searchQuestion(value).subscribe(values=>{
                this.allQuestions = values;
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
        console.log(question);
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
        this.questionService.createQuestionActivity(act).subscribe(value => {
            if (value != 0) {
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
        this.searchForm.get("category").patchValue(0)
        this.searchForm.get("show").patchValue(0)
        this.searchForm.get("sortBy").patchValue(0)
    }

}
