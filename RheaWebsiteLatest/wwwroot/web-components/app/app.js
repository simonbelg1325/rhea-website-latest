import { __decorate } from "tslib";
import { AppBase, AppServiceHooksBase, Polymer, WebComponent } from "../../vidyano.js";
import "../wrapper/wrapper.js";
import "../portal-page/portal-page.js";
import "../tiny-mce-js/tiny-mce-js.js";
class AppServiceHooks extends AppServiceHooksBase {
    onRedirectToSignIn(keepUrl) {
        if (keepUrl && window.app.path.startsWith("sign-in/")) {
            window.app.changePath(window.app.path);
            return;
        }
        window.app.changePath("sign-in" + (keepUrl && window.app.path ? "/" + encodeURIComponent(App.removeRootPath(window.app.path).replace(/sign-in\/?/, "")).replace(/\./g, "%2E") : ""), true);
    }
    onRedirectToSignOut(keepUrl) {
        window.app.changePath("sign-out" + (keepUrl && window.app.path ? "/" + encodeURIComponent(App.removeRootPath(decodeURIComponent(this.app.path)).replace(/sign-in\/?/, "")).replace(/\./g, "%2E") : ""), true);
    }
}
let App = class App extends AppBase {
    static get template() {
        const baseTemplate = AppBase.template;
        baseTemplate.content.appendChild(Polymer.html `<link rel="import" href="app.html">`.content);
        return baseTemplate;
    }
    constructor() {
        super(new AppServiceHooks());
    }
};
App = __decorate([
    WebComponent.register({}, "rhea")
], App);
