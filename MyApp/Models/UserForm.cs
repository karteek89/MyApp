using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class UserForm
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public string Field10 { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}