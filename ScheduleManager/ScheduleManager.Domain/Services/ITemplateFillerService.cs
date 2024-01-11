namespace ScheduleManager.Domain.Services
{
    public interface ITemplateFillerService
    {
        public string FillTemplate(string path, object model);
    }
}
