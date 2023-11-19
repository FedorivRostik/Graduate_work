import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RadiationMapComponent } from './pages/maps/radiation-map/radiation-map.component';
import { HomeComponent } from './base/home/home.component';
import { SignUpComponent } from './base/sign-up/sign-up.component';
import { LogInComponent } from './base/log-in/log-in.component';
import { AllWrapperComponent } from './base/wrappers/all-wrapper/all-wrapper.component';
import { CleanWrapperComponent } from './base/wrappers/clean-wrapper/clean-wrapper.component';

const routes: Routes = [
  {
    path: '',
    component: AllWrapperComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: '', component: HomeComponent },
      { path: 'maps/radiation-map', component: RadiationMapComponent },
    ],
  },
  {
    path: '',
    component: CleanWrapperComponent,
    children: [
      { path: 'sign-up', component: SignUpComponent },
      { path: 'log-in', component: LogInComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRouteModule {}
