using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebAPI.OutputCache;

namespace SampleWebApiCacheConfig.CacheConfig
{   
    public class ProfileElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        [ConfigurationProperty("enable", IsRequired = true, DefaultValue = false)]
        public bool Enable
        {
            get
            {
                return Convert.ToBoolean(this["enable"]);
            }
        }

        [ConfigurationProperty("clientTimeSpan", IsRequired = true, DefaultValue = 0)]
        public int ClientTimeSpan
        {
            get
            {
                return Convert.ToInt32(this["clientTimeSpan"]);
            }
        }

        [ConfigurationProperty("serverTimeSpan", IsRequired = true, DefaultValue = 0)]
        public int ServerTimeSpan
        {
            get
            {
                return Convert.ToInt32(this["serverTimeSpan"]);
            }
        }

        [ConfigurationProperty("anonymousOnly", IsRequired = true, DefaultValue = false)]
        public bool AnonymousOnly
        {
            get
            {
                return Convert.ToBoolean(this["anonymousOnly"]);
            }
        }

        [ConfigurationProperty("noCache", IsRequired = true, DefaultValue = false)]
        public bool NoCache
        {
            get
            {
                return Convert.ToBoolean(this["noCache"]);
            }
        }

        [ConfigurationProperty("mustRevalidate", IsRequired = true, DefaultValue = true)]
        public bool MustRevalidate
        {
            get
            {
                return Convert.ToBoolean(this["mustRevalidate"]);
            }
        }

        [ConfigurationProperty("excludeQueryStringFromCacheKey", IsRequired = true, DefaultValue = false)]
        public bool ExcludeQueryStringFromCacheKey
        {
            get
            {
                return Convert.ToBoolean(this["excludeQueryStringFromCacheKey"]);
            }
        }
    }

    [ConfigurationCollection(typeof(ProfileElement), AddItemName = "add")]
    public class ProfilesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProfileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProfileElement)element).Name;
        }

        public new ProfileElement this[string name]
        {
            get
            {
                return (ProfileElement)BaseGet(name);
            }
        }
    }

    public class WebApiCacheConfigSection : ConfigurationSection
    {
        /// <summary>
        /// Perfiles
        /// </summary>
        [ConfigurationProperty("profiles", IsDefaultCollection = true)]
        public ProfilesCollection Profiles
        {
            get { return ((ProfilesCollection)(this["profiles"])); }
        }

        /// <summary>
        /// Obtiene los perfiles para cada accion que fuerfon configurados en el web.config
        /// </summary>
        /// <returns></returns>
        public static WebApiCacheConfigSection GetConfig()
        {
            return (WebApiCacheConfigSection)ConfigurationManager.GetSection("webApiCacheConfig");
        }
    }
       
}