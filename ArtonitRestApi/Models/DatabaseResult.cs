using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtonitRestApi.Models
{
    public class DatabaseResult
    {
        public State State { get; set; }

        public object Value { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum State
    {
        Successes,
        Error
    }
}
