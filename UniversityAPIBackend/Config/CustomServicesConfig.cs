using UniversityAPIBackend.Services;

namespace UniversityAPIBackend.Config
{
    public class CustomServicesConfig
    {

        public static void DeclareCustomServices(WebApplicationBuilder builder)
        {
            DeclareCustomSerachServices(builder);
        }

        private static void DeclareCustomSerachServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICoursesService, CoursesService>();
            builder.Services.AddScoped<IStudentsService, StudentsService>();
            builder.Services.AddScoped<IChaptersService, ChaptersService>();
            // TODO: Inject the remaining search services
        }
    }
}
