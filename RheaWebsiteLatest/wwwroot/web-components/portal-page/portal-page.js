import { __decorate } from "tslib";
import { WebComponent, Polymer } from "../../vidyano.js";
let PortalPage = class PortalPage extends WebComponent {
    static get template() {
        return Polymer.html `<link rel="import href="portal-page.html">`;
    }
    async connectedCallback() {
        super.connectedCallback();
        await this.app.initialize;
    }
    async _activate(e) {
        const { parameters } = e.detail;
        this._setPo(await this.app.service.getPersistentObject(null, "PortalPage", parameters.objectId, false));
        this._setQuery(this.po.queries);
        const po = await this.app.service.getPersistentObject(null, "PortalNavbar");
        const items = JSON.parse(po.getAttributeValue("Items"));
        this._setItems(items);
        this._setIsBusy(false);
        console.log(this.query);
    }
    _checkName(po, name) {
        const poName = po.getAttributeValue("Name");
        if (poName === name) {
            return true;
        }
        return false;
    }
    _isRoute(activeRoute, where) {
        if (activeRoute === "" && where === "portalpage(/:objectId*)")
            return true;
        else if (activeRoute === where)
            return true;
    }
    _navigate(e) {
        const newRoute = e.target.getAttribute("path");
        if (newRoute != null) {
            this.app.changePath(newRoute);
        }
        if (newRoute === "portalpage/7") {
            window.location.href = "https://helpdesk.rhea.be";
        }
    }
};
PortalPage = __decorate([
    WebComponent.register({
        properties: {
            po: {
                type: Object,
                readOnly: true
            },
            query: {
                type: Object,
                readOnly: true
            },
            items: {
                type: Array,
                readOnly: true
            },
            isBusy: {
                type: Boolean,
                readOnly: true,
                value: true
            }
        },
        listeners: {
            "app-route-activate": "_activate"
        },
        forwardObservers: ["query"]
    }, "rhea")
], PortalPage);
export { PortalPage };
