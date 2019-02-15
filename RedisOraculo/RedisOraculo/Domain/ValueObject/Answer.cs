using System;
using System.Collections.Generic;
using System.Text;

namespace RedisOraculo.Domain.ValueObject
{
    public class Answer
    {
        public List<string> questions { get; set; }
        public string answer { get; set; }
        public double score { get; set; }
        public int id { get; set; }
        public string source { get; set; }
        public List<object> metadata { get; set; }
    }
}
