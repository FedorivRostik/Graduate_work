import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';

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
  ],
  imports: [BrowserModule, HttpClientModule, AppRouteModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
