using System;
using System.IO;

using Modem.DataAudio;

namespace Modem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //const uint sampleRate = 44100;

            //// Generate a test WAV
            //WaveFile wav = new WaveFile(sampleRate, 2, 8);

            //sbyte[] leftChannel = Generate8BitTone(440f, sampleRate, 40);
            //sbyte[] rightChannel = Generate8BitTone(880f, sampleRate, 20);

            //sbyte[] rightZero = new sbyte[leftChannel.Length];

            //for (int i = 0; i < rightZero.Length; i++)
            //{
            //    if (i < rightChannel.Length)
            //        rightZero[i] = rightChannel[i];
            //    else
            //        rightZero[i] = 0;
            //}

            //if (leftChannel.Length != rightZero.Length)
            //{
            //    Console.WriteLine("Something went wrong.");
            //    Console.ReadLine();
            //    return;
            //}

            //sbyte[] shortData = new sbyte[leftChannel.Length + rightZero.Length];

            //for (int i = 0; i < leftChannel.Length; i++)
            //{
            //    shortData[(2 * i)] = leftChannel[i];
            //    shortData[(2 * i) + 1] = rightZero[i];
            //}

            //byte[] dataBytes = new byte[shortData.Length];
            //Buffer.BlockCopy(shortData, 0, dataBytes, 0, dataBytes.Length);

            //wav.SetData(dataBytes);

            //using (FileStream fs = File.Create("test.wav"))
            //{
            //    wav.WriteToStream(fs);
            //}

            byte[] fileBytes = File.ReadAllBytes("Program.cs");
            using (FileStream fs = File.Create("test.wav"))
            {
                DataAudio.Modem.KcsEncode300(fs, fileBytes);
            }

            Console.WriteLine("Go check your .wav file.");
            Console.ReadLine();
        }

        private static short[] Generate16BitTone(double frequency, uint sampleRate, double seconds)
        {
            const int amplitude = 32760;
            uint numSamples = (uint)(sampleRate * seconds);
            short[] array = new short[numSamples];
            double step = (Math.PI * 2 * frequency) / sampleRate;

            for (int i = 0; i < numSamples; i++)
            {
                array[i] = Convert.ToInt16(amplitude * Math.Sin(step * i));
            }

            return array;
        }

        private static sbyte[] Generate8BitTone(double frequency, uint sampleRate, double seconds)
        {
            const int amplitude = 127;
            uint numSamples = (uint)(sampleRate * seconds);
            sbyte[] array = new sbyte[numSamples];
            double step = (Math.PI * 2 * frequency) / sampleRate;

            for (int i = 0; i < numSamples; i++)
            {
                array[i] = Convert.ToSByte(amplitude * Math.Sin(step * i));
            }

            return array;
        }
    }
}
