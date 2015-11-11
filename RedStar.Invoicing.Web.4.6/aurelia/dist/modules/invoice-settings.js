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
				function InvoiceSettings(http) {
					_classCallCheck(this, _InvoiceSettings);

					this.http = http;
				}

				_createClass(InvoiceSettings, [{
					key: 'submit',
					value: function submit() {
						var _this = this;

						//TODO: separate class
						var reader = new FileReader();
						reader.onload = function () {
							var file = reader.result;

							var settingsDTO = {
								logo: file,
								logoName: _this.files[0].name,
								invoiceTemplate: _this.invoiceTemplate
							};

							_this.http.createRequest('http://' + window.location.host + '/api/settings').asPost().withHeader('Content-Type', 'application/json; charset=utf-8').withContent(settingsDTO).send().then(function (response) {
								console.log(response.response);
							})['catch'](function (err) {
								console.log(err);
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