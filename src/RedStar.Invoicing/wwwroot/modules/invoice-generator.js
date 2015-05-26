import {computedFrom} from 'aurelia-framework';

export class InvoiceGenerator {
    items = [];

    addInvoiceItem() {
        this.items.push(new InvoiceItem());
    }

    get subtotal() {
        let sum = 0;
        this.items.map(i => sum += i.total);
        return sum;
    }

    get vat() {
        return 0.21 * this.subtotal;
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