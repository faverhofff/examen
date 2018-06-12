import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { IndexComponent } from './index/index.component';
import { TopMenuComponent } from './topmenu/top.menu.component';
import { SearchComponent } from './search/search.component';

const routes: Routes = [
    { path: 'index', component: IndexComponent },
    { path: 'search', component: SearchComponent },
    { path: '', component: SearchComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ],
    declarations: []
})

export class AppRoutingModule { }