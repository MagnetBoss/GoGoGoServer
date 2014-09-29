using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using gogogoserverService.DataObjects;
using gogogoserverService.Models;

namespace gogogoserverService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            AutoMapper.Mapper.Initialize(cfg =>
            {
                
                cfg.CreateMap<ParticipantItem, ParticipantItemDto>();
                cfg.CreateMap<ParticipantItemDto, ParticipantItem>();
                cfg.CreateMap<EventItem, EventItemDto>();
                cfg.CreateMap<EventItemDto, EventItem>();
            });

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new gogogoserverInitializer());
        }
    }

    public class gogogoserverInitializer : ClearDatabaseSchemaIfModelChanges<gogogoserverContext>
    {
        protected override void Seed(gogogoserverContext context)
        {
            base.Seed(context);
        }
    }
}

