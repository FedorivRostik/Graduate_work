import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/layouts/header/header.component';
import { FooterComponent } from './components/layouts/footer/footer.component';
import { RadiationMapComponent } from './pages/maps/radiation-map/radiation-map.component';
import { AppRouteModule } from './app-routing.module';
import { HomeComponent } from './base/home/home.component';
import { SignUpComponent } from './base/sign-up/sign-up.component';
import { LogInComponent } from './base/log-in/log-in.component';
import { CleanWrapperComponent } from './base/wrappers/clean-wrapper/clean-wrapper.component';
import { AllWrapperComponent } from './base/wrappers/all-wrapper/all-wrapper.component';
import { ProductsComponent } from './pages/products/products/products.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './interceptors/jwtInterceptor';
import { MainAdminPageComponent } from './pages/admin_pages/main-admin-page/main-admin-page.component';
import { AdminProductsComponent } from './pages/admin_pages/main-admin-page/admin-products/admin-products.component';
import { AdminUsersComponent } from './pages/admin_pages/main-admin-page/admin-users/admin-users.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppDialogComponent } from './components/app-dialog/app-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AddAdminProductComponent } from './pages/admin_pages/main-admin-page/admin-products/add-admin-product/add-admin-product.component';
import { AdminGenresComponent } from './pages/admin_pages/main-admin-page/admin-genres/admin-genres.component';
import { AddAdminGenreComponent } from './pages/admin_pages/main-admin-page/admin-genres/add-admin-genre/add-admin-genre.component';
import { CartComponent } from './pages/cart/cart.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    RadiationMapComponent,
    HomeComponent,
    SignUpComponent,
    LogInComponent,
    CleanWrapperComponent,
    AllWrapperComponent,
    ProductsComponent,
    MainAdminPageComponent,
    AdminProductsComponent,
    AdminUsersComponent,
    AppDialogComponent,
    AddAdminProductComponent,
    AdminGenresComponent,
    AddAdminGenreComponent,
    CartComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRouteModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
