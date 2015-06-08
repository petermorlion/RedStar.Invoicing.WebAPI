import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceSettings {
	file = '';
	
	constructor(http) {
		this.http = http;
	}
	
	submit() {
		//TODO: separate class
		let settingsDTO = {
			logo: this.file,
			logoName: this.file.name,
			invoiceTemplate: this.invoiceTemplate
		};
		
		this.postToServer(settingsDTO);
	}
	
	fileSelected() {
		let reader = new FileReader();
		let file = this.$event.target.files[0];
		reader.readAsDataURL(file);
		this.fileName = file.name;
		let that = this;
		reader.onload = function() {
			that.file = this.result;
		};
	}
	
	postToServer(settingsDTO) {
		this.http.createRequest(`http://${window.location.host}/api/settings`)
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