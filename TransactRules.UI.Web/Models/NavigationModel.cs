using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactRules.UI.Web.Models
{
    public class NavigationModel
    {
        private const string NavigationModelKey = "_navigationModel";

        public NavigationModel()
        {
            NavigationMenus = new List<NavigationMenu>();
        }

        public List<NavigationMenu> NavigationMenus { get; set; }

        public static NavigationModel Current
        {
            get
            {
                return (NavigationModel) HttpContext.Current.Items[NavigationModelKey];
            }
            set 
            {
                HttpContext.Current.Items[NavigationModelKey] = value;
            }
        }
    }
}