import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';
import {toastr} from 'toastr';

@inject(HttpClient)
@inject(toastr)
export class InvoiceSettings {
    constructor(http, toastr) {
        this.http = http;
        this.toastr = toastr;
	}
	
    submit() {
        toastr.info("Saving...");
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
                    toastr.info("Settings saved")
                }).catch(err => {
                    console.log(err);
                });
	    };

	    reader.readAsDataURL(this.files[0]);
	}
}