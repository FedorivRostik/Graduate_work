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
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './interceptors/jwtInterceptor';
import { MainAdminPageComponent } from './pages/admin_pages/main-admin-page/main-admin-page.component';
import { AdminProductsComponent } from './pages/admin_pages/main-admin-page/admin-products/admin-products.component';
import { AdminProductComponent } from './pages/admin_pages/main-admin-page/admin-products/admin-product/admin-product.component';

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
    AdminProductComponent,
  ],
  imports: [BrowserModule, HttpClientModule, AppRouteModule, FormsModule],
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
