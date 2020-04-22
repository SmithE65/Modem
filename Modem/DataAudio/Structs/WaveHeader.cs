using System;
using System.Collections.Generic;
using System.Text;

namespace Modem.DataAudio.Structs
{
    internal class WaveHeader
    {
        public string GroupId { get; set; } = "RIFF";
        public uint FileLength { get; set; } = 0;
        public string RiffType { get; set; } = "WAVE";
    }
}
