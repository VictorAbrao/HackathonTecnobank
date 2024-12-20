﻿namespace Hackathon.SharedKernel.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void DefineId(int id) => Id = id;
        public void DefineUpdateAt(DateTime updatedAt) => UpdatedAt = updatedAt;
    }
}
