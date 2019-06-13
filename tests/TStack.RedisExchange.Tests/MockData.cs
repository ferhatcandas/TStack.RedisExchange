using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TStack.RedisExchange.Tests.Entity;

namespace TStack.RedisExchange.Tests
{
    public static class MockData
    {
        const string alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        const string numerics = "0123456789";
        public static List<Person> GetPersonsMock(int length = 5)
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < length; i++)
            {
                persons.Add(GetPersonMock());
            }
            return persons;
            //return new List<Person>()
            //{
            //    new Person("ferhat",1442.20,25,new DateTime(1987,07,24)),
            //    new Person("ahmet",2400.92,32,new DateTime(2000,07,24)),
            //    new Person("ayşe",5025.67,42,new DateTime(1992,07,24)),
            //    new Person("selim",700,15,new DateTime(1966,07,24)),
            //    new Person("hakan",986.67,7,new DateTime(1980,07,24)),
            //    new Person("fuat",7650.25,33,new DateTime(1960,07,24))
            //};
        }
        public static Person GetPersonMock()
        {
            return new Person(alphanumerics.GetRandomize(GetRandomLength()), GetDoubleRandom(),GetRandomLength(), new DateTime(2001, 07, 24));
        }

        private static double GetDoubleRandom(int length = 4)
        {
            var nums = string.Join("", numerics.ToList().Where(x => x.ToString() != "0"));
            double value = double.Parse(nums.GetRandomize(length) + "," + numerics.GetRandomize(2));
            return value;
        }
        private static int GetRandomLength()
        {
            return int.Parse("1" + numerics.GetRandomize(1));
        }
        private static string GetRandomize(this string value, int length)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(value, length)
                     .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
