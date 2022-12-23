namespace Application.СhainOfResponsibilities.WorkerModels;

public record MasterWorkerModel(string name, int access)
    : BaseWorkerModel(name, access);