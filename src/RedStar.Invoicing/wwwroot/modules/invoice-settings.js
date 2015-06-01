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
		reader.readAsText(this.file, 'UTF-8');
		let that = this;
		reader.onload = function($event) {
			that.postToServer($event, that.http);
		}
	}
	
	fileSelected() {
		this.file = this.$event.target.files[0];
	}
	
	postToServer(loadEvent, http) {
		let json = { logo: event.target.result };
		
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