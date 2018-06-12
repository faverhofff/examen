import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { IndexComponent } from './index/index.component';
import { TopMenuComponent } from './topmenu/top.menu.component';
import { SearchComponent } from './search/search.component';
import { PreloaderComponent } from './preloader/preloader.component';
import { AppComponent } from './app/app.component';

import { AppRoutingModule } from './routing.module';
import { GlobalVars } from './app.global';

@NgModule({
  declarations: [
      IndexComponent,
      TopMenuComponent,
      SearchComponent,
      PreloaderComponent,
      AppComponent
  ],
  imports: [
      BrowserModule,
      AppRoutingModule,
      HttpModule,
      FormsModule
  ],
  providers: [
    GlobalVars
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
