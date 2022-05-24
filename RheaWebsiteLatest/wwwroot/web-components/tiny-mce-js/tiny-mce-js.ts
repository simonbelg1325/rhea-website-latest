//import * as tinymce from 'tinymce';
import { WebComponent, Vidyano, Polymer } from "../../vidyano.js";

//@WebComponent.register()




declare var tinymce: any;
//namespace Vidyano.WebComponents {
//    @Vidyano.WebComponents.Attributes.PersistentObjectAttribute.register({
//        properties: {
//            editor: {
//                type: Object,
//                readOnly: true
//            },
//            htmlValue: {
//                type: String,
//                readOnly: true
//            }
//        },
//        observers: ["_changeEditingMode(editing)"],
//    }, "vi")
//    export class TinyMceJs extends Vidyano.WebComponents.Attributes.PersistentObjectAttribute {
//        private _textChangeListener: Function;
//        private _updating: boolean;
//        readonly htmlValue: string; private _setHtmlValue: (htmlValue: string) => void;
//        readonly editor: any; private _setEditor: (editor: any) => void;

//        async attached() {
//            super.attached();
//        }

//        private _changeEditingMode(editing) {
//            if (!!this.editor) {
//                if (editing) {
//                    this.editor[0].mode.set("design");
//                    this.$$(".tox-editor-header").hidden = false;
//                } else {
//                    this.editor[0].mode.set("readonly");
//                    this.$$(".tox-editor-header").hidden = true;
//                }
//            }
//        }

//        protected detached() {
//            if (this._textChangeListener) {
//                this.editor[0].off("Change", this._textChangeListener);
//                this._textChangeListener = null;
//            }
//            if (this.editor) {
//                this.editor[0].destroy();
//                this._setEditor(undefined);
//            }

//            super.detached();
//        }

//        private _textChange() {
//            try {
//                this._updating = true;
//                this.attribute?.setValue(this.editor[0]?.getContent());
//            }
//            finally {
//                this._updating = false;
//            }
//        }

//        protected async _valueChanged(value: string) {
//            if (this._updating) {
//                this._setHtmlValue((<any>this.editor[0])?.getContent());
//                return;
//            }
//            if (!this.editor) {
//                for (var i = tinymce.editors.length - 1; i > -1; i--) {
//                    var ed_id = tinymce.editors[i].id;
//                    tinymce.execCommand("mceRemoveEditor", true, ed_id);
//                }
//                this._setEditor(await tinymce.init({
//                    target: this.$.editor,
//                    height: "100%",
//                    menubar: true,
//                    plugins: [
//                        'advlist autolink lists link image charmap print preview anchor',
//                        'searchreplace visualblocks',
//                        'insertdatetime media table paste wordcount table'
//                    ],
//                    toolbar: 'undo redo | formatselect | ' +
//                        'bold italic backcolor | alignleft aligncenter ' +
//                        'alignright alignjustify | bullist numlist outdent indent | ' +
//                        'removeformat | help | table | code',
//                    content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
//                    paste_data_images: true,
//                    paste_as_text: true,
//                    branding: false,
//                    removed_menuitems: 'newdocument'
//                })
//                );
//                this.editor[0].on("Change", this._textChangeListener = this._textChange.bind(this));
//            }
//            if (!!this.editor) {
//                if (!value)
//                    this.editor[0]?.setContent("");
//                else {
//                    this.editor[0].setContent(this.attribute.value);
//                }
//                this._setHtmlValue((<any>this.editor[0])?.getContent());
//            }
//            if (!!this.editor) {
//                this._changeEditingMode(this.editing);
//                debugger;
//            }
//        }
//    }


//}