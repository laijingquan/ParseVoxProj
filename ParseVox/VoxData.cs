
using System.Collections.Generic;

namespace ParseVox
{
    public class VoxData
    {
        public int x;
        public int y;
        public int z;
        public int colorIndex;
    }

    public class MainData
    {
        public List<VoxData> voxDatas = new List<VoxData>();
    }
}
