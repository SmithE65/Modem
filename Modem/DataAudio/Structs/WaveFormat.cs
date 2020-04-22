using System;
using System.Collections.Generic;
using System.Text;

namespace Modem.DataAudio.Structs
{
    internal class WaveFormat
    {
        public string ChunkId { get; set; } = "fmt ";
        public uint ChunkSize { get; set; } = 18;
        public ushort FormatTag { get; set; } = 1;
        public ushort Channels { get; set; } = 2;
        public uint SamplesRate { get; set; } = 44100;
        public uint AverageByteRate => SamplesRate * BlockAlign;
        public ushort BlockAlign  => (ushort)(Channels * BitDepth / 8);
        public uint BitDepth { get; set; } = 16;
    }
}
