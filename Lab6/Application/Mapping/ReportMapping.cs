using Application.Dto;
using Domain.Accounts;
using Domain.Activity;

namespace Application.Mapping;

public static class ReportMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(report.Id, new List<MessageLog>(report.MessageLogs));
}
