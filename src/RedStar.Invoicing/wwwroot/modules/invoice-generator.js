import {inject} from 'aurelia-framework';
import {computedFrom} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';

@inject(HttpClient)
export class InvoiceGenerator {
    items = [];

    constructor(http) {
        this.http = http;
    }

    addInvoiceItem() {
        this.items.push(new InvoiceItem());
    }

    print() {           
        //TODO: get template from server and then use property instead of DOM
        let html = document.getElementById('invoice-template').innerHTML;
        let json = { html : html, invoiceNumber: invoiceNumber };
        
        this.http.createRequest(`http://${window.location.host}/api/pdf`)
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

    get subtotal() {
        let sum = 0;
        this.items.map(i => sum += i.total);
        return sum;
    }

    get vat() {
        return 0.21 * this.subtotal;
    }

    get total() {
        return this.subtotal + this.vat;
    }
}

export class InvoiceItem {
    amount = 1;
    description = '';
    unitPrice = 1;

    @computedFrom('unitPrice', 'amount')
    get total() {
        return this.unitPrice * this.amount;
    }
}