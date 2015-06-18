import {inject} from 'aurelia-framework';
/*import {Router} from 'aurelia-router';*/
import {ConventionalViewStrategy} from 'aurelia-framework';

ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
    if (moduleId === 'modules/invoice-generator') {
        return 'InvoiceGenerator';
    }
    
    return moduleId.replace('view-models', 'views') + '.html';
};

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
/*@inject(Router)
export class App {
    constructor(router) {
        this.router = router;
        
        ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
            if (moduleId === 'modules/invoice-generator') {
                return 'InvoiceGenerator';
            }
            
            return moduleId.replace('view-models', 'views') + '.html';
        };
        
        this.router.configure(config => {
            config.title = 'Redstar Invoicing';
            config.map([
              { route: ['','invoice-generator'],  moduleId: './modules/invoice-generator',      nav: true, title:'Invoice Generator' },
              { route: ['invoice-settings'],  moduleId: './modules/invoice-settings',      nav: true, title:'Invoice Settings' }
            ]);
        });
    }
}*/



/*import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import {ConventionalViewStrategy} from 'aurelia-framework';

@inject(Router)
export class App {
    
    constructor(router) {
        ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
            if (moduleId === 'modules/invoice-generator') {
                return 'InvoiceGenerator';
            }
            
            return moduleId.replace('view-models', 'views') + '.html';
        };
        
        router.configure(config => {
            config.title = 'Redstar Invoicing';
            config.map([
              { route: ['','invoice-generator'],  moduleId: './modules/invoice-generator',      nav: true, title:'Invoice Generator' },
              { route: ['invoice-settings'],  moduleId: './modules/invoice-settings',      nav: true, title:'Invoice Settings' }
            ]); 
        });

        this.router = router;
    }
}*/