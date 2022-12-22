namespace Application.СhainOfResponsibilities.WorkerModels;

public record SlaveWorkerModel(string name, int access)
    : BaseWorkerModel(name, access);