using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace hakimslivs.Models
{
    public class Category
    {
        [Key]
        public string Name { get; set; }
    }
}
