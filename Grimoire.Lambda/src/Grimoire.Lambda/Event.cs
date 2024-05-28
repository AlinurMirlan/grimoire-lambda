using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Lambda;

public class Event
{
    public required string Type { get; set; }
    public required string StreamId { get; set; }
}
