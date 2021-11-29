using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LanguageExt.ClassInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using LemmaSharp;
using Iveonik.Stemmers;
using System.Threading.Tasks;
using LemmaSharp;

namespace Vocabulary_System_Se307_
{
    class Grams
    {
        private string name;

        public Grams(string name)
        {
            this.name = name;
        }

        List<string> uniList = new List<string>();
        List<string> uniOut = new List<string>();

        List<string> biList = new List<string>();
        List<string> biOut = new List<string>();

        List<string> triList = new List<string>();
        List<string> triOut = new List<string>();

        public void Unigrams_(string[] uni) // UniGram Method.
        {
            
            for (int i = 0; i < uni.Length; i++)
            {
                Console.WriteLine(uni[i] + "-");
                uniList.Add(uni[i]);
                
            }
            
        }
        public void UniFile() // TO STORE RESULT OF PROCESS UNIGRAM
        {
            string uniFile = @"C:\Users\GuvenSari\Desktop\demo\Unigram_lemmatized_VOCABULARY.txt";
            foreach(string s in uniList)
            {
                uniOut.Add(s.ToString());

            }
            File.WriteAllLines(uniFile,uniOut);
            
        }


        public void Bigrams_(string[] bi) // BiGram Method.
        {
            for (int i = 1; i <= 51; i++)
            {
                Console.WriteLine(bi[i - 1] + " - " + bi[i]);
                biList.Add(bi[i - 1]);
            }

        }
        public void BiFile() // TO STORE RESULT OF PROCESS BIGRAM
        {
            string biFile = @"C:\Users\GuvenSari\Desktop\demo\Bigram_lemmatized_VOCABULARY.txt";
            foreach (string s in biList)
            {
                biOut.Add(s.ToString());

            }
            
            File.WriteAllLines(biFile, biOut);
        }
        public void Trigrams_(string[] tri) //TriGram Method.
        {
            for (int i = 2; i <= 51; i++)
            {
                Console.WriteLine(tri[i - 2] + " - " + tri[i - 1] + " - " + tri[i]);
                triList.Add(tri[i-2]);
            }


        }
        public void TriFile()  // TO STORE RESULT OF PROCESS TRIGRAM
        {
            string triFile = @"C:\Users\GuvenSari\Desktop\demo\Trigram_lemmatized_VOCABULARY.txt";
            foreach (string s in triList)
            {
                triOut.Add(s.ToString());

            }
            
            File.WriteAllLines(triFile, triOut); 
            
        }

        public void TestStemmer(IStemmer stemmer, string[] words) //Lemmatizer method.
        {
            Console.WriteLine("Stemmer: " + stemmer);
            Console.WriteLine("Stemming words ...");
            Console.WriteLine(" ");
            foreach (string word in words)
            {
                Console.WriteLine(word + " --> " + stemmer.Stem(word));
            }
        }
        //public void Lemmatizer(ILemmatizer lmtz, string[] word) { 
        //    for(int i=0; i<word.Length; i++)
        //    {
        //        word[i]= lmtz.Lemmatize(word[i]);

        //    }
        //}

