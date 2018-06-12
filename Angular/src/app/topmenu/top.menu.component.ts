import { Component, Input, Output, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormGroup, FormControl } from '@angular/forms';
import { GlobalVars } from '../app.global';

@Component({
    selector: 'top-menu-root',
    templateUrl: './top.menu.component.html',
    styleUrls: ['./top.menu.component.css'],
    providers: [ GlobalVars ]
})

export class TopMenuComponent {
    @Input() placeholder: string;
    @Input() linkcount: boolean = true;
    @Input() action: string;

    private data: any[];
    private input_data: any[];
    private message_error: string;
    private preload: boolean = false;
    private result: any[];  
    private url_index: any[];


    private urlSearch = this.globals.url+"search";
    private urlIndex = this.globals.url+"index";
    private urlClear = this.globals.url+"clear";

    constructor(private http: Http, private globals: GlobalVars) {
    }

    private clearMessages() {
        this.preload = true;
        this.message_error = "";
    }

    private showMessages(response) {
        this.preload = false; 
        
        if (response.json().Word)
          this.message_error = response.json().Word[0];
        if (response.json().Url)  
          this.message_error = response.json().Url[0];
        if (!response.json().Word && !response.json().Url)
          this.data = response.json();
    }

    public execute() {
        switch(this.action) {
            case "1":
                this.search();
                break;
            case "2":
                this.indexer();
                break;
        }
    }

    public search() {
        this.clearMessages();
    
        this.http.get(this.urlSearch, {
          params: { Word: this.input_data}
        }).subscribe(response => {
          this.showMessages(response);
        });
    } 

    public indexer() {
        this.clearMessages();
    
        this.http.get(this.urlIndex, {
          params: { Url: this.input_data}
        }).subscribe(response => {
          this.showMessages(response);
        });
    }  
    
    clearDB() {
        this.http.get(this.urlClear).subscribe(response => {
            alert("Database cleared");
        });        
     }
  
    haveItems() {
        return this.result.length > 0;
    }    
}