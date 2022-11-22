using Backups.Algorithms;
using Backups.Extra.Visitors;

namespace Backups.Extra.AlgorithmSuper;

public interface IAlgorithmSuper : IAlgorithm
{
    void Accept(IAlgorithmVisitor visitor);
}
