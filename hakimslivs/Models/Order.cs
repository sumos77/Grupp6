using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace hakimslivs.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateTime OrderDate { get; set; }

        [ForeignKey("AspNetUserId")]
        public ApplicationUser User { get; set; }
    }
}
