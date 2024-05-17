using Domain.Models;
using Domain.Request;

namespace Domain.Mappers;

public class QuestMapper
{
    public static Quest ToClass(QuestRequest quest) => new Quest
    {
        Id = null,
        UserId = quest.UserId,
        Description = quest.Description
    };
}
