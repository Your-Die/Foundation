using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Chinchillada.Foundation
{
    [Serializable]
    public class RegexMatcher
    {
        [SerializeField] private string pattern;

        private Regex regex;

        private void EnsureRegex()
        {
            if (this.regex == null) 
                this.regex = new Regex(this.pattern);
        }

        public MatchCollection Match(string text)
        {
            this.EnsureRegex();
            return this.regex.Matches(text);
        }
    }
}