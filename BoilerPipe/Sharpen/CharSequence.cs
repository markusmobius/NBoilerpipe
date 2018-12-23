namespace Sharpen
{
    public class CharSequence
    {
        public static implicit operator CharSequence(string str)
        {
            return new StringCharSequence(str);
        }

        public static implicit operator CharSequence(System.Text.StringBuilder str)
        {
            return new StringCharSequence(str.ToString());
        }
    }

    public class StringCharSequence : CharSequence
    {
        private string str;

        public StringCharSequence(string str)
        {
            this.str = str;
        }

        public override string ToString()
        {
            return str;
        }
    }
}