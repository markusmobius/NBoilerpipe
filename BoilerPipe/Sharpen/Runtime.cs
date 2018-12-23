using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Sharpen
{
    public class Runtime
    {
        private static Runtime instance;

        public int AvailableProcessors()
        {
            return Environment.ProcessorCount;
        }

        public static long CurrentTimeMillis()
        {
            return DateTime.UtcNow.ToMillisecondsSinceEpoch();
        }



        public static void SetProperty(string key, string value)
        {
            //			throw new NotImplementedException ();
        }

        public static Runtime GetRuntime()
        {
            if (instance == null)
            {
                instance = new Runtime();
            }
            return instance;
        }

        public static int IdentityHashCode(object ob)
        {
            return RuntimeHelpers.GetHashCode(ob);
        }

        public long MaxMemory()
        {
            return int.MaxValue;
        }

        public static byte[] GetBytesForString(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static byte[] GetBytesForString(string str, string encoding)
        {
            return Encoding.GetEncoding(encoding).GetBytes(str);
        }

  


        public static void PrintStackTrace(Exception ex, TextWriter tw)
        {
            tw.WriteLine(ex);
        }

        public static string Substring(string str, int index)
        {
            return str.Substring(index);
        }

        public static string Substring(string str, int index, int endIndex)
        {
            return str.Substring(index, endIndex - index);
        }

        public static void Wait(object ob)
        {
            Monitor.Wait(ob);
        }

        public static bool Wait(object ob, long milis)
        {
            return Monitor.Wait(ob, (int)milis);
        }


        public static void SetCharAt(StringBuilder sb, int index, char c)
        {
            sb[index] = c;
        }

        public static bool EqualsIgnoreCase(string s1, string s2)
        {
            return s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
        }

        public static long NanoTime()
        {
            return Environment.TickCount * 1000 * 1000;
        }

        public static int CompareOrdinal(string s1, string s2)
        {
            return string.CompareOrdinal(s1, s2);
        }

  
        public static string GetStringForBytes(byte[] chars, int start, int len, string encoding)
        {
            return GetEncoding(encoding).Decode(chars, start, len);
        }

        public static Encoding GetEncoding(string name)
        {
            //			Encoding e = Encoding.GetEncoding (name, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
            Encoding e = Encoding.GetEncoding(name.Replace('_', '-'));
            if (e is UTF8Encoding)
                return new UTF8Encoding(false, true);
            return e;
        }
    }
}