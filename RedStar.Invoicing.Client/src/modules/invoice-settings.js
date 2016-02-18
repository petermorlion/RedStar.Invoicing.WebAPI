import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceSettings {
    constructor(http, toastr) {
        this.http = http;
        this.status = 'idle';
    }

    activate() {
        // this.http
        //     // TODO: use /api/
        //     .get(`http://${window.location.host}/api/settings`)
        //     .then(response => {
        //         //this.logoUrl = response.content.LogoUrl;
        //         this.invoiceTemplate = response.content.InvoiceTemplate;
        //     }).catch(err => {
        //         console.log(err);
        //     });
    }

    finishAjaxCall() {
        this.status = 'done';
        var vm = this;
        setTimeout(function() { vm.status = 'idle'; }, 1500);
    }

    submit() {
        this.status = 'busy';

	    //TODO: separate class
	    let reader = new FileReader();
	    reader.onload = () => {
	        let file = reader.result;

	        let settingsDTO = {
	            logo: file,
	            logoName: this.files[0].name,
	            invoiceTemplate: this.invoiceTemplate
	        };

	        this.http.createRequest(`http://${window.location.host}/api/settings`)
                .asPost()
                .withHeader('Content-Type', 'application/json; charset=utf-8')
                .withContent(settingsDTO)
                .send()
                .then(response => {
                    console.log(response.response);
                    this.finishAjaxCall();
                }).catch(err => {
                    // TODO: error handling
                    console.log(err);
                    this.finishAjaxCall();
                });
	    };

	    reader.readAsDataURL(this.files[0]);
	}
}
