import {computedFrom} from 'aurelia-framework';

export class InvoiceGenerator {
    items = [];

    addInvoiceItem() {
        this.items.push(new InvoiceItem());
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