System.register(['babel-runtime/helpers/define-decorated-property-descriptor', 'babel-runtime/helpers/create-decorated-class', 'babel-runtime/helpers/class-call-check', 'codemirror', 'aurelia-framework'], function (_export) {
    var _defineDecoratedPropertyDescriptor, _createDecoratedClass, _classCallCheck, fromTextArea, customElement, bindable, HtmlEditor;

    return {
        setters: [function (_babelRuntimeHelpersDefineDecoratedPropertyDescriptor) {
            _defineDecoratedPropertyDescriptor = _babelRuntimeHelpersDefineDecoratedPropertyDescriptor['default'];
        }, function (_babelRuntimeHelpersCreateDecoratedClass) {
            _createDecoratedClass = _babelRuntimeHelpersCreateDecoratedClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_codemirror) {
            fromTextArea = _codemirror.fromTextArea;
        }, function (_aureliaFramework) {
            customElement = _aureliaFramework.customElement;
            bindable = _aureliaFramework.bindable;
        }],
        execute: function () {
            'use strict';

            HtmlEditor = (function () {
                var _instanceInitializers = {};

                function HtmlEditor() {
                    _classCallCheck(this, _HtmlEditor);

                    _defineDecoratedPropertyDescriptor(this, 'html', _instanceInitializers);
                }

                _createDecoratedClass(HtmlEditor, [{
                    key: 'attached',
                    value: function attached() {
                        var _this = this;

                        var textArea = document.getElementById('invoice-template');
                        var options = {
                            mode: 'htmlmixed',
                            lineNumbers: true,
                            value: this.html
                        };

                        var codeMirror = fromTextArea(textArea, options);

                        var vm = this;

                        codeMirror.on('change', function (instance, changeObj) {
                            _this.html = instance.doc.getValue();
                            console.log('viewmodel updated');
                        });

                        this.editor = codeMirror;
                    }
                }, {
                    key: 'htmlChanged',
                    value: function htmlChanged() {
                        console.log('value changed');
                        if (this.editor && !this.updatedFromAjaxCall) {
                            this.editor.doc.setValue(this.html);
                            this.updatedFromAjaxCall = true;
                        }
                    }
                }, {
                    key: 'html',
                    decorators: [bindable],
                    initializer: null,
                    enumerable: true
                }], null, _instanceInitializers);

                var _HtmlEditor = HtmlEditor;
                HtmlEditor = customElement('html-editor')(HtmlEditor) || HtmlEditor;
                return HtmlEditor;
            })();

            _export('HtmlEditor', HtmlEditor);
        }
    };
});