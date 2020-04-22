using System;
using System.Collections.Generic;
using System.Text;

namespace Modem.DataAudio.Structs
{
    internal class WaveData
    {
        public string ChunkId { get; set; } = "data";
        public uint ChunkSize => (uint)DataArray.Length;
        public byte[] DataArray { get; set; } = new byte[0];
    }
}
