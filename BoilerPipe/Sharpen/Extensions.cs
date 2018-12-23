namespace Sharpen
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Extensions
    {
        private static readonly long EPOCH_TICKS;


        
        public static void Add<T> (this IList<T> list, int index, T item)
        {
            list.Insert (index, item);
        }


        
        public static string Decode (this Encoding e, byte[] chars, int start, int len)
        {
            try {
                byte[] bom = e.GetPreamble ();
                if (bom != null && bom.Length > 0) {
                    if (len >= bom.Length) {
                        int pos = start;
                        bool hasBom = true;
                        for (int n=0; n<bom.Length && hasBom; n++) {
                            if (bom[n] != chars [pos++])
                                hasBom = false;
                        }
                        if (hasBom) {
                            len -= pos - start;
                            start = pos;
                        }
                    }
                }
                return e.GetString (chars, start, len);
            } catch (DecoderFallbackException) {
                throw new CharacterCodingException ();
            }
        }
        
      /*  public static string Decode (this Encoding e, ByteBuffer buffer)
        {
            return e.Decode (buffer.Array (), buffer.ArrayOffset () + buffer.Position (), buffer.Limit () - buffer.Position ());
        }*/

       
        public static bool AddItem<T> (this IList<T> list, T item)
        {
            list.Add (item);
            return true;
        }

        public static bool AddItem<T> (this ICollection<T> list, T item)
        {
            list.Add (item);
            return true;
        }

        public static U Get<T, U> (this IDictionary<T, U> d, T key)
        {
            U val;
            d.TryGetValue (key, out val);
            return val;
        }

       

        public static T GetLast<T> (this IList<T> list)
        {
            return ((list.Count == 0) ? default(T) : list[list.Count - 1]);
        }

       
        
        public static bool IsEmpty<T> (this ICollection<T> col)
        {
            return (col.Count == 0);
        }
        

        public static Sharpen.Iterator<T> Iterator<T> (this ICollection<T> col)
        {
            return new EnumeratorWrapper<T> (col, col.GetEnumerator ());
        }

        

        public static T Last<T> (this ICollection<T> col)
        {
            IList<T> list = col as IList<T>;
            if (list != null) {
                return list [list.Count - 1];
            }
            return col.Last<T> ();
        }
        
        public static ListIterator<T> ListIterator<T> (this IList<T> col)
        {
            return new ListIterator<T> (col, 0);
        }
        
        public static ListIterator<T> ListIterator<T> (this IList<T> col, int n)
        {
            return new ListIterator<T> (col, n);
        }

        
        public static T Remove<T> (this IList<T> list, int i)
        {
            T old;
            try {
                old = list[i];
                list.RemoveAt (i);
            } catch (IndexOutOfRangeException) {
                throw new NoSuchElementException ();
            }
            return old;
        }

        public static T RemoveFirst<T> (this IList<T> list)
        {
            return list.Remove<T> (0);
        }
        
        public static T RemoveLast<T> (this IList<T> list)
        {
            return list.Remove<T> (list.Count - 1);			
        }
        
        public static string ReplaceAll (this string str, string regex, string replacement)
        {
            Regex rgx = new Regex (regex);
            
            if (replacement.IndexOfAny (new char[] { '\\','$' }) != -1) {
                // Back references not yet supported
                StringBuilder sb = new StringBuilder ();
                for (int n=0; n<replacement.Length; n++) {
                    char c = replacement [n];
                    if (c == '$')
                        throw new NotSupportedException ("Back references not supported");
                    if (c == '\\')
                        c = replacement [++n];
                    sb.Append (c);
                }
                replacement = sb.ToString ();
            }
            
            return rgx.Replace (str, replacement);
        }
        
        

        public static string[] Split (this string str, string regex)
        {
            return str.Split (regex, 0);
        }
        
        public static string[] Split (this string str, string regex, int limit)
        {
            Regex rgx = new Regex (regex);
            List<string> list = new List<string> ();
            int startIndex = 0;
            if (limit != 1) {
                int nm = 1;
                foreach (Match match in rgx.Matches (str)) {
                    list.Add (str.Substring (startIndex, match.Index - startIndex));
                    startIndex = match.Index + match.Length;
                    if (limit > 0 && ++nm == limit)
                        break;
                }
            }
            if (startIndex < str.Length) {
                list.Add (str.Substring (startIndex));
            }
            if (limit >= 0) {
                int count = list.Count - 1;
                while ((count >= 0) && (list[count].Length == 0)) {
                    count--;
                }
                list.RemoveRange (count + 1, (list.Count - count) - 1);
            }
            return list.ToArray ();
        }

        public static IList<T> SubList<T> (this IList<T> list, int start, int len)
        {
            List<T> sublist = new List<T> (len);
            for (int i = start; i < (start + len) && i < list.Count; i++) {
                sublist.Add (list [i - start]);
            }
            return sublist;
        }
        
        public static bool StartsWith (this string str, string prefix, int offset)
        {
            return str.Substring (offset).StartsWith (prefix);
        }
        
        public static CharSequence SubSequence (this string str, int start, int end)
        {
            return (CharSequence)str.Substring (start, end);
        }

        

        public static long ToMillisecondsSinceEpoch (this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc) {
                throw new ArgumentException ("dateTime is expected to be expressed as a UTC DateTime", "dateTime");
            }
            return new DateTimeOffset (DateTime.SpecifyKind (dateTime, DateTimeKind.Utc), TimeSpan.Zero).ToMillisecondsSinceEpoch ();
        }

        public static long ToMillisecondsSinceEpoch (this DateTimeOffset dateTimeOffset)
        {
            return (((dateTimeOffset.Ticks - dateTimeOffset.Offset.Ticks) - EPOCH_TICKS) / TimeSpan.TicksPerMillisecond);
        }

        
    }
}
