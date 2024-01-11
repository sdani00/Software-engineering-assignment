using RazorEngineCore;
using ScheduleManager.Domain.Services;

namespace ScheduleManager.Application.Services
{
    public class TemplateFillerService : ITemplateFillerService
    {
        private IRazorEngine _razorEngine;

        public TemplateFillerService(IRazorEngine razorEngine)
        {
            _razorEngine = razorEngine;
        }

        public string FillTemplate(string path, object model)
        {
            string template = File.ReadAllText(path);
            IRazorEngineCompiledTemplate compiledTemplate = _razorEngine.Compile(template);

            string result = compiledTemplate.Run(model);
            return result;
        }
    }
}
