namespace UsersManagement.Persistence.Events;

internal sealed record UserMarkedAsDeletedEvent(string UserId, DateTime Deadline);
