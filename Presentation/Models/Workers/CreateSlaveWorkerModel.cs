namespace Application.СhainOfResponsibilities.WorkerModels;

public record CreateSlaveWorkerModel(string name, int access, string login, string password)
    : CreateBaseWorkerModel(name, access, login, password);