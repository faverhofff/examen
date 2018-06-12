using System.Web.Http;
using Examen.Services;
using Examen.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Http.Cors;

namespace Examen.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api")]
    public class ApplicationController : ApiController
    {
        protected Aplication _Service { get; set; }

        public ApplicationController()
        {
            _Service = new Aplication();
        }

        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
        }

        /// <summary>
        /// Search match of a word
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri] Search Form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Json(_Service.Search(Form.Word));
        }

        /// <summary>
        /// Indexing a page and his link results into the system
        /// </summary>
        /// <param name="Site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("index")]
        public IHttpActionResult IndexPage([FromUri] IndexUrl Site)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!UrlParser.checkURLFormat(Site.Url))
                return BadRequest(ModelState);

            _Service.Scrap(new List<string>() { Site.Url });

            return Json(_Service.getIndexResult());
        }

        /// <summary>
        /// Database from cero
        /// </summary>
        /// <param name="Site"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("clear")]
        public IHttpActionResult ClearPage([FromBody] IndexUrl Site)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _Service.RemoveIndexContent();

            return Json(new List<bool>() { true });
        }


    }
}
