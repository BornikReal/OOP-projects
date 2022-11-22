using Backups.Algorithms;
using Backups.Extra.AlgorithmVisitors;

namespace Backups.Extra.AlgorithmSuper;

public interface IAlgorithmSuper : IAlgorithm
{
    void Accept(IAlgorithmVisitor visitor);
}
