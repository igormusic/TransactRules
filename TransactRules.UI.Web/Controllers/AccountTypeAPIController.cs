using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TransactRules.Configuration;
using TransactRules.UI.Web.Models;
using TransactRules.Configuration.Data;
using TransactRules.Core.Utilities;

namespace TransactRules.UI.Web.Controllers
{
    public class AccountTypeAPIController : ApiController
    {
        private AccountTypeRepository db = new AccountTypeRepository { UnitOfWork = SessionState.Current.UnitOfWork };

        // GET api/AccountTypeAPI
        public IEnumerable<AccountType> GetAccountTypes()
        {
            return db.Items();
        }

        // GET api/AccountTypeAPI/5
        public AccountType GetAccountType(int id)
        {
            AccountType accounttype = db.GetById(id);

            if (accounttype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return accounttype;
        }

        // PUT api/AccountTypeAPI/5
        public HttpResponseMessage PutAccountType(int id, AccountType accounttype)
        {
            if (ModelState.IsValid && id == accounttype.Id)
            {

                try
                {
                    db.Update(accounttype);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/AccountTypeAPI
        public HttpResponseMessage PostAccountType(AccountType accounttype)
        {
            if (ModelState.IsValid)
            {
                db.Create(accounttype);
              
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, accounttype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = accounttype.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/AccountTypeAPI/5
        public HttpResponseMessage DeleteAccountType(int id)
        {
            AccountType accounttype = db.GetById(id);
            if (accounttype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                db.Delete(accounttype);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, accounttype);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}