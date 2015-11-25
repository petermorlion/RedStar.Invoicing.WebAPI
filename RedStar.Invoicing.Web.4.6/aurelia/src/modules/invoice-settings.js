import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceSettings {
    constructor(http, toastr) {
        this.http = http;
        this.status = 'idle';
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
                    this.status = 'done';
                }).catch(err => {
                    console.log(err);
                    this.status = 'done';
                });

	        setTimeout(function() {
	            this.status = 'idle';
	        }, 1500);
	    };

	    reader.readAsDataURL(this.files[0]);
	}
}