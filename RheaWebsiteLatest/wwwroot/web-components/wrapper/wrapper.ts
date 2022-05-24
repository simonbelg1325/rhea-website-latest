import { App, AppRoute, Polymer, Vidyano, WebComponent } from "../../vidyano.js";

@WebComponent.register({
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

export class Wrapper extends WebComponent {
    static get template() {
        return Polymer.html`<link rel="import" href="wrapper.html">`
    }

    readonly activeMenu: String; private _setActiveMenu: (value: String) => void;
    readonly activeRoute: String; private _setActiveRoute: (value: String) => void;
    readonly po: Vidyano.PersistentObject; private _setPo: (value: Vidyano.PersistentObject) => void;
    readonly items: Array<any>; private _setItems: (value: Array<any>) => void;

    async connectedCallback() {
        super.connectedCallback();
        await this.app.initialize;
        this._setActiveMenu(this._getActiveMenu());
    }

    private async _signedInChanged() {
        const isSignedIn = this.service.isSignedIn;

        if (isSignedIn == undefined || !isSignedIn) return;
        this._setPo(await this.app.service.getPersistentObject(null, "PortalNavbar"));
        const items = JSON.parse(this.po.getAttributeValue("Items"));
        this._setItems(items);

        this._setActiveMenu(this._getActiveMenu());
        this.app.changePath(this._getCurrentPathLocation());
    }

    private _getCurrentPathLocation() {
        let path = document.location.pathname.trimStart("/");
        console.log(path);
        if (path === "" || (!path.contains("portalpage"))) {
            return "portalpage/1";
        } else {
            return path;
        }
    }

    private _getActiveMenu() {
        const route = <AppRoute>this.parentNode;
        const currentPath = App.removeRootPath(route.path);
        const newPath = App.removeRootPath(this.app.path);
        let path = this._getCurrentPathLocation();
        if (path.contains("portalpage")) {
            return "portalpage/1";
        }
        return path;
    }

    private _hasItems(items: any) {
        return items != null && items.length > 0;
    }

    private _isMenuActive(menu: string, activeMenu: string) {
        return menu == activeMenu;
    }

    private _signOut(e: Polymer.Gestures.TapEvent) {
        this.app.redirectToSignOut(false);
    }

    private _navigate(e: Polymer.Gestures.TapEvent) {
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

    private _isRoute(activeRoute: string, where: string) {
        if (activeRoute === "" && where === "portalpage(/:objectId*)")
            return true;
        else if (activeRoute === where)
            return true;

    }

    private _activate(e: CustomEvent) {
        debugger;
        console.log(e.target);
        const route = <AppRoute>this.parentNode;
        const currentPath = App.removeRootPath(route.path);
        const newPath = App.removeRootPath(this.app.path);

        if (newPath.startsWith("portalpage")) {
            this._setActiveRoute(newPath);
        }
    }

    private _toggle(e: Polymer.Gestures.TapEvent) {
        this.shadowRoot.querySelector(".nav-icon").classList.toggle("open");
        this.shadowRoot.querySelector(".sidebar-container").classList.toggle("open");
    }
}