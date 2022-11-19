using Backups.Algorithms;
using Backups.Extra.Visitor;

namespace Backups.Extra.AlgorithmSuper;

public interface IAlgorithmSuper : IAlgorithm
{
    void Accept(IAlgorithmVisitor visitor);
}
