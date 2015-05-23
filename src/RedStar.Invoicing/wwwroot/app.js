export class App {
    configureRouter(config, router){
        config.title = 'Redstar Invoicing';
        config.map([
          { route: ['','invoice-generator'],  moduleId: './modules/invoice-generator',      nav: true, title:'Invoice Generator' }
        ]);

        this.router = router;
    }
}