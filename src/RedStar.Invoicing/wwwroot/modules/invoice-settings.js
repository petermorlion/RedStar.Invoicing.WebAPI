import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceSettings {
	file = '';
	
	constructor(http) {
		this.http = http;
	}
	
	submit() {
		let reader = new FileReader();
		reader.readAsDataURL(this.file);
		let that = this;
		reader.onload = function($event) {
			that.postToServer($event, that.http, that.file.name);
		}
	}
	
	fileSelected() {
		this.file = this.$event.target.files[0];
	}
	
	postToServer(loadEvent, http, fileName) {
		let json = { logo: loadEvent.target.result, logoName: fileName };
		
		http.createRequest(`http://${window.location.host}/api/settings`)
            .asPost()
            .withHeader('Content-Type', 'application/json; charset=utf-8')
            .withContent(json)
            .send()
            .then(response => {
                console.log(response.response);
        }).catch(err => {
            console.log(err);
        });
	}
}