System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'aurelia-framework', 'aurelia-http-client', '../components/invoice-template.js', '../components/invoice.js'], function (_export) {
    var _createClass, _classCallCheck, inject, HttpClient, InvoiceTemplate, Invoice, InvoiceGenerator;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_aureliaHttpClient) {
            HttpClient = _aureliaHttpClient.HttpClient;
        }, function (_componentsInvoiceTemplateJs) {
            InvoiceTemplate = _componentsInvoiceTemplateJs.InvoiceTemplate;
        }, function (_componentsInvoiceJs) {
            Invoice = _componentsInvoiceJs.Invoice;
        }],
        execute: function () {
            'use strict';

            InvoiceGenerator = (function () {
                function InvoiceGenerator(http) {
                    _classCallCheck(this, _InvoiceGenerator);

                    this.http = http;
                }

                _createClass(InvoiceGenerator, [{
                    key: 'activate',
                    value: function activate() {
                        var _this = this;

                        this.http
                        // TODO: use /api/
                        .get('http://' + window.location.host + '/api/invoicegenerator').then(function (response) {
                            _this.logoUrl = response.content.LogoUrl;
                            //this.invoiceTemplate = response.content.InvoiceTemplate;
                            _this.invoice = new Invoice();
                            _this.invoiceTemplate = new InvoiceTemplate('<template>' + response.content.InvoiceTemplate + '</template>', _this.invoice);
                        })['catch'](function (err) {
                            console.log(err);
                        });
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
                }]);

                var _InvoiceGenerator = InvoiceGenerator;
                InvoiceGenerator = inject(HttpClient)(InvoiceGenerator) || InvoiceGenerator;
                return InvoiceGenerator;
            })();

            _export('InvoiceGenerator', InvoiceGenerator);
        }
    };
});