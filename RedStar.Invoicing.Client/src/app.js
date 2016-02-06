import {ConventionalViewStrategy} from 'aurelia-framework';

ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
    console.log('Requested module with id "' + moduleId + '".');
    var id = (moduleId.endsWith('.js') || moduleId.endsWith('.ts')) ? moduleId.substring(0, moduleId.length - 3) : moduleId;
    if (id === 'modules/invoice-generator') {
        return 'InvoiceGenerator';
    }

    return id + '.html';
}

        
export class App {
    configureRouter(config, router) {
        
        router.configure(config => {
            config.title = 'Redstar Invoicing';
            config.map([
              { route: ['','invoice-generator'],  moduleId: './modules/invoice-generator',      nav: true, title:'Invoice Generator' },
              { route: ['invoice-settings'],  moduleId: './modules/invoice-settings',      nav: true, title:'Invoice Settings' }
            ]);
        });
        
        this.router = router;
    }
}