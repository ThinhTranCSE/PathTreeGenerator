using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    [Serializable]
    public class PathTree
    {
        public DirectoryNode Root { get; private set; }
        
        

        public PathTree(string path)
        {
            Root = new DirectoryNode(path, null);
            Root.Traverse();
        }

        public string Display(bool isDisplayHash = false)
        {
            return Root.Display("", isDisplayHash);
        }

        public void SaveTree(string savePath)
        {
            try
            {
                using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, this);
                }
                Console.WriteLine("Tree serialized successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during serialization: " + ex.Message);
            }
        }

        public static PathTree? LoadTree(string loadPath)
        {
            try
            {
                using (FileStream fs = new FileStream(loadPath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (PathTree)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during deserialization: " + ex.Message);
                return null;
            }
        }
    }
}
