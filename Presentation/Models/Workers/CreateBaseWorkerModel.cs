namespace Application.СhainOfResponsibilities.WorkerModels;

public abstract record CreateBaseWorkerModel(string name, int access, string login, string password);