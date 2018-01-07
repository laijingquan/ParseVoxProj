
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

    public class RGBA
    {
        public int r;
        public int g;
        public int b;
        public int a;
    }

    public class MainData
    {
        public List<VoxData> voxDatas = new List<VoxData>();
        public List<RGBA> rgbas = new List<RGBA>();
    }
}
