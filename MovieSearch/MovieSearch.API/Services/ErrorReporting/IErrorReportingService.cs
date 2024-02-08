namespace MovieSearch.API.Services.ErrorReporting
{
    public interface IErrorReportingService
    {
        void ReportError(Exception exception);
    }
}
