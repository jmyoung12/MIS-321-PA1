using System;
using System.Collections.Generic;
using System.Text;

namespace jmyoung12_pa1
{
    public class Post
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return this.ID + "#" + this.Message + "#" + this.Timestamp.ToShortDateString();
        }
    }
}
