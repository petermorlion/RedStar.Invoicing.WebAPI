import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-http-client';
import {InvoiceTemplate} from '../components/invoice-template.js';
import {Invoice} from '../components/invoice.js';
import {Configuration} from '../components/config.js';

@inject(HttpClient, Configuration)
export class InvoiceGenerator {
    constructor(http, config) {
        this.http = http;
        this.config = config;
    }

    activate() {
        // this.http
        //     // TODO: use /api/
        //     .get(`http://${window.location.host}/api/invoicegenerator`)
        //     .then(response => {
        //         this.logoUrl = response.content.LogoUrl;
        //         //this.invoiceTemplate = response.content.InvoiceTemplate;
        //         this.invoice = new Invoice();
        //         this.invoiceTemplate = new InvoiceTemplate(`<template>${response.content.InvoiceTemplate}</template>`, this.invoice);
        //     }).catch(err => {
        //         console.log(err);
        //     });
    }

    print() {
        //TODO: get template from server and then use property instead of DOM
        let html = document.getElementById('invoice-template').innerHTML;
        let json = { html : html, invoiceNumber: this.invoiceNumber };

        this.http.createRequest(`http://${this.config.serverUri}/api/invoice`)
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
