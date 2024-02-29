using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    [Serializable]
    public abstract class TreeNode : ITreeNode
    {
        public string Name { get; protected set; }

        public string Path { get; set; }

        public string CheckSumString => BitConverter.ToString(CheckSum).Replace("-", "").ToLower();
        public byte[] CheckSum { get; protected set; } = Array.Empty<byte>();

        public ITreeNode? Parent { get; protected set; }

        public int Level { get; protected set;}


        public TreeNode(string path, ITreeNode? parent)
        {
            //check file or directory path exists, if not throw error
            if(!Directory.Exists(path) && !File.Exists(path))
            {
                throw new ArgumentException("Path does not exist", nameof(path));
            }
            Path = path;
            Parent = parent;
            Level = parent == null ? 0 : parent.Level + 1;
        }

        public abstract IEnumerable<ITreeNode> Traverse(); // returns all file children of the node
        public abstract string Display(string indent = "", bool isDisplayHash = false);
        protected static byte[] CalculateHash(byte[] inputBytes)
        {
            byte[] hashBytes = MD5.HashData(inputBytes);
            return hashBytes;
        }

    }
}
