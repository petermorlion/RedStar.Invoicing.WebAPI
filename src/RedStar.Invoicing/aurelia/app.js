import {ConventionalViewStrategy} from 'aurelia-framework';

ConventionalViewStrategy.convertModuleIdToViewUrl = function(moduleId){
    console.log(moduleId);
            if (moduleId === 'modules/invoice-generator') {
                 console.log('Returning InvoiceGenerator');
                 return 'InvoiceGenerator';
            }
             
            console.log('Returning ' + moduleId + '.html');
            return moduleId + '.html';
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