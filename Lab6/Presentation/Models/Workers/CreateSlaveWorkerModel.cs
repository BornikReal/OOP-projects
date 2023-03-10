namespace Application.ChainOfResponsibilities.WorkerModels;

public record CreateSlaveWorkerModel(Guid sessionId, string name, int access, string login, string password)
    : CreateBaseWorkerModel(sessionId, name, access, login, password);