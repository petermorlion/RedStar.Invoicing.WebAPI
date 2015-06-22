System.register(['babel-runtime/helpers/create-class', 'babel-runtime/helpers/class-call-check', 'aurelia-framework'], function (_export) {
    var _createClass, _classCallCheck, ConventionalViewStrategy, App;

    return {
        setters: [function (_babelRuntimeHelpersCreateClass) {
            _createClass = _babelRuntimeHelpersCreateClass['default'];
        }, function (_babelRuntimeHelpersClassCallCheck) {
            _classCallCheck = _babelRuntimeHelpersClassCallCheck['default'];
        }, function (_aureliaFramework) {
            ConventionalViewStrategy = _aureliaFramework.ConventionalViewStrategy;
        }],
        execute: function () {
            'use strict';

            ConventionalViewStrategy.convertModuleIdToViewUrl = function (moduleId) {
                console.log(moduleId);
                if (moduleId === 'modules/invoice-generator') {
                    console.log('Returning InvoiceGenerator');
                    return 'InvoiceGenerator';
                }

                console.log('Returning ' + moduleId + '.html');
                return moduleId + '.html';
            };

            App = (function () {
                function App() {
                    _classCallCheck(this, App);
                }

                _createClass(App, [{
                    key: 'configureRouter',
                    value: function configureRouter(config, router) {

                        router.configure(function (config) {
                            config.title = 'Redstar Invoicing';
                            config.map([{ route: ['', 'invoice-generator'], moduleId: './modules/invoice-generator', nav: true, title: 'Invoice Generator' }, { route: ['invoice-settings'], moduleId: './modules/invoice-settings', nav: true, title: 'Invoice Settings' }]);
                        });

                        this.router = router;
                    }
                }]);

                return App;
            })();

            _export('App', App);
        }
    };
});