namespace Backups.Extra.Visitor;

public interface IAlgorithmVisitor
{
    void Visit(SplitStorageAlgorithmVisitor algorithm);
    void Visit(SingleStorageAlgorithmVisitor algorithm);
}
