namespace Application.ChainOfResponsibilities.WorkerModels;

public record MasterWorkerModel(string name, int access)
    : BaseWorkerModel(name, access);