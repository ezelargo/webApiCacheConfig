using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.OutputCache;

namespace SampleWebApiCacheConfig.CacheConfig
{
    public class WebApiCacheOutputAttribute : CacheOutputAttribute
    {
        /// <summary>
        /// Perfil de cacheo declarado en web.config
        /// </summary>
        public string CacheProfile { get; set; }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            ProfileElement webApiProfileElement = null;
            webApiProfileElement = getWebApiCacheProfile();
            if (webApiProfileElement != null && webApiProfileElement.Enable) {
                this.ClientTimeSpan = webApiProfileElement.ClientTimeSpan;
                this.ServerTimeSpan = webApiProfileElement.ServerTimeSpan;
                this.MustRevalidate = webApiProfileElement.MustRevalidate;
                this.ExcludeQueryStringFromCacheKey = webApiProfileElement.ExcludeQueryStringFromCacheKey;
                this.AnonymousOnly = webApiProfileElement.AnonymousOnly;
            }
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// Obtiene el perfil de cache para una action api.
        /// </summary>        
        /// <returns></returns>
        private ProfileElement getWebApiCacheProfile()
        {
            var webApiCacheConfig = WebApiCacheConfigSection.GetConfig();
            ProfileElement webApiProfileElement = null;
            if (webApiCacheConfig != null && webApiCacheConfig.Profiles != null && webApiCacheConfig.Profiles.Count > 0) {
                webApiProfileElement = webApiCacheConfig.Profiles[CacheProfile];
            }
            return webApiProfileElement;
        }
    } 
}