using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataStructures
{
    [Serializable]
    public class FileNode : TreeNode
    {
        public FileNode(string path, ITreeNode? parent) : base(path, parent)
        {
            Name = System.IO.Path.GetFileName(path);
        }

        public override IEnumerable<ITreeNode> Traverse()
        {
            byte[] pathBytes = Encoding.UTF8.GetBytes(Path);
            byte[] fileContent = File.ReadAllBytes(Path);
            //using (var md5 = System.Security.Cryptography.MD5.Create())
            //{
            //    md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);
            //    md5.TransformFinalBlock(fileContent, 0, fileContent.Length);
            //    CheckSum = BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
            //    pathToHashDict.Add(Path, md5.Hash);
            //}
            CheckSum = CalculateHash(fileContent);
            return Array.Empty<ITreeNode>();
        }

        public override string Display(string indent, bool isDisplayHash = false)
        {
            //display file name with indent
            StringBuilder stringBuilder = new StringBuilder();
            if (isDisplayHash)
            {
                stringBuilder.Append($"{indent}|{Name} - {CheckSumString}\n");
            }
            else
            {
                stringBuilder.Append($"{indent}|{Name}\n");
            }
            return stringBuilder.ToString();
        }
    }
}
