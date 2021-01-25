import { FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-keka-select',
  templateUrl: './keka-select.component.html',
})
export class KekaSelectComponent implements OnInit {

  @Input() formGroup:FormGroup;
  @Input() controlName: string;
  
  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit(){
    this.renderSelect()
  }

  renderSelect() {
    let selects, selectsCount, selectElement, selectedItemDiv, optionsList, optionItem;
    let i, j, l;
    let searchForm = this.formGroup;
    let control = this.controlName;
    selects = document.getElementsByClassName("keka-custom-select");
    console.log(selects)
    l = selects.length;

    for (i = 0; i < l; i++) {
        selectElement = selects[i].getElementsByTagName("select")[0];
        selectsCount = selectElement.length;
        /*for each element, create a new DIV that will act as the selected item:*/
        selectedItemDiv = document.createElement("DIV");
        selectedItemDiv.setAttribute("class", "select-selected");
        selectedItemDiv.innerHTML = selectElement.options[selectElement.selectedIndex].innerHTML;
        selects[i].appendChild(selectedItemDiv);
        /*for each element, create a new DIV that will contain the option list:*/
        optionsList = document.createElement("DIV");
        optionsList.setAttribute("class", "select-items select-hide");

        for (j = 1; j < selectsCount; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            optionItem = document.createElement("DIV");
            optionItem.innerHTML = selectElement.options[j].innerHTML;

            optionItem.addEventListener("click", function (e) {
                let y, i, k, s, h, sl, yl;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                    if (s.options[i].innerHTML == this.innerHTML) {
                        s.selectedIndex = i;
                        h.innerHTML = this.innerHTML;
                        // --------------
                        console.log(this.innerHTML)
                        console.log(s.getAttribute("formcontrolname"))
                        let controlName:string = control;
                        searchForm.get(controlName).patchValue(this.innerHTML);
                        //---------------
                        y = this.parentNode.getElementsByClassName("same-as-selected");
                        yl = y.length;
                        for (k = 0; k < yl; k++) {
                            y[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                h.click();
            });
            optionsList.appendChild(optionItem);
        }

        selects[i].appendChild(optionsList);

        selectedItemDiv.addEventListener("click", function (e) {
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");
        });
    }

    function closeAllSelect(elmnt) {
        let x,
            y,
            i,
            xl,
            yl,
            arrNo = [];
        x = document.getElementsByClassName("select-items");
        y = document.getElementsByClassName("select-selected");
        xl = x.length;
        yl = y.length;
        for (i = 0; i < yl; i++) {
            if (elmnt == y[i]) {
                arrNo.push(i);
            } else {
                y[i].classList.remove("select-arrow-active");
            }
        }
        for (i = 0; i < xl; i++) {
            if (arrNo.indexOf(i)) {
                x[i].classList.add("select-hide");
            }
        }
    }
    document.addEventListener("click", closeAllSelect);

}
}
