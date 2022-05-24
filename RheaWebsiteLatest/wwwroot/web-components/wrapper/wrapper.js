import { __decorate } from "tslib";
import { App, Polymer, WebComponent } from "../../vidyano.js";
let Wrapper = class Wrapper extends WebComponent {
    static get template() {
        return Polymer.html `<link rel="import" href="wrapper.html">`;
    }
    async connectedCallback() {
        super.connectedCallback();
        await this.app.initialize;
        this._setActiveMenu(this._getActiveMenu());
    }
    async _signedInChanged() {
        const isSignedIn = this.service.isSignedIn;
        if (isSignedIn == undefined || !isSignedIn)
            return;
        this._setPo(await this.app.service.getPersistentObject(null, "PortalNavbar"));
        const items = JSON.parse(this.po.getAttributeValue("Items"));
        this._setItems(items);
        this._setActiveMenu(this._getActiveMenu());
        this.app.changePath(this._getCurrentPathLocation());
    }
    _getCurrentPathLocation() {
        let path = document.location.pathname.trimStart("/");
        console.log(path);
        if (path === "" || (!path.contains("portalpage"))) {
            return "portalpage/1";
        }
        else {
            return path;
        }
    }
    _getActiveMenu() {
        const route = this.parentNode;
        const currentPath = App.removeRootPath(route.path);
        const newPath = App.removeRootPath(this.app.path);
        let path = this._getCurrentPathLocation();
        if (path.contains("portalpage")) {
            return "portalpage/1";
        }
        return path;
    }
    _hasItems(items) {
        return items != null && items.length > 0;
    }
    _isMenuActive(menu, activeMenu) {
        return menu == activeMenu;
    }
    _signOut(e) {
        this.app.redirectToSignOut(false);
    }
    _navigate(e) {
        const newRoute = e.model.item.Path;
        if (newRoute != null) {
            this.app.changePath(newRoute);
            this.shadowRoot.querySelector(".sidebar-container").classList.toggle("open");
            this.shadowRoot.querySelector(".nav-icon").classList.toggle("open");
        }
        if (newRoute === "portalpage/7") {
            window.location.href = "https://helpdesk.rhea.be";
        }
    }
    _isRoute(activeRoute, where) {
        if (activeRoute === "" && where === "portalpage(/:objectId*)")
            return true;
        else if (activeRoute === where)
            return true;
    }
    _activate(e) {
        debugger;
        console.log(e.target);
        const route = this.parentNode;
        const currentPath = App.removeRootPath(route.path);
        const newPath = App.removeRootPath(this.app.path);
        if (newPath.startsWith("portalpage")) {
            this._setActiveRoute(newPath);
        }
    }
    _toggle(e) {
        this.shadowRoot.querySelector(".nav-icon").classList.toggle("open");
        this.shadowRoot.querySelector(".sidebar-container").classList.toggle("open");
    }
};
Wrapper = __decorate([
    WebComponent.register({
        properties: {
            po: {
                type: Object,
                readOnly: true
            },
            items: {
                type: Array,
                readOnly: true
            },
            activeMenu: {
                type: String,
                readOnly: true
            },
            activeRoute: {
                type: String,
                readOnly: true
            },
            hasItems: {
                type: Boolean,
                computed: "_hasItems(items)"
            }
        },
        forwardObservers: [
            "_signedInChanged(service.isSignedIn)",
        ],
        listeners: {
            "app-route-activate": "_activate"
        },
        observers: [
            "_menuSelection()"
        ]
    }, "rhea")
], Wrapper);
export { Wrapper };
