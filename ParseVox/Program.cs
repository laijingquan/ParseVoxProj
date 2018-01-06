using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

namespace ParseVox
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/李剑峰/Source/Repos/ParseVox/ParseVox/chr_rain.vox";
            List<byte> readBytes = new List<byte>();

            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            var voxBytes = br.ReadBytes(4);
            string vox = Encoding.UTF8.GetString(voxBytes);
            int versionBytes = System.BitConverter.ToInt32(br.ReadBytes(4), 0);


            //read Main Chunk
            var mainID = Encoding.UTF8.GetString(br.ReadBytes(4));
            var mainChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
            var mainChildCount = BitConverter.ToInt32(br.ReadBytes(4), 0);

            int offset = 0;
            int size = 0;
            size = mainChildCount;//子Chunk的内容大小

            //read child Chunk
            while (offset < size)
            {
                var childID = Encoding.UTF8.GetString(br.ReadBytes(4));
                var childChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                var childChildCount = BitConverter.ToInt32(br.ReadBytes(4), 0);
                switch (childID)
                {
                    case "XYZI":
                        ParseXYZI(br, childChunkSize);
                        break;
                    default:
                        var content = br.ReadBytes(childChunkSize);
                        break;
                        //case "PACK":
                        //    break;
                        //case "SIZE":
                        //    break;
                        //case "RGBA":
                        //    break;
                        //case "MATT":
                        //    break;
                }
                offset += childChunkSize + 12;
            }
        }

        static void TestJson()
        {

        }

        static void ParseXYZI(BinaryReader br, int readsize)
        {
            var md = new MainData();
            var numVoexls = BitConverter.ToInt32(br.ReadBytes(4), 0);
            readsize -= 4;
            while (readsize > 0)
            {
                var c = br.ReadBytes(4);
                var x = c[0];
                var y = c[1];
                var z = c[2];
                var colorIndex = c[3];
                readsize -= 4;

                md.voxDatas.Add(new VoxData() { x = x, y = y, z = z, colorIndex = colorIndex });
            }

            string json = JsonConvert.SerializeObject(md,Formatting.Indented);

            SaveJsonToFile("C:/Users/李剑峰/Source/Repos/ParseVox/ParseVox/chr_rain.json", json);
        }

        static void SaveJsonToFile(string despath, string json)
        {
            using (FileStream file = new FileStream(despath, FileMode.Create, FileAccess.Write))
            {
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                file.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }
    }
}
