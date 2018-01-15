import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

// Components
import { AppComponent } from './components/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { PortfolioComponent } from './components/portfolio/portfolio.component';
import { PurchaseComponent } from './components/purchase/purchase.component';

// Routing module
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PortfolioComponent,
    PurchaseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule      // Routing module
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
