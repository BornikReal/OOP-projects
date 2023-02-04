namespace Application.ChainOfResponsibilities.WorkerModels;

public record SlaveWorkerModel(string name, int access)
    : BaseWorkerModel(name, access);