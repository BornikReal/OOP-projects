namespace Application.СhainOfResponsibilities.WorkerModels;

public abstract record CreateBaseWorkerModel(Guid sessionId, string name, int access, string login, string password);