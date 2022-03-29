using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TStack.RedisExchange.Model;
using TStack.RedisExchange.Tests.Connection;
using TStack.RedisExchange.Tests.Entity;
using Xunit;

namespace TStack.RedisExchange.Tests.Tests
{
    public class SortedSetTests
    {
        ProjectProvider provider = new ProjectProvider(new RedisContext());

        [Fact]
        public void Add_model_and_score_should_success()
        {
            string key = "SortedSetItemKey";
            provider.Delete(key);

            var person1 = MockData.GetPersonMock();

            bool actual = provider.AddSortedSet(key, person1, 700);

            actual.Should().BeTrue();

            var person2 = MockData.GetPersonMock();

            actual = provider.AddSortedSet(key, person2, 500);

            actual.Should().BeTrue();

            var redisPersons = provider.GetSortedSets<Person>(key);

            redisPersons.FirstOrDefault().Id.Should().Be(person2.Id);

            redisPersons.LastOrDefault().Id.Should().Be(person1.Id);

            actual = provider.Delete(key);

            actual.Should().BeTrue();

            actual = provider.Exists(key);

            actual.Should().BeFalse();
        }

        [Fact]
        public void Add_sortedsetlist_should_success()
        {
            string key = "SortedSetItemsKey";
            provider.Delete(key);

            var persons = MockData.GetPersonsMock(12);

            int count = 500;

            List<SortedSetEntity<Person>> list = new List<SortedSetEntity<Person>>();
            foreach (var person in persons)
            {
                SortedSetEntity<Person> entities = new SortedSetEntity<Person>(person, count);
                list.Add(entities);
                count += 500;
            }

            long addedCount = provider.AddSortedSet(key, list);

            addedCount.Should().Be(12);

            var sortedSets = provider.GetSortedSetsAll<Person>(key);

            int expCount = 500;
            int index = 0;
            foreach (var set in sortedSets)
            {
                var person = persons.ElementAt(index);

                set.Item.Id.Should().Be(person.Id);
                set.Score.Should().Be(expCount);

                index += 1;
                expCount += 500;
            }

            bool actual = provider.Delete(key);

            actual.Should().BeTrue();

            actual = provider.Exists(key);

            actual.Should().BeFalse();

        }
    }
}
