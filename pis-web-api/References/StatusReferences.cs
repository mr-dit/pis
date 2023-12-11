using pis.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Models.db.ReportStates;

namespace pis_web_api.References
{
    public static class StatusReferences
    {
        private static Dictionary<string, Func<Report, ReportStatusAbstract>> _statusFactoryMethods =
        new Dictionary<string, Func<Report, ReportStatusAbstract>>(StringComparer.OrdinalIgnoreCase)
        {
            { "Черновик", DRAFT },
            { "Доработка", REWORK },
            { "Согласование у исполнителя", AGREEMENT_FROM_PERFORMER },
            { "Согласовано исполнителем", AGREED_FROM_PERFORMER },
            { "Согласовано ОМСУ", AGREED_FROM_OMSU },
        };

        public static ReportStatusAbstract GetStatusByName(string name, Report report)
        {
            if (_statusFactoryMethods.TryGetValue(name, out var factoryMethod))
            {
                return factoryMethod(report);
            }

            throw new ArgumentException("Не существует статуса с именем:", name);
        }

        public static ReportStatusAbstract DRAFT(Report report) => new DraftStatus(report);
        public static ReportStatusAbstract REWORK(Report report) => new ReworkStatus(report);
        public static ReportStatusAbstract AGREEMENT_FROM_PERFORMER(Report report) => new AgreementFromPerformerStatus(report);
        public static ReportStatusAbstract AGREED_FROM_PERFORMER(Report report) => new AgreedFromPerformer(report);
        public static ReportStatusAbstract AGREED_FROM_OMSU(Report report) => new AgreedFromOMSU(report);
    }
}
