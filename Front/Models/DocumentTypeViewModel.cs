using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Models
{
    public class DocumentTypeViewModel
    {
        [DisplayName("Id")]
        public int IdDocumentType { get; set; }
        [DisplayName("IdDocumentType")]
        public string Document { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}