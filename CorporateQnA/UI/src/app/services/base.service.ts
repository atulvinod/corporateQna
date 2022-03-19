import {isDevMode} from '@angular/core';

export class BaseService {
    private httpRoot = "https://localhost:5001";

    getHttpRoot(){
        if(isDevMode()){
            return this.httpRoot;
        }else{
            return window.location.origin;
        }
    }
}
