System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'aurelia-framework'], function (_export) {
    var _createClass, _classCallCheck, InlineViewStrategy, InvoiceTemplate;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_aureliaFramework) {
            InlineViewStrategy = _aureliaFramework.InlineViewStrategy;
        }],
        execute: function () {
            'use strict';

            InvoiceTemplate = (function () {
                function InvoiceTemplate(templateHtml) {
                    _classCallCheck(this, InvoiceTemplate);

                    this.templateHtml = templateHtml;
                }

                _createClass(InvoiceTemplate, [{
                    key: 'getViewStrategy',
                    value: function getViewStrategy() {
                        return new InlineViewStrategy(this.templateHtml);
                    }
                }]);

                return InvoiceTemplate;
            })();

            _export('InvoiceTemplate', InvoiceTemplate);
        }
    };
});