using System;

namespace BreadersHomebook
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {
        }
    }
}