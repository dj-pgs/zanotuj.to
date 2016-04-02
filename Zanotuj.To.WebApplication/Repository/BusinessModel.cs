using System;

namespace Zanotuj.To.WebApplication.Repository
{
    public abstract class BusinessModel
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}