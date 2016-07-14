using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectUpload.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}