using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.RedisExchange.Model
{
    public class SortedSetEntity<T>
    {
        public SortedSetEntity()
        {

        }
        public SortedSetEntity(T item, double score)
        {
            Item = item;
            Score = score;
        }
        public T Item { get; set; }
        public double Score { get; set; }
    }
}
