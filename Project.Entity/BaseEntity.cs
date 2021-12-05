using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DataStatus Status { get; set; } = DataStatus.Active;
    }
}
