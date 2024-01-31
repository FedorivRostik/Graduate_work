import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RadiationMapComponent } from './pages/maps/radiation-map/radiation-map.component';
import { HomeComponent } from './base/home/home.component';
import { SignUpComponent } from './base/sign-up/sign-up.component';
import { LogInComponent } from './base/log-in/log-in.component';
import { AllWrapperComponent } from './base/wrappers/all-wrapper/all-wrapper.component';
import { CleanWrapperComponent } from './base/wrappers/clean-wrapper/clean-wrapper.component';
import { ProductsComponent } from './pages/products/products/products.component';
import { MainAdminPageComponent } from './pages/admin_pages/main-admin-page/main-admin-page.component';
import { AdminProductsComponent } from './pages/admin_pages/main-admin-page/admin-products/admin-products.component';
import { AdminUsersComponent } from './pages/admin_pages/main-admin-page/admin-users/admin-users.component';

const routes: Routes = [
  {
    path: '',
    component: AllWrapperComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: '', component: HomeComponent },
      { path: 'maps/radiation-map', component: RadiationMapComponent },
      { path: 'products/products', component: ProductsComponent },
    ],
  },
  {
    path: '',
    component: CleanWrapperComponent,
    children: [
      { path: 'sign-up', component: SignUpComponent },
      { path: 'log-in', component: LogInComponent },
      {
        path: 'admin-panel',
        component: MainAdminPageComponent,
        children: [
          { path: 'products', component: AdminProductsComponent },
          { path: 'users', component: AdminUsersComponent },
        ],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRouteModule {}
