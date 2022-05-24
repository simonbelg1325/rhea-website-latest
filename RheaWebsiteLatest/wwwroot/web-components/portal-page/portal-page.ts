import { WebComponent, Vidyano, Polymer } from "../../vidyano.js";


interface IShowDocumentRouteParameters {
    objectId: string;
}



@WebComponent.register({
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

export class PortalPage extends WebComponent {
    static get template() {
        return Polymer.html`<link rel="import href="portal-page.html">`
    }

    po: Vidyano.PersistentObject; private _setPo: (value: Vidyano.PersistentObject) => void;
    query: Vidyano.Query[]; private _setQuery: (value: Vidyano.Query[]) => void;
    isBusy: boolean; private _setIsBusy: (isBusy: boolean) => void;
    readonly items: Array<any>; private _setItems: (value: Array<any>) => void;

    async connectedCallback() {
        super.connectedCallback();
        await this.app.initialize;
    }

    private async _activate(e: CustomEvent) {
        const { parameters }: { parameters: IShowDocumentRouteParameters; } = e.detail;
        this._setPo(await this.app.service.getPersistentObject(null, "PortalPage", parameters.objectId, false));
        this._setQuery(this.po.queries);
        const po = await this.app.service.getPersistentObject(null, "PortalNavbar");
        const items = JSON.parse(po.getAttributeValue("Items"));
        this._setItems(items);
        this._setIsBusy(false);

        console.log(this.query)
    }

    private _checkName(po: Vidyano.PersistentObject, name: string) {
        const poName = po.getAttributeValue("Name");
        if (poName === name) {
            return true;
        }
        return false;
    }

    private _isRoute(activeRoute: string, where: string) {
        if (activeRoute === "" && where === "portalpage(/:objectId*)")
            return true;
        else if (activeRoute === where)
            return true;

    }

    private _navigate(e: Polymer.Gestures.TapEvent) {

        const newRoute = (e.target as any).getAttribute("path");
        if (newRoute != null) {
            this.app.changePath(newRoute);
        }

        if (newRoute === "portalpage/7") {
            window.location.href = "https://helpdesk.rhea.be";
        }
    }
}