import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

    makes: any[];
    models: any[];
    features: any[];
    vehicle: any = {
        features: [],
        contact:{}
    };

    constructor(
        private vehicleService: VehicleService,
        private toastyService: ToastyService,
        private route: ActivatedRoute,
        private router: Router) {
        route.params.subscribe(p => {
            this.vehicle.id = +p['id'];
        });
    }

    ngOnInit() {
        var sources = [
        this.vehicleService.getMakes(),
        this.vehicleService.getFeatures()
        ];

        if (this.vehicle.id)
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[0];
            this.features = data[1];

            if (this.vehicle.id)
                this.vehicle = data[2];
        });

    }
    onMakeChanged() {
        var selectedMake = this.makes.find(make => make.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
        delete this.vehicle.modelId;

    }
    onFeatureToggle(featureId: number, $event: any) {
        if ($event.target.checked)
        {
            this.vehicle.features.push(featureId);
        }
        else
        {
            let index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }
    submit() {
        this.vehicleService.create(this.vehicle)
            .subscribe(
            x => console.log(x));
    }

}
