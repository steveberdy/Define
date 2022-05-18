using System;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net.Http;
using Newtonsoft.Json;

namespace Define
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rootCommand = new RootCommand("Command-line dictionary interface")
            {
                new Argument<string>("word", "Word to look up in the dictionary")
            };
            rootCommand.Handler = CommandHandler.Create<string>(async (word) =>
            {
                var response = await new HttpClient().GetAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}").ConfigureAwait(false);
                var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var definitions = JsonConvert.DeserializeObject<DictionaryResponse[]>(text);

                foreach (var def in definitions)
                {
                    Console.Write("\"" + def.Word + "\" ");
                    Console.WriteLine(string.IsNullOrEmpty(def.Phonetic) ? string.Join(" ", def.Phonetics.Select(x => x.Text).Where(x => !string.IsNullOrEmpty(x))) : def.Phonetic);

                    foreach (var meaning in def.Meanings)
                    {
                        Console.WriteLine("  " + meaning.PartOfSpeech);
                        foreach (var definition in meaning.Definitions)
                        {
                            Console.WriteLine("    " + definition.Definition);
                            if (!string.IsNullOrEmpty(definition.Example)) Console.WriteLine("      i.e   \"" + definition.Example + "\"");
                            if (definition.Synonyms.Any()) Console.WriteLine("      syn - " + string.Join(", ", definition.Synonyms));
                            if (definition.Antonyms.Any()) Console.WriteLine("      ant - " + string.Join(", ", definition.Antonyms));
                        }
                    }
                    Console.WriteLine();
                }
            });
            rootCommand.Invoke(args);
        }
    }
}