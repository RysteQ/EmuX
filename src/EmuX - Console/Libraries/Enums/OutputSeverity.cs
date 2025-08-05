using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX_Console.Libraries.Enums;

/// <summary>
/// Used to describe the severity of a message on the console
/// </summary>
public enum OutputSeverity : byte
{
    Normal,
    Important,
    Error
}
