using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Wei.BitBot.Core;

namespace Wei.BitBot.BlockBeats
{
    public class BlockBeatsMessageNotificationBuilder : MessageNotificationBuilder
    {
        public BlockBeatsDetectiveDataItem Data { get; set; } = default!;

        public override StringContent Build()
        {
            var text = $"#### {Data.Title} \n {AddBlockQuote(new ReverseMarkdown.Converter().Convert(Data.Content))}";
            if (!string.IsNullOrWhiteSpace(Data.Img_Url))
            {
                text += $"> ![picture](\"{Data.Img_Url}\")";
            }

            return new StringContent(JsonSerializer.Serialize(new
            {
                msgtype = "markdown",
                markdown = new { title = Data.Title, text }
            }), Encoding.UTF8, "application/json");
        }

        private static string AddBlockQuote(string input)
        {
            // 按换行符分割段落
            string[] paragraphs = input.Split(["\n"], StringSplitOptions.RemoveEmptyEntries);

            // 在每个段落前添加 > 符号
            StringBuilder result = new StringBuilder();
            foreach (var paragraph in paragraphs)
            {
                result.AppendLine($"> {paragraph.Trim()}");
            }

            return result.ToString();
        }
    }
}
