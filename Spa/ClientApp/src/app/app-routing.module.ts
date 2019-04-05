import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { HomeComponent } from "./home/home.component";
import { SelectionComponent } from "./selection/selection.component";

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "selection", component: SelectionComponent, },
  {
    path: "selection/:step",
    component: SelectionComponent,
    children: [
      {
        path: "",
        redirectTo: "step1",
        pathMatch: "full"
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
