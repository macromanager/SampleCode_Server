using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace MacroContext.Infrastructure.Abstractions.Mapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration MapConfig { get; private set; }

        static AutoMapperConfiguration()
        {
            MapConfig = new MapperConfiguration(cfg =>
            {
                foreach (var profile in GetProfiles())
                {
                    cfg.AddProfile(profile);
                }

            });
        }

        private static IEnumerable<Profile> GetProfiles()
        {
            IEnumerable<Profile> profiles = typeof(PackageProfile)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Profile)) && !t.IsAbstract)
                .Select(t => (Profile)Activator.CreateInstance(t));
            return profiles;
        }
        
    }
}
