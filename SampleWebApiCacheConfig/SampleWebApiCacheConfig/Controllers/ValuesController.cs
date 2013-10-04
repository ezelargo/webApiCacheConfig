using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using SampleWebApiCacheConfig.CacheConfig;

namespace SampleWebApiCacheConfig.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values     
        [System.Web.Http.HttpGet]
        [WebApiCacheOutput(CacheProfile = "ValuesController_Get")]
        public HttpResponseMessage Get()
        {            
            HttpResponseMessage httpResponseMessage = null;
            httpResponseMessage = this.Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    @Name = "Pajarito",
                    @Sleep = "En arboles",
                    @Eat = "Gusanitos e insectos",
                    @HeLive = "En su casa, Argentina"
                }
                , Configuration.Formatters.JsonFormatter);

            return httpResponseMessage;
        }

        // GET api/values/1 he live or > 1 he died...
        [System.Web.Http.HttpGet]
        [WebApiCacheOutput(CacheProfile = "ValuesController_GetParam")]
        public HttpResponseMessage Get(int id)
        {            
            HttpResponseMessage httpResponseMessage = null;
            try {
                if (id == 1) {
                    httpResponseMessage = this.Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            @Name = "Mono",
                            @Sleep = "En arboles",
                            @Eat = "Gusanitos e insectos",
                            @HeLive = "En su casa, Argentina"
                        }
                        , Configuration.Formatters.JsonFormatter);
                } else {
                    throw new Exception();
                }
            } catch {
                httpResponseMessage = this.Request.CreateResponse(HttpStatusCode.NotFound,
                    new
                    {
                        @title = "Error",
                        @description = "Mono died...",
                    }, Configuration.Formatters.JsonFormatter);
            }
            return httpResponseMessage;
        }

        #region Dont Work with WebApi outputCache

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        #endregion

    }
}