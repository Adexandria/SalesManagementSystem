﻿namespace SalesManagementSystem.Model
{
    public class Good
    {
        public Good()
        {
            GoodId = Guid.NewGuid();
        }
        public Guid GoodId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; } 
    }
}
