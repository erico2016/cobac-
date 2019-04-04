public class GenerateReportModel 
{
    public long ID { get; set; }

    public string ReportID { get; set; }

    public string ReportName { get; set; }

    public string ReportDescription { get; set; }

    public string FileType { get; set; }

    public DateTime? ReportDelivery { get; set; }

    public string ReportSchedule { get; set; }

    public DateTime? ReportPosition { get; set; }

    public DateTime? ReportPositionLast { get; set; }

    public DateTime? ReportPositionNext { get; set; }

    public string ReportDaysName { get; set; }

    public string ReportMonthsName { get; set; }

    public byte? ReportByWeekPosition { get; set; }

    public string ReportByWeekDayName { get; set; }

    public string ReportRecipients { get; set; }

    public string ReportCategory { get; set; }

    public string ReportStatus { get; set; }

    public DateTime? Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public string ModifiedBy { get; set; }

}
