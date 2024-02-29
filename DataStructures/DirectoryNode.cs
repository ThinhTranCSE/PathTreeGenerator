using DataStructures.Interfaces;
using System.Text;

namespace DataStructures
{
    [Serializable]
    public class DirectoryNode : TreeNode
    {
        public List<ITreeNode> Children { get; private set; }

        public List<ITreeNode> Files { get; private set; }

        public DirectoryNode(string path, ITreeNode? parent) : base(path, parent)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path); //dir name
            Children = new List<ITreeNode>();
            Files = new List<ITreeNode>();
        }

        public void Add(ITreeNode node)
        {
            Children.Add(node);
        }

        public override IEnumerable<ITreeNode> Traverse()
        {
            string[] subDirectories = System.IO.Directory.GetDirectories(Path);
            Array.Sort(subDirectories);
            string[] files = System.IO.Directory.GetFiles(Path);
            Array.Sort(files);

            foreach (string file in files)
            {
                FileNode fileNode = new FileNode(file, this);
                fileNode.Traverse();
                Add(fileNode);
                Files.Add(fileNode);
            }
            foreach (string subDirectory in subDirectories)
            {
                DirectoryNode directoryNode = new DirectoryNode(subDirectory, this);
                var subFiles = directoryNode.Traverse();
                Add(directoryNode);
                Files.AddRange(subFiles);
            }


            byte[] pathBytes = Encoding.UTF8.GetBytes(Path);
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);
                foreach (var child in Children)
                {
                    byte[] hash = child.CheckSum;
                    md5.TransformBlock(hash, 0, hash.Length, null, 0);
                }
                md5.TransformFinalBlock(new byte[0], 0, 0);
                CheckSum = md5.Hash;
            }
            return Files;
        }

        public override string Display(string indent = "", bool isDisplayHash = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (isDisplayHash)
            {
                stringBuilder.Append($"{indent}|{Name} - {CheckSumString}\n");
            }
            else
            {
                stringBuilder.Append($"{indent}|{Name}\n");
            }
            foreach (ITreeNode child in Children)
            {
                stringBuilder.Append(child.Display(indent + "        ", isDisplayHash));
            }
            return stringBuilder.ToString();
        }
    }
}