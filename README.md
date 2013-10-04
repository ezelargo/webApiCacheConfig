webApiCacheConfig
=================

Plugin para AspNetWebApi-OutputCache (https://github.com/filipw/AspNetWebApi-OutputCache) que permite la configuracion en web.cofig:

1)- Debes crear el elemento "section" dentro de "configsections" del web.config de tu proyecto:
        
    <configuration>
      <configSections>
      
        <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
        <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
        
        <!-- Cache Output Api Configuration -->
        <section name="webApiCacheConfig"
                 type="SampleWebApiCacheConfig.CacheConfig.WebApiCacheConfigSection, SampleWebApiCacheConfig"
                 requirePermission="false"
                 allowLocation="true" />    
        
    </configSections>

2)- Crea los perfiles para cada una de las acciones que representan las apis de tu controlador:

    <!-- Cache Output Api Configuration -->
      <webApiCacheConfig>
        <profiles>
          <!-- name == ControllerName_ActionName -->
          <add name="ValuesController_Get" enable="true" clientTimeSpan="120" serverTimeSpan="0" anonymousOnly="false" noCache="false" mustRevalidate="true" excludeQueryStringFromCacheKey="false"/>
          <add name="ValuesController_GetParam" enable="true" clientTimeSpan="240" serverTimeSpan="0" anonymousOnly="false" noCache="false" mustRevalidate="true" excludeQueryStringFromCacheKey="false"/>    
        </profiles>
      </webApiCacheConfig>

3)- Agregar las clases WebApiCacheConfig y  WebApiCacheOutputAttribute en tu proyecto
    (podes ver esto en SampleWebApiCacheConfig project)

4)- Implementa WebApiCacheOutputAttribute en las acciones de tu controlador:

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
      
    
