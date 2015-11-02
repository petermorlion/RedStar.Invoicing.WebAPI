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

					this.file = '';

					this.http = http;
				}

				_createClass(InvoiceSettings, [{
					key: 'submit',
					value: function submit() {
						//TODO: separate class
						var settingsDTO = {
							logo: this.file,
							logoName: this.fileName,
							invoiceTemplate: this.invoiceTemplate
						};

						this.http.createRequest('http://' + window.location.host + '/api/settings').asPost().withHeader('Content-Type', 'application/json; charset=utf-8').withContent(settingsDTO).send().then(function (response) {
							console.log(response.response);
						})['catch'](function (err) {
							console.log(err);
						});
					}
				}, {
					key: 'fileSelected',
					value: function fileSelected() {
						var _this = this;

						var reader = new FileReader();
						var file = this.$event.target.files[0];
						reader.readAsDataURL(file);
						this.fileName = file.name;
						reader.onload = function () {
							_this.file = reader.result;
						};
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