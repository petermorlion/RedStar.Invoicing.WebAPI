import {InlineViewStrategy} from 'aurelia-framework';

export class InvoiceTemplate {
    constructor(templateHtml, model) {
        this.templateHtml = templateHtml;
        this.model = model;
    }

    getViewStrategy(){  
        return new InlineViewStrategy(this.templateHtml);
    }
}