import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceSettings {
	file = '';
	
	constructor(http) {
		this.http = http;
	}
	
	submit() {
		// TODO: validate + don't crash when no logo
		let reader = new FileReader();
		reader.readAsDataURL(this.file);
		let that = this;
		reader.onload = function($event) {
			//TODO: separate class
			let settingsDTO = {
				logo: $event.target.result,
				logoName: that.file.name,
				invoiceTemplate: that.invoiceTemplate
			};
			
			that.postToServer(settingsDTO, that.http);
		}
	}
	
	fileSelected() {
		this.file = this.$event.target.files[0];
	}
	
	postToServer(settingsDTO, http) {
		http.createRequest(`http://${window.location.host}/api/settings`)
            .asPost()
            .withHeader('Content-Type', 'application/json; charset=utf-8')
            .withContent(settingsDTO)
            .send()
            .then(response => {
                console.log(response.response);
        }).catch(err => {
            console.log(err);
        });
	}
}