using System.Collections.Generic;

namespace Visual_Novel_Universe.Models
{
    public class VnListUpdatedMessage
    {
        public IEnumerable<VisualNovel> VisualNovels { get; set; }
    }
}
