System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'babel-runtime/helpers/create-decorated-class', 'aurelia-framework', 'aurelia-http-client'], function (_export) {
    var _createClass, _classCallCheck, _createDecoratedClass, inject, computedFrom, HttpClient, InvoiceGenerator, InvoiceItem;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_babelRuntimeHelpersCreateDecoratedClass) {
            _createDecoratedClass = _babelRuntimeHelpersCreateDecoratedClass['default'];
        }, function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
            computedFrom = _aureliaFramework.computedFrom;
        }, function (_aureliaHttpClient) {
            HttpClient = _aureliaHttpClient.HttpClient;
        }],
        execute: function () {
            'use strict';

            InvoiceGenerator = (function () {
                function InvoiceGenerator(http) {
                    _classCallCheck(this, _InvoiceGenerator);

                    this.items = [];
                    this.invoiceTemplate = '';

                    this.http = http;
                }

                var _InvoiceGenerator = InvoiceGenerator;

                _createClass(_InvoiceGenerator, [{
                    key: 'activate',
                    value: function activate() {
                        var that = this;
                        this.http
                        // TODO: use /api/
                        .get('http://' + window.location.host + '/invoicegenerator/logourl').then(function (response) {
                            that.logoUrl = response.content;
                        })['catch'](function (err) {
                            console.log(err);
                        });
                    }
                }, {
                    key: 'addInvoiceItem',
                    value: function addInvoiceItem() {
                        this.items.push(new InvoiceItem());
                    }
                }, {
                    key: 'print',
                    value: function print() {
                        //TODO: get template from server and then use property instead of DOM
                        var html = document.getElementById('invoice-template').innerHTML;
                        var json = { html: html, invoiceNumber: this.invoiceNumber };

                        this.http.createRequest('http://' + window.location.host + '/api/invoice').asPost().withHeader('Content-Type', 'application/json; charset=utf-8').withContent(json).send().then(function (response) {
                            console.log(response.response);
                        })['catch'](function (err) {
                            console.log(err);
                        });
                    }
                }, {
                    key: 'subtotal',
                    get: function () {
                        var sum = 0;
                        this.items.map(function (i) {
                            return sum += i.total;
                        });
                        return sum;
                    }
                }, {
                    key: 'vat',
                    get: function () {
                        return 0.21 * this.subtotal;
                    }
                }, {
                    key: 'total',
                    get: function () {
                        return this.subtotal + this.vat;
                    }
                }]);

                InvoiceGenerator = inject(HttpClient)(InvoiceGenerator) || InvoiceGenerator;
                return InvoiceGenerator;
            })();

            _export('InvoiceGenerator', InvoiceGenerator);

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
                    get: function () {
                        return this.unitPrice * this.amount;
                    }
                }]);

                return InvoiceItem;
            })();

            _export('InvoiceItem', InvoiceItem);
        }
    };
});