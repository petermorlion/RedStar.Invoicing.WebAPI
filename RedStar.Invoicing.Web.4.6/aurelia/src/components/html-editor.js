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

      var vm = this;

      codeMirror.on('change', function(instance, changeObj) {
          // TODO: use arrow function?
          vm.html = instance.doc.getValue();
          console.log('viewmodel updated');
      });

      this.editor = codeMirror;
  }

  htmlChanged() {
      console.log('value changed');
      if (this.editor && !this.updatedFromAjaxCall)
      {
          this.editor.doc.setValue(this.html);
          this.updatedFromAjaxCall = true;
      }
  }
}