using System;
using Newtonsoft.Json;
using Vidyano.Service.Repository;

namespace RheaWebsiteLatest.Service.Actions
{
    public class PortalNavbarActions : DefaultPersistentObjectActions<RheaWebsiteLatestContext, object>
    {
        public PortalNavbarActions(RheaWebsiteLatestContext context) : base(context)
        {
        }

        public override void OnConstruct(PersistentObject obj)
        {
            base.OnConstruct(obj);
        }

        public override void OnLoad(PersistentObject obj, PersistentObject? parent)
        {
            base.OnLoad(obj, parent);
            List<Menu> menus = new();
            var activePortalPages = Context.PortalPages.Where(it => it.Active == true).OrderBy(it => it.Order);
            foreach (var portalPage in activePortalPages)
            {
                menus.Add(new Menu { Name = portalPage.Name, Path = $"portalpage/{portalPage.Id}" });
            }
            obj.SetAttributeValue("Items", JsonConvert.SerializeObject(menus));
        }
    }

    class Menu
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
