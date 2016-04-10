using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TransactRules.UI.Web.Models;
using TransactRules.Core.Utilities;

namespace TransactRules.UI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
     
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if(HttpContext.Current.Request.CurrentExecutionFilePathExtension == "")
            {
                NavigationModel.Current = CreateNavigationModel();
                SessionState.Current.UnitOfWork = new TransactRules.Core.NHibernateDriver.UnitOfWork(); 
            }

        }

         private NavigationModel CreateNavigationModel()
        {
            var model = new NavigationModel
            {
                NavigationMenus = new List<NavigationMenu>() { 
                    new NavigationMenu { Title ="Configuration", Color = ThemeColor.color_3, ImageUrl = "~/Content/img/menu_icons/forms.png", IsActive = true,
                     NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "Account Types", Url = "AccountType" },
                                             new NavigationLink { Title = "Calculations", Url = "Calculation" },
                                            }
                    },

                    new NavigationMenu { Title ="Runtime", Color = ThemeColor.color_13, ImageUrl = "~/Content/img/menu_icons/dashboard.png",
                                         NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "Calendars", Url = "Calendar" },
                                             new NavigationLink { Title = "Accounts", Url = "Account" }
                                            }
                                        },
                }
            };

            return model;
         }

        public static NavigationModel CreateTestNavigationModel()
        {
            var model = new NavigationModel
            {
                NavigationMenus = new List<NavigationMenu>() { 
                    new NavigationMenu { Title ="Dashboard", Color = ThemeColor.color_4, ImageUrl = "~/Content/img/menu_icons/dashboard.png", IsActive = true, LinkUrl = "index.html"},
                    new NavigationMenu { Title ="Form Elements", Color = ThemeColor.color_7, ImageUrl = "~/Content/img/menu_icons/forms.png", LinkUrl = "#collapse1",
                                         NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "General", Url = "forms_general.html" },
                                             new NavigationLink { Title = "Wizards", Url = "forms_wizard.html" },
                                             new NavigationLink { Title = "Validation", Url = "forms_validation.html" },
                                             new NavigationLink { Title = "Editor", Url = "forms_editor.html" }
                                            }
                                        },
                    new NavigationMenu { Title ="UI Widgets", Color = ThemeColor.color_3, ImageUrl = "~/Content/img/menu_icons/widgets.png", LinkUrl = "#collapse2",
                                         NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "Buttons", Url = "ui_buttons.html" },
                                             new NavigationLink { Title = "Dialogs", Url = "ui_dialogs.html" },
                                             new NavigationLink { Title = "Icons", Url = "ui_icons.html" },
                                             new NavigationLink { Title = "Tabs", Url = "ui_tabs.html" },
                                             new NavigationLink { Title = "Accordion", Url = "ui_accordion.html" }
                                            }
                                        },
                    new NavigationMenu { Title ="Calendar", Color = ThemeColor.color_13, ImageUrl = "~/Content/img/menu_icons/calendar.png", LinkUrl = "calendar2.html"},
                    new NavigationMenu { Title ="Maps", Color = ThemeColor.color_10, ImageUrl = "~/Content/img/menu_icons/maps.png", LinkUrl = "maps.html"},
                    new NavigationMenu { Title ="Tables", Color = ThemeColor.color_12, ImageUrl = "~/Content/img/menu_icons/tables.png", LinkUrl = "#collapse3",
                                         NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "Static", Url = "tables_static.html" },
                                             new NavigationLink { Title = "Dynamic", Url = "tables_dynamic.html" }
                                            }
                                        },
                    new NavigationMenu { Title ="Charts", Color = ThemeColor.color_19, ImageUrl = "~/Content/img/menu_icons/statistics.png", LinkUrl = "#collapse4",
                                         NavigationLinks = new List<NavigationLink>() {
                                             new NavigationLink { Title = "Statistics Elements", Url = "statistics.html" },
                                             new NavigationLink { Title = "Charts", Url = "charts.html" }
                                            }
                                        },
                    new NavigationMenu { Title ="Grid", Color = ThemeColor.color_24, ImageUrl = "~/Content/img/menu_icons/grid.png",  LinkUrl = "grid.html"},
                    new NavigationMenu { Title ="Media", Color = ThemeColor.color_8, ImageUrl = "~/Content/img/menu_icons/gallery.png",  LinkUrl = "media.html"},
                    new NavigationMenu { Title ="File Explorer", Color = ThemeColor.color_4, ImageUrl = "~/Content/img/menu_icons/explorer.png",  LinkUrl = "file_explorer.html"},
                    new NavigationMenu { Title ="Specific Pages", Color = ThemeColor.color_25, ImageUrl = "~/Content/img/menu_icons/others.png", LinkUrl = "#collapse5",
                        NavigationLinks = new List<NavigationLink>() {
                            new NavigationLink { Title = "Profile", Url = "profile.html" },
                            new NavigationLink { Title = "Search", Url = "search.html" },
                            new NavigationLink { Title = "Login", Url = "index2.html" },
                            new NavigationLink { Title = "404 Error", Url = "404.html" },
                            new NavigationLink { Title = "Blog", Url = "blog.html" }
                        }
                    },
                }
            };

            return model;
        }
    }
}