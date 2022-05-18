using System;

namespace Define
{
    public class DictionaryResponse
    {
        public string Word { get; set; }
        public string Phonetic { get; set; }
        public ResponsePhonetic[] Phonetics { get; set; }
        /// <summary>Meanings as different parts of speech</summary>
        public ResponseMeaning[] Meanings { get; set; }
        public ResponseLicense License { get; set; }
        public string[] SourceUrls { get; set; }
    }

    public class ResponsePhonetic
    {
        public string Text { get; set; }
        public string Audio { get; set; }
        public string SourceUrl { get; set; }
        public ResponseLicense License { get; set; }
    }

    public class ResponseMeaning
    {
        public string PartOfSpeech { get; set; }
        public ResponseDefinition[] Definitions { get; set; }
        public string[] Synonyms { get; set; }
        public string[] Antonyms { get; set; }
    }

    public class ResponseLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class ResponseDefinition
    {
        public string Definition { get; set; }
        public string[] Synonyms { get; set; }
        public string[] Antonyms { get; set; }
        public string Example { get; set; }
    }
}