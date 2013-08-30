using System.Web.Http;


namespace Mvc.AngularTemplate.Controllers
{
    public class ValuesController : ApiController
    {
        public string Get([FromUri]ApiDemoClass ApiDemoClass)
        {
            return ApiDemoClass.Section;
        }
    }

    public class ApiDemoClass
    {
        public string Section { get; set; }
    }
}
