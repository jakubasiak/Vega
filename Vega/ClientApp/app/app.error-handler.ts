import { ErrorHandler, Inject, NgZone } from "@angular/core";
import { ToastyService } from "ng2-toasty";

export class AppErrorHandler implements ErrorHandler {
    constructor(
        @Inject(NgZone) private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService,
    )
    {
    }
    handleError(error: any): void {
        this.ngZone.run(() => {
            if (typeof (window) !== 'undefined') {
                var message: string = "An unexpected error happened.";
                if (error.status == 404)
                    message = "404 not found."
                this.toastyService.error({
                    title: "Error",
                    msg: message,
                    showClose: true,
                    timeout: 5000,
                    theme: 'bootstrap'
                });
            }
         });
    }

}