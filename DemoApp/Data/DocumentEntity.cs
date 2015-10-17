using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data.Document
{
    public class DocumentEntity
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedWhen { get; set; }
        public int CreatedBy { get; set; }
    }
}
