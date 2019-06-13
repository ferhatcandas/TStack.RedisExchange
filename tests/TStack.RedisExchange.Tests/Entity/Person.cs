using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Tests.Entity
{
    public class Person
    {
        public Person()
        {

        }
        public Person(string name, double salary,int count, DateTime birthDate)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Count = count;
            Salary = salary;
            BirthDate = birthDate;
        }
        public string Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
