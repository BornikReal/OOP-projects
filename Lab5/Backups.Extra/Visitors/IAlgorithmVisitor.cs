using Backups.Extra.AlgorithmSuper;

namespace Backups.Extra.Visitors;

public interface IAlgorithmVisitor
{
    void Visit(SplitStorageAlgorithmVisitor algorithm);
    void Visit(SingleStorageAlgorithmVisitor algorithm);
}
