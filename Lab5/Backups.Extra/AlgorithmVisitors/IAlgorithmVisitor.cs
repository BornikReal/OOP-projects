using Backups.Extra.AlgorithmSuper;

namespace Backups.Extra.AlgorithmVisitors;

public interface IAlgorithmVisitor
{
    void Visit(SplitStorageAlgorithmVisitor algorithm);
    void Visit(SingleStorageAlgorithmVisitor algorithm);
}
