import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'app-view-vehicle',
    templateUrl: './view-vehicle.component.html',
    styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {

    vehicle: any;
    vehicleId: number;
    constructor(
        private vehicleService: VehicleService,
        private toastyService: ToastyService,
        private route: ActivatedRoute,
        private router: Router)
    {
        route.params.subscribe(p => {
            this.vehicleId = +p['id'];
            if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
                this.router.navigate(["/vehicles"]);
                return;
            }
        });
    }

    ngOnInit() {
        this.vehicleService.getVehicle(this.vehicleId)
            .subscribe(
                v => this.vehicle = v,
                err => {
                    if (err.status == 404)
                    {
                        this.router.navigate(["/vehicles"]);
                        return;
                    }
                }
        );
    }

    delete() {
        if (confirm("Are you sure?")) {
            this.vehicleService.delete(this.vehicleId)
                .subscribe(x => {
                    this.router.navigate(["/vehicles"]);
                });
        }
    }

}
