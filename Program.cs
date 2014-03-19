using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace あの言語
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1)
                {
                    if (File.Exists(args[0]))
                    {
                        StreamReader sr = new StreamReader(args[0]);
                        Console.Write(Run(sr.ReadToEnd()));
                    }
                }
            }
            else
            {
                Console.WriteLine("あの言語 ver0.01\n\n何も指定されていません。");
            }
            Console.ReadKey();
        }
        static private string Run(string Source)
        {
            int readIndex = 0;
            byte[] Memory = new byte[10];
            int Pointer = 0;
            string result = "";
            do
            {
                string p = Source.Substring(readIndex, 1);
                switch (p)
                {
                    case "あ":
                        Memory[Pointer]++;
                        break;
                    case "の":
                        Pointer++;
                        break;
                    case "…":
                        if (Memory[Pointer] == 0)
                        {
                            int Index = 0;
                            readIndex++;
                            for (; readIndex < Source.Length; readIndex++)
                            {
                                if (Source[readIndex] == '。')
                                    if (Index == 0)
                                        break;
                                    else
                                        Index--;
                                if (Source[readIndex] == '…')
                                    Index++;
                            }
                        }
                        break;
                    case "。":
                        if (Memory[Pointer] != 0)
                        {
                            int Index = 0;
                            readIndex--;
                            for (; readIndex >= 0; readIndex--)
                            {
                                if (Source[readIndex] == '…')
                                    if (Index == 0)
                                        break;
                                    else
                                        Index--;
                                if (Source[readIndex] == '。')
                                    Index++;
                            }
                        }
                        break;
                    case "　":
                        result += Encoding.ASCII.GetString(Memory, Pointer, 1);
                        break;
                    default:
                        break;
                }
                if (Pointer >= 10)
                    Pointer = 0;
                if (Pointer < 0)
                    Pointer = 9;
                readIndex++;
            } while (Source.Length > readIndex);
            return result;
        }
    }
}
