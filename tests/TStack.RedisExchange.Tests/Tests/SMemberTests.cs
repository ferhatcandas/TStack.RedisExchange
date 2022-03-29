using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TStack.RedisExchange.Tests.Connection;
using TStack.RedisExchange.Tests.Entity;
using Xunit;

namespace TStack.RedisExchange.Tests.Tests
{
    public class SMemberTests
    {
        ProjectProvider provider = new ProjectProvider(new RedisContext());

        [Fact]
        public void Add_must_success()
        {
            string key = "SMemberKey";
            provider.Delete(key);

            var persons = MockData.GetPersonsMock(15);

            long addedCount = provider.AddSet<Person>(key, persons);

            addedCount.Should().Be(15);

            var redisPersons = provider.GetSet<Person>(key);

            foreach (var redisPerson in redisPersons)
            {
                persons.Any(x => x.Id == redisPerson.Id).Should().BeTrue();
            }

            bool actual = provider.Delete(key);

            actual.Should().BeTrue();

            actual = provider.Exists(key);

            actual.Should().BeFalse();

        }
    }
}
