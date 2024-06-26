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
import { AddAdminProductComponent } from './pages/admin_pages/main-admin-page/admin-products/add-admin-product/add-admin-product.component';
import { AdminGenresComponent } from './pages/admin_pages/main-admin-page/admin-genres/admin-genres.component';
import { AddAdminGenreComponent } from './pages/admin_pages/main-admin-page/admin-genres/add-admin-genre/add-admin-genre.component';
import { CartComponent } from './pages/cart/cart.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { PayComponent } from './pages/pay/pay.component';
import { PaymentRedirectComponent } from './pages/payment-redirect/payment-redirect.component';
import { GetRadiationInfoComponent } from './pages/maps/radiation-map/get-radiation-info/get-radiation-info.component';
import { AirMapComponent } from './pages/maps/air-map/air-map.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { UpdateProfileComponent } from './pages/profile/update-profile/update-profile.component';

const routes: Routes = [
  {
    path: '',
    component: AllWrapperComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: '', component: HomeComponent },

      {
        path: 'maps/radiation-map',
        component: RadiationMapComponent,
        children: [{ path: ':value', component: GetRadiationInfoComponent }],
      },
      {
        path: 'maps/air-map',
        component: AirMapComponent,
        children: [{ path: ':value', component: GetRadiationInfoComponent }],
      },

      { path: 'products/products', component: ProductsComponent },
      { path: 'cart', component: CartComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'pay/redirect', component: PaymentRedirectComponent },
      { path: 'pay', component: PayComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'profile/update', component: UpdateProfileComponent },
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
          { path: 'products/add', component: AddAdminProductComponent },
          { path: 'products', component: AdminProductsComponent },
          { path: 'users', component: AdminUsersComponent },
          { path: 'genres/add', component: AddAdminGenreComponent },
          { path: 'genres', component: AdminGenresComponent },
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
