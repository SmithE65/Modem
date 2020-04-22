using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Modem.DataAudio
{
    public static class Modem
    {
        private const uint sampleRate = 48000;

        public static void KcsEncode300(Stream stream, byte[] dataBytes)
        {
            if (sampleRate % 300 != 0)
                throw new ArgumentException("Sample rate not evenly divisible by baud.");

            const uint bitSamples = sampleRate / 300;
            const uint frameSize = 11; // leading zero, eight data bits, two trailing ones

            WaveFile waveFile = new WaveFile(sampleRate, 1, 8);

            // signed byte buffer = dataBytes * byte frameSize * samples/bit
            sbyte[] waveData = new sbyte[dataBytes.Length * frameSize * bitSamples]; // bytes * bits/byte * samples/bit = samples

            uint index = 0;
            for (int i = 0; i < dataBytes.Length; i++)
            {
                bool[] bits = GetBits(dataBytes[i]);

                index = WriteBit300(waveData, index, false, bitSamples);

                for (int j = 0; j < 8; j++)
                {
                    index = WriteBit300(waveData, index, bits[j], bitSamples);
                }

                index = WriteBit300(waveData, index, true, bitSamples);
                index = WriteBit300(waveData, index, true, bitSamples);
            }

            byte[] outputBytes = new byte[waveData.Length];
            Buffer.BlockCopy(waveData, 0, outputBytes, 0, outputBytes.Length);

            waveFile.SetData(outputBytes);
            waveFile.WriteToStream(stream);
        }

        private static bool[] GetBits(byte b)
        {
            bool[] result = new bool[8];
            result[0] = (b & 0b00000001) != 0;
            result[1] = (b & 0b00000010) != 0;
            result[2] = (b & 0b00000100) != 0;
            result[3] = (b & 0b00001000) != 0;
            result[4] = (b & 0b00010000) != 0;
            result[5] = (b & 0b00100000) != 0;
            result[6] = (b & 0b01000000) != 0;
            result[7] = (b & 0b10000000) != 0;

            return result;
        }

        private static uint WriteBit300(sbyte[] buffer, uint offset, bool bit, uint bitSamples)
        {
            double t;
            if (bit)
            {
                t = Math.PI * 2 * 2400 / sampleRate;
            }
            else
            {
                t = Math.PI * 2 * 1200 / sampleRate;
            }
            
            for (int i = 0; i < bitSamples; i++)
            {
                buffer[offset + i] = (sbyte)(127 * Math.Sin(t * i));
            }

            return offset + bitSamples;
        }
    }
}
