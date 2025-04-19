using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.BitBot.BlockBeats
{
    public class BlockBeatsDetectiveDto
    {
        public int Code { get; set; }
        public string Msg { get; set; } = default!;
        public BlockBeatsDetectiveData Data { get; set; } = default!;
    }

    public class BlockBeatsDetectiveData
    {
        public List<BlockBeatsDetectiveDataItem> List { get; set; } = default!;
    }

    public class BlockBeatsDetectiveDataItem
    {
        public int Id { get; set; }
        public int Article_Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? Img_Url { get; set; }
    }
}
