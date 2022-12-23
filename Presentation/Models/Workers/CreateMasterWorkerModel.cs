namespace Application.СhainOfResponsibilities.WorkerModels;

public record CreateMasterWorkerModel(Guid sessionId, string name, int access, string login, string password)
    : CreateBaseWorkerModel(sessionId, name, access, login, password);