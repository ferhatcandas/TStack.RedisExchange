using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TStack.RedisExchange.Tests;
using FluentAssertions;
using System.Threading;
using TStack.RedisExchange.Tests.Entity;
using System.Linq;
using TStack.RedisExchange.Tests.Connection;

namespace TStack.RedisExchange.Tests.Tests
{
    public class StringSetTests
    {
        ProjectProvider provider = new ProjectProvider(new RedisContext());
        [Fact]
        public void Add_model_should_add_success()
        {
            string key = "stringsetkey";
            provider.Delete(key);

            var person = MockData.GetPersonMock();

            bool actual = provider.Add(key, person);

            actual.Should().BeTrue();
        }
        [Fact]
        public void Add_model_with_expireTime_TimeSpan_success()
        {
            string key = "stringsetkey";

            provider.Delete(key);

            var person = MockData.GetPersonMock();

            var timeOut = new TimeSpan(0, 0, 5);

            bool actual = provider.Add(key, person, timeOut);

            actual.Should().BeTrue();

            actual = provider.Exists(key);

            actual.Should().BeTrue();

            Thread.Sleep(timeOut);

            actual = provider.Exists(key);

            actual.Should().BeFalse();
        }
        [Fact]
        public void Add_model_with_expireTime_DateTime_success()
        {
            string key = "stringsetkey";

            provider.Delete(key);

            var person = MockData.GetPersonMock();

            var timeOut = new DateTime(DateTime.Now.AddSeconds(5).Ticks);

            bool actual = provider.Add(key, person, timeOut);

            actual.Should().BeTrue();

            actual = provider.Exists(key);

            actual.Should().BeTrue();

            Thread.Sleep(new TimeSpan(DateTime.Now.AddSeconds(5).Ticks - DateTime.Now.Ticks));

            actual = provider.Exists(key);

            actual.Should().BeFalse();
        }
        [Fact]
        public void Increment_key_value_should_incrase()
        {
            string key = "incrementKey";
            provider.Delete(key);

            long value = provider.Increment(key, 4);

            value.Should().Be(4);
        }
        [Fact]
        public void Increment_key_must_expire()
        {
            string key = "incrementKey";
            provider.Delete(key);

            var timeOut = new TimeSpan(0, 0, 5);

            long value = provider.Increment(key, timeOut, 4);

            value.Should().Be(4);

            Thread.Sleep(timeOut);

            bool actual = provider.Exists(key);

            actual.Should().BeFalse();
        }
        [Fact]
        public void Decrement_key_must_decrease()
        {
            string key = "decrementKey";
            provider.Delete(key);

            var timeOut = new TimeSpan(0, 0, 10);

            long value = provider.Increment(key, timeOut, 4);

            value.Should().Be(4);

            value = provider.Decrement(key, 2);

            value.Should().Be(2);
        }
        [Fact]
        public void GetString_must_return_personMockData()
        {
            string key = "personList";
            provider.Delete(key);

            var persons = MockData.GetPersonsMock();

            var timeOut = new TimeSpan(0, 0, 15);

            bool actual = provider.Add(key, persons, timeOut);

            actual.Should().BeTrue();

            var redisPersons = provider.GetString<List<Person>>(key);

            foreach (var person in persons)
            {
                bool exist = redisPersons.FirstOrDefault(x => x.Id == person.Id) != null;

                exist.Should().BeTrue();
            }

        }
    }
}
