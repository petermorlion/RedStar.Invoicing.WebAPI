export class InvoiceGenerator {
    items = [];

    addInvoiceItem() {
        this.items.push(new InvoiceItem());
        console.log('added item');
    }
}

export class InvoiceItem {
    amount = 1;
    description = '';
    unitPrice = 1;

    getTotal() {
        return this.unitPrice * this.amount;
    }
}