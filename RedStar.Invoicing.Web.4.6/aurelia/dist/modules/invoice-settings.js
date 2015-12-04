System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'aurelia-framework', 'aurelia-http-client'], function (_export) {
    var _createClass, _classCallCheck, inject, HttpClient, InvoiceSettings;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_aureliaFramework) {
            inject = _aureliaFramework.inject;
        }, function (_aureliaHttpClient) {
            HttpClient = _aureliaHttpClient.HttpClient;
        }],
        execute: function () {
            'use strict';

            InvoiceSettings = (function () {
                function InvoiceSettings(http, toastr) {
                    _classCallCheck(this, _InvoiceSettings);

                    this.http = http;
                    this.status = 'idle';
                }

                _createClass(InvoiceSettings, [{
                    key: 'activate',
                    value: function activate() {
                        var _this = this;

                        this.http
                        // TODO: use /api/
                        .get('http://' + window.location.host + '/api/settings').then(function (response) {
                            //this.logoUrl = response.content.LogoUrl;
                            _this.invoiceTemplate = response.content.InvoiceTemplate;
                        })['catch'](function (err) {
                            console.log(err);
                        });
                    }
                }, {
                    key: 'finishAjaxCall',
                    value: function finishAjaxCall() {
                        this.status = 'done';
                        var vm = this;
                        setTimeout(function () {
                            vm.status = 'idle';
                        }, 1500);
                    }
                }, {
                    key: 'submit',
                    value: function submit() {
                        var _this2 = this;

                        this.status = 'busy';

                        //TODO: separate class
                        var reader = new FileReader();
                        reader.onload = function () {
                            var file = reader.result;

                            var settingsDTO = {
                                logo: file,
                                logoName: _this2.files[0].name,
                                invoiceTemplate: _this2.invoiceTemplate
                            };

                            _this2.http.createRequest('http://' + window.location.host + '/api/settings').asPost().withHeader('Content-Type', 'application/json; charset=utf-8').withContent(settingsDTO).send().then(function (response) {
                                console.log(response.response);
                                _this2.finishAjaxCall();
                            })['catch'](function (err) {
                                // TODO: error handling
                                console.log(err);
                                _this2.finishAjaxCall();
                            });
                        };

                        reader.readAsDataURL(this.files[0]);
                    }
                }]);

                var _InvoiceSettings = InvoiceSettings;
                InvoiceSettings = inject(HttpClient)(InvoiceSettings) || InvoiceSettings;
                return InvoiceSettings;
            })();

            _export('InvoiceSettings', InvoiceSettings);
        }
    };
});