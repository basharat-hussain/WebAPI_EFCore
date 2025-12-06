using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_EFCore_Data.Models
{
    public class Student : BaseModel
    {
        public int Id { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Parentage { get; set; }
        public string Address { get; set; }
    }
}
