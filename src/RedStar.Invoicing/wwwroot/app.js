import {ConventionalViewStrategy} from 'aurelia-framework';

ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
    if (moduleId === 'modules/invoice-generator') {
        return 'InvoiceGenerator';
    }
    
    return moduleId.replace('view-models', 'views') + '.html';
}

export class App {
    configureRouter(config, router){
        config.title = 'Redstar Invoicing';
        config.map([
          { route: ['','invoice-generator'],  moduleId: './modules/invoice-generator',      nav: true, title:'Invoice Generator' },
          { route: ['invoice-settings'],  moduleId: './modules/invoice-settings',      nav: true, title:'Invoice Settings' }
        ]);

        this.router = router;
    }
}