import {fromTextArea} from 'codemirror';
import {customElement, bindable} from 'aurelia-framework';

@customElement('html-editor')
export class HtmlEditor {
  @bindable html;

    attached() {
        var textArea = document.getElementById('invoice-template');
        var options = {
            mode: 'htmlmixed',
            lineNumbers: true,
            value: this.html
        };

        var codeMirror = fromTextArea(textArea, options);

        codeMirror.on('change', (instance, changeObj) => {
            this.html = instance.doc.getValue();
        });

        this.editor = codeMirror;
    }

    htmlChanged() {
        if (this.editor && !this.updatedFromAjaxCall)
        {
            this.editor.doc.setValue(this.html);
            this.updatedFromAjaxCall = true;
        }
    }
}