using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactRules.UI.Web.Models
{
    public class NavigationMenu
    {
        public NavigationMenu()
        {
            NavigationLinks = new List<NavigationLink>();
            IsCollapsed = true;
        }

        public string Title { get; set; }
        public string LinkUrl { get; set; }
        public string ImageUrl { get; set; }
        public ThemeColor Color { get; set; }
        public bool IsActive { get; set; }
        public bool IsCollapsed { get; set; }
        public List<NavigationLink> NavigationLinks { get; set; }
    }
}