using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Interfaces
{
    public interface ITreeNode
    {
        string Name { get; }
        string Path { get; }
        string CheckSumString { get; }
        byte[] CheckSum { get; }

        ITreeNode? Parent { get; }

        int Level { get; }

        string Display(string indent = "", bool isDisplayHash = false);
        IEnumerable<ITreeNode> Traverse();
    }
}
