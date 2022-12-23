namespace Application.СhainOfResponsibilities.WorkerModels;

public record CreateMasterWorkerModel(string name, int access, string login, string password)
    : CreateBaseWorkerModel(name, access, login, password);