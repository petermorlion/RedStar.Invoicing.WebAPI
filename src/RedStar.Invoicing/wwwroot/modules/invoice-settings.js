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
			logoName: this.fileName,
			invoiceTemplate: this.invoiceTemplate
		};
		
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
	
	fileSelected() {
		let reader = new FileReader();
		let file = this.$event.target.files[0];
		reader.readAsDataURL(file);
		this.fileName = file.name;
		reader.onload = () => {
			this.file = reader.result;
		};
	}
}