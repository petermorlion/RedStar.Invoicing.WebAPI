import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';
import {Configuration} from '../components/config.js';

@inject(HttpClient, Configuration)
export class InvoiceSettings {
    constructor(http, config) {
        this.http = http;
        this.config = config;
        this.status = 'idle';
        this.reader = new FileReader();
    }

    activate() {
        this.http
            .get(`http://${this.config.serverUri}/api/settings`)
            .then(response => {
                //this.logoUrl = response.content.LogoUrl;
                this.invoiceTemplate = response.content.InvoiceTemplate;
            }).catch(err => {
                console.log(err);
            });
    }

    finishAjaxCall() {
        this.status = 'done';
        var vm = this;
        setTimeout(function() { vm.status = 'idle'; }, 1500);
    }
    
    hasLogoFile() {
        return this.files && this.files.length > 0;
    }
    
    startAjaxCall() {
        let file = this.reader.result;

        let settingsDTO = {
            logo: file,
            //logoName: this.hasLogoFile() ? this.files[0].name : '',
            invoiceTemplate: this.invoiceTemplate
        };

        this.http.createRequest(`http://${this.config.serverUri}/api/settings`)
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

    submit() {
        this.status = 'busy';

	    this.reader.onload = this.startAjaxCall;

        if (this.hasLogoFile()) {
            this.reader.readAsDataURL(this.files[0]);
        } else {
            this.startAjaxCall();
        }
	}
}
