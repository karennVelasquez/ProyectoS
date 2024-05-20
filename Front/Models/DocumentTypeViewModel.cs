using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.ComponentModel;
using System.Text;

namespace Front.Models
{
    public class DocumentTypeViewModel
    {
        [DisplayName("Id")]
        public int IdDocumentType { get; set; }

        [DisplayName("Document Type")]
        public string Document { get; set; }
        public bool IsDelete { get; set; }
    }
}