using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Modem.DataAudio.Structs;

namespace Modem.DataAudio
{
    public class WaveFile
    {
        private WaveHeader Header { get; set; } = new WaveHeader();
        private WaveFormat Format { get; set; } = new WaveFormat();
        private WaveData Data { get; set; } = new WaveData();

        public WaveFile(uint sampleRate, ushort channels, uint bitDepth)
        {
            Format.SamplesRate = sampleRate;
            Format.Channels = channels;
            Format.BitDepth = bitDepth;
        }

        public WaveFile()
        {

        }

        public void SetData(byte[] dataBytes)
        {
            Data.DataArray = dataBytes;
        }

        public void WriteToStream(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Write the header to the stream
                writer.Write(Header.GroupId.ToCharArray());
                writer.Write(Header.FileLength);
                writer.Write(Header.RiffType.ToCharArray());

                // Write the format chunk
                writer.Write(Format.ChunkId.ToCharArray());
                writer.Write(Format.ChunkSize);
                writer.Write(Format.FormatTag);
                writer.Write(Format.Channels);
                writer.Write(Format.SamplesRate);
                writer.Write(Format.AverageByteRate);
                writer.Write(Format.BlockAlign);
                writer.Write(Format.BitDepth);

                // Write the data chunk
                writer.Write(Data.ChunkId.ToCharArray());
                writer.Write(Data.ChunkSize);
                writer.Write(Data.DataArray);

                // Write the file size
                writer.Seek(4, SeekOrigin.Begin);
                writer.Write((uint) (writer.BaseStream.Length - 8));
            }
        }
    }
}