        public string ExtractTextFromPdf(string path) //Extract text on PDF ....
        {
            using (PdfReader reader = new PdfReader(path))
            {

                StringBuilder text = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to VocabularyApplication +(Press a key to continue App...)");
            Console.ReadKey();

            Grams vocabulary = new Grams("Grams for text");

            Console.WriteLine(" -> Reading PDF <-");
            Console.WriteLine("----------------------------");
            var ExtractedPDFToString = vocabulary.ExtractTextFromPdf(@"C:\Users\GuvenSari\Desktop\deneme-dönüştürüldü.pdf"); // Reading pdf
            Console.WriteLine(ExtractedPDFToString);

            TimeSpan ts = TimeSpan.FromSeconds(5);


            Console.WriteLine(" -> Removing Punctuation <- ");
            Console.WriteLine("----------------------------");
            ExtractedPDFToString = Regex.Replace(ExtractedPDFToString, @"[^\w\d\s]", "");// Remove punctuation...
            Console.WriteLine(ExtractedPDFToString);


            Console.WriteLine(" ->Converting Lower Case <- ");
            Console.WriteLine("----------------------------");
            var tolowercase = ExtractedPDFToString.ToLower();  // Lower Case 
            Console.WriteLine(tolowercase);


            string text1 = Convert.ToString(tolowercase);
            //string[] words = text1.Split(' ');    
            string[] words = text1.Split(' ', StringSplitOptions.RemoveEmptyEntries); // Assigning words to array one by one


            Console.WriteLine(" +Press a key to check contain Duplicates");

            Console.ReadKey();

            if (words.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                Console.WriteLine("Contains duplicate");
              
            }
            else
            {
                Console.WriteLine("No duplicate");
            }
                
            Console.WriteLine("Press a key to Remove Duplicates :");
            Console.ReadKey();
            Console.Write("-----------------------");

            string[] duplicates = words.Distinct().ToArray(); //REMOVE DUPLİCATES
            for(int i=0; i<duplicates.Length; i++) 
            {
                try
                {
                    Console.WriteLine(duplicates[i]);
                }

                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    throw new ArgumentOutOfRangeException("index parameter out of range", e);
                }

            }

            Console.WriteLine(" +Press a key to start Stemming...");
            Console.ReadKey();

            vocabulary.TestStemmer(new EnglishStemmer(),duplicates); //Stemmer or Lemmatizer

            //ILemmatizer lmtz = new ILemmatizer.PreBuildCompact();
            //vocabulary.Lemmatizer(duplicates);


            Console.WriteLine("----------------------------");

            Console.WriteLine("Please enter what you want to choose :");
            Console.WriteLine("1 -> Unigrams");
            Console.WriteLine("2 -> Bigrams");
            Console.WriteLine("3 -> Trigrams");

            var input_ = Console.ReadLine();
            int input = Convert.ToInt32(input_);


            switch (input)
            {
                case 1:
                    Console.WriteLine("-----> UNİGRAMS <-----");
                    vocabulary.Unigrams_(duplicates);
                    Console.WriteLine("-----> UNİGRAMS END <-----");
                    Console.WriteLine(" ");
                    vocabulary.UniFile();
                    break;

                case 2:
                    Console.WriteLine("-----> BIGRAMS <-----");
                    vocabulary.Bigrams_(duplicates);
                    Console.WriteLine("-----> BIGRAMS END <-----");
                    Console.WriteLine(" ");
                    vocabulary.BiFile();
                    break;

                case 3:
                    Console.WriteLine("-----> TRIGRAMS <-----");
                    vocabulary.Trigrams_(duplicates);
                    Console.WriteLine("-----> TRIGRAMS END <-----");
                    Console.WriteLine(" ");
                    vocabulary.TriFile();
                    break;           
            }


            Console.WriteLine("Please Enter 9 if you want to open Vocabulary text file");
            var stats = Console.ReadLine();
            int statistics = Convert.ToInt32(stats);

            while (statistics == 9)
            {
                Console.WriteLine();

                Console.WriteLine
                ("If you want to see the statistics" +
                "Enter 1 to UNIGRAM." +
                "Enter 2 to BIGRAM." +
                "ENTER 3 TO TRIGRAM.");

                var num = Console.ReadLine();
                int number = Convert.ToInt32(num);

              

                if (number == 1)
                {
                    int uni_count = 0;
                    string filePath1 = @"C:\Users\GuvenSari\Desktop\demo\Unigram_lemmatized_VOCABULARY.txt";

                    //string[] lines = File.ReadAllLines(filePath);
                    List<string> lines = File.ReadAllLines(filePath1).ToList();

                    //foreach (string line in lines)
                    //{
                    //    Console.WriteLine(line);
                    //}
                    for (int i = 0; i <lines.Count; i++)
                    {
                        Console.WriteLine(lines[i]+"-");
                        uni_count++;
                    }
                    Console.WriteLine("Number of UNIGRAMS --> " + uni_count + " ");
                    return;

                }
                else if (number == 2)
                {
                    int bi_count = 0;
                    string filePath2 = @"C:\Users\GuvenSari\Desktop\demo\Bigram_lemmatized_VOCABULARY.txt";

                    List<string> lines = File.ReadAllLines(filePath2).ToList();

                    //foreach (string s in lines)
                    //{
                    //    Console.WriteLine(s);
                    //}
                    for(int i=1; i<lines.Count; i++)
                    {
                        Console.WriteLine(lines[i-1] +"-"+ lines[i]);
                        bi_count++;
                    }
                    Console.WriteLine("Number of BIGRAMS --> " + (bi_count -1) + " ");
                    return;
                }
                else if (number == 3)
                {
                    int tri_count = 0;
                    string filePath3 = @"C:\Users\GuvenSari\Desktop\demo\Trigram_lemmatized_VOCABULARY.txt";

                    List<string> lines = File.ReadAllLines(filePath3).ToList();

                    //foreach (string s in lines)
                    //{
                    //    Console.WriteLine(s);
                    //}
                    for (int i = 2; i < lines.Count; i++)
                    {
                        Console.WriteLine(lines[i - 2] + "-" + lines[i-2]+"-"+lines[i]);
                        tri_count++;
                    
                    }
                    Console.WriteLine("Number of TRIGRAMS --> " + (tri_count-2) + " ");
                    return;
                }

                Console.ReadKey();
            }

            Console.ReadKey();

           
        } // end of main
        


        public static IEnumerable<string> makeNgrams(string text,int nGramsize)
        {
            StringBuilder nGram = new StringBuilder();
            Queue<int> wordLengths = new Queue<int>();
            int wordCount = 0;
            int lastWordLength = 0;
            
            //append the first character, if valid
            //avoids if statement for each for loop to check i==0 for before and after vars.
            if(text !="" && char.IsLetterOrDigit(text[0]))
            {
                nGram.Append(text[0]);
                lastWordLength++;
            }
            //generate ngrams
            for(int i = 0; i<text.Length; i++)
            {
                char before = text[i - 1];
                char after = text[i + 1];

                if (char.IsLetterOrDigit(text[i])
                    || (text[i] != ' '&&(char.IsSeparator(text[i]) || char.IsPunctuation(text[i]))
                    && (char.IsLetterOrDigit(before) && char.IsLetterOrDigit(after))
                        )
                    )
                {
                    nGram.Append(text[i]);
                    lastWordLength++;
                }
                else
                {
                    if(lastWordLength > 0)
                    {
                        wordLengths.Enqueue(lastWordLength);
                        lastWordLength = 0;
                        wordCount++;
                        if(wordCount >= nGramsize)
                        {
                            yield return nGram.ToString();
                            nGram.Remove(0, wordLengths.Dequeue() + 1);
                            wordCount -= 1;

                        }
                        nGram.Append(" ");
                    }
                }
            }
        }

    }
}

