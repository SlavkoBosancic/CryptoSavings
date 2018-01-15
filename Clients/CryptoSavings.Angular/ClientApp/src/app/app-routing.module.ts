import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//Components
import { PortfolioComponent } from './components/portfolio/portfolio.component';
import { PurchaseComponent } from './components/purchase/purchase.component';

const routes: Routes = [
    { path: '', redirectTo: '/portfolio', pathMatch: 'full' },
    { path: 'portfolio', component: PortfolioComponent },
    { path: 'purchase', component: PurchaseComponent },
    { path: '**', redirectTo: '/portfolio' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }