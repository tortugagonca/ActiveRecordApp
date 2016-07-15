using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace ActiveRecordApp.Controllers
{
    [System.Web.Http.RoutePrefix("api")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Verifica dados usuário windows";

            return View();
        }
        [System.Web.Http.HttpGet, System.Web.Http.Route("VerificaExistenciaUsuario")]
        public string VerificaExistenciaUsuario([FromBody]string username)
        {
            var de = RetornaUsuario();
            if (de == null)
            {
                return "Usuário não encontrado.";
            }
            else
            {
                return "Usuário encontrado.";
            }

        }

        private DirectoryEntry RetornaUsuario()
        {
            var userDn = string.Empty; //txtUsername.Text;
            //var de = new DirectoryEntry(fullPath);
            var de = GetDirectoryObject("db1.com.br");
            return FindUser(de, userDn);
        }
        private static DirectoryEntry FindUser(DirectoryEntry de, string userName)
        {
            //var deSearch = new DirectorySearcher(de)
            //{ SearchRoot = de, Filter = "(&(objectCategory=user)" };
            DirectorySearcher dSearch = new DirectorySearcher(de);
            dSearch.PageSize = 10000;
            dSearch.SizeLimit = 10000;
            dSearch.Filter = "(&(objectClass = user))";
            var qqqq = dSearch.FindAll();

            var results = dSearch.FindOne();
            return results != null ? results.GetDirectoryEntry() : null;
        }
        public static DirectoryEntry GetDirectoryEntryByUserName(string userName)
        {
            var de = GetDirectoryObject(GetDomain());
            return FindUser(de, userName);
        }

        private static string GetDomain()
        {
            string adDomain = WebConfigurationManager.AppSettings["adDomainFull"];
            return adDomain;
            //var domain = new StringBuilder();
            //string[] dcs = adDomain.Split('.');
            //for (var i = 0; i < dcs.GetUpperBound(0) + 1; i++)
            //{
            //    domain.Append("DC=" + dcs[i]);
            //    if (i < dcs.GetUpperBound(0))
            //    {
            //        domain.Append(",");
            //    }
            //}
            //return domain.ToString();
        }

        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {
            string adminUser = WebConfigurationManager.AppSettings["adAdminUser"];
            string adminPassword = WebConfigurationManager.AppSettings["adAdminPassword"];
            string fullPath = "LDAP://" + domainReference;

            var directoryEntry = new DirectoryEntry(fullPath, adminUser, adminPassword, AuthenticationTypes.Secure);
            return directoryEntry;
        }
    }
}
