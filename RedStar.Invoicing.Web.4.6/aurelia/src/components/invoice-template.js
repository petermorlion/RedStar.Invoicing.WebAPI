import {InlineViewStrategy} from 'aurelia-framework';

export class InvoiceTemplate {
    constructor(templateHtml){
        this.templateHtml = templateHtml;
    }

    getViewStrategy(){  
        return new InlineViewStrategy(this.templateHtml);
    }
}