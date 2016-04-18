using System;
using System.ComponentModel.DataAnnotations;

namespace Zanotuj.To.WebApplication.Repository
{
    public abstract class BusinessModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public BusinessModel()
        {
            CreateTime=UpdateTime=DateTime.Now;
        }

    }
}