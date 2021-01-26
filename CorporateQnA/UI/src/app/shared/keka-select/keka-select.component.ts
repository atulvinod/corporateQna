import { FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';
import { faAd } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-keka-select',
    templateUrl: './keka-select.component.html',
})
export class KekaSelectComponent implements OnInit {

    @Input() formGroup: FormGroup;
    @Input() controlName: string;
    @Input() options:string[] = []
    
    selectID: string

    constructor() { }

    ngOnInit(): void {}

    generated = false;
    ngAfterViewInit() {
        if (this.generated == false) {
            this.renderSelect()
            this.generated = true
        }
    }

    renderSelect() {
        let selects, selectsCount, selectElement, selectedItemDiv, optionsList, optionItem;
      
        let searchForm = this.formGroup;
        let control = this.controlName;
        selects = document.getElementById(this.selectID);

        selectElement = selects.getElementsByTagName("select")[0];
        selectsCount = selectElement.length;
        /*for each element, create a new DIV that will act as the selected item:*/
        selectedItemDiv = document.createElement("DIV");
        selectedItemDiv.setAttribute("class", "select-selected");
        selectedItemDiv.innerHTML = selectElement.options[selectElement.selectedIndex].innerHTML;
        selects.appendChild(selectedItemDiv);
        /*for each element, create a new DIV that will contain the option list:*/
        optionsList = document.createElement("DIV");
        optionsList.setAttribute("class", "select-items select-hide");

      
        for (let j = 1; j < selectsCount; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            optionItem = document.createElement("DIV");
            optionItem.innerHTML = selectElement.options[j].innerHTML;

            optionItem.addEventListener("click", function (e) {
                let selected, selecteElement, displaySelected, optionsCount, selectedLength;
                selecteElement = this.parentNode.parentNode.getElementsByTagName("select")[0];
                optionsCount = selecteElement.length;
                displaySelected = this.parentNode.previousSibling;
                for (let i = 0; i < optionsCount; i++) {
                    if (selecteElement.options[i].innerHTML == this.innerHTML) {
                        selecteElement.selectedIndex = i;
                        displaySelected.innerHTML = this.innerHTML;
                        // --------------
                        let controlName: string = control;
                        searchForm.get(controlName).patchValue(this.innerHTML);
                        //---------------
                        selected = this.parentNode.getElementsByClassName("same-as-selected");
                        selectedLength = selected.length;
                        for (let k = 0; k < selectedLength; k++) {
                            selected[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                displaySelected.click();
            });
            optionsList.appendChild(optionItem);
        }

        selects.appendChild(optionsList);

        selectedItemDiv.addEventListener("click", function (e) {
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");
        });


        function closeAllSelect(elmnt) {
            let selectedOptions, optionsLength, selectedLength, arrNo = [];
            let options = document.getElementsByClassName("select-items");
            selectedOptions = document.getElementsByClassName("select-selected");
            optionsLength = options.length;
            selectedLength = selectedOptions.length;
            
            let i;
            for (i = 0; i < selectedLength; i++) {
                if (elmnt == selectedOptions[i]) {
                    arrNo.push(i);
                } else {
                    selectedOptions[i].classList.remove("select-arrow-active");
                }
            }
            for (i = 0; i < optionsLength; i++) {
                if (arrNo.indexOf(i)) {
                    options[i].classList.add("select-hide");
                }
            }
        }
        document.addEventListener("click", closeAllSelect);
    }
}
