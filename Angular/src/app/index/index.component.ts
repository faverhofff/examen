import { Http } from '@angular/http';
import { Component } from '@angular/core';
import { GlobalVars } from '../app.global';

@Component({
  selector: 'index-root',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css'],
  providers: [ GlobalVars ]
})
export class IndexComponent {

    private urlIndex;
    private urlClear;
    private result: any[];  
    url_index: any[];
    
    constructor(private http: Http, private globals: GlobalVars) {    
    }

}
