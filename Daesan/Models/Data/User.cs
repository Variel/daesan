using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daesan.Models.Data
{
    public class User
    {
        public User() { }
        public User(string id) => Id = id;

        public string Id { get; set; }
        public int CurrentScene { get; set; }
        public DateTimeOffset StartedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? FinishedAt { get; set; }
    }
}
