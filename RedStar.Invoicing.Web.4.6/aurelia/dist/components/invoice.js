System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'babel-runtime/helpers/create-decorated-class', 'aurelia-framework'], function (_export) {
    var _createClass, _classCallCheck, _createDecoratedClass, computedFrom, Invoice, InvoiceItem;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_babelRuntimeHelpersCreateDecoratedClass) {
            _createDecoratedClass = _babelRuntimeHelpersCreateDecoratedClass['default'];
        }, function (_aureliaFramework) {
            computedFrom = _aureliaFramework.computedFrom;
        }],
        execute: function () {
            'use strict';

            Invoice = (function () {
                function Invoice() {
                    _classCallCheck(this, Invoice);

                    this.items = [];
                }

                _createClass(Invoice, [{
                    key: 'addInvoiceItem',
                    value: function addInvoiceItem() {
                        this.items.push(new InvoiceItem());
                    }
                }, {
                    key: 'subtotal',
                    get: function get() {
                        var sum = 0;
                        this.items.map(function (i) {
                            return sum += i.total;
                        });
                        return sum;
                    }
                }, {
                    key: 'vat',
                    get: function get() {
                        return 0.21 * this.subtotal;
                    }
                }, {
                    key: 'total',
                    get: function get() {
                        return this.subtotal + this.vat;
                    }
                }]);

                return Invoice;
            })();

            _export('Invoice', Invoice);

            InvoiceItem = (function () {
                function InvoiceItem() {
                    _classCallCheck(this, InvoiceItem);

                    this.amount = 1;
                    this.description = '';
                    this.unitPrice = 1;
                }

                _createDecoratedClass(InvoiceItem, [{
                    key: 'total',
                    decorators: [computedFrom('unitPrice', 'amount')],
                    get: function get() {
                        return this.unitPrice * this.amount;
                    }
                }]);

                return InvoiceItem;
            })();

            _export('InvoiceItem', InvoiceItem);
        }
    };
});