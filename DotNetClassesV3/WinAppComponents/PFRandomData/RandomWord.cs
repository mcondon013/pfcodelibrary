//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PFRandomData
{
    /// <summary>
    /// Class for the generation of random words.
    /// </summary>
    public class RandomWord
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        private RandomNumber _rn = new RandomNumber();
        private int _maxWordsArrayInx = 0;

        private string[] _words = { "" };
        WordDataArrays _wordArrays = new WordDataArrays();
        NameDataArrays _nameArrays = new NameDataArrays();
        LocationDataArrays _locationArrays = new LocationDataArrays();

        //private variables for properties
        private enWordType _wordType = enWordType.NotSpecified;

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomWord()
        {
            InitInstance(enWordType.NotSpecified);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordType">Type of word to generate with this instance (e.g. noun, verb, adjective, etc.).</param>
        public RandomWord(enWordType wordType)
        {
            InitInstance(wordType);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordType">Type of word to generate with this instance (e.g. noun, verb, adjective, etc.).</param>
        /// <param name="customWordsFile">Full path to the file containg the word values.</param>
        public RandomWord(enWordType wordType, string customWordsFile)
        {
            LoadWordsFromFile(wordType, customWordsFile);
        }

        private void InitInstance(enWordType wordType)
        {
            _wordType = wordType;

            switch (wordType)
            {
                case enWordType.NotSpecified:
                    _words = _wordArrays.Words; 
                    break;
                case enWordType.Noun:
                    _words = _wordArrays.Nouns;
                    break;
                case enWordType.Verb:
                    _words = _wordArrays.Verbs;
                    break;
                case enWordType.Adjective:
                    _words = _wordArrays.Adjectives;
                    break;
                case enWordType.Adverb:
                    _words = _wordArrays.Adverbs;
                    break;
                case enWordType.Pronoun:
                    _words = _wordArrays.Pronouns;
                    break;
                case enWordType.Determiner:
                    _words = _wordArrays.Determiners;
                    break;
                case enWordType.DeterminerPronoun:
                    _words = _wordArrays.DeterminerPronouns;
                    break;
                case enWordType.Preposition:
                    _words = _wordArrays.Prepositions;
                    break;
                case enWordType.Conjunction:
                    _words = _wordArrays.Conjunctions;
                    break;
                case enWordType.Interjection:
                    _words = _wordArrays.Interjections;
                    break;
                case enWordType.Interrogative:
                    _words = _wordArrays.Interrogatives;
                    break;
                case enWordType.SubordinateConjunction:
                    _words = _wordArrays.SubordinateConjunctions;
                    break;
                case enWordType.CityName:
                    _words = _wordArrays.CityNames;
                    break;
                case enWordType.StateName:
                    _words = _wordArrays.StateNames;
                    break;
                case enWordType.FirstName:
                    _words = _wordArrays.FirstNames;
                    break;
                case enWordType.FirstNameMale:
                    _words = _nameArrays.US_FirstNamesMale;
                    break;
                case enWordType.FirstNameFemale:
                    _words = _nameArrays.US_FirstNamesFemale;
                    break;
                case enWordType.LastName:
                    _words = _wordArrays.LastNames;
                    break;
                case enWordType.BusinessName:
                    _words = _wordArrays.Biz_Names;
                    break;
                case enWordType.BizName3Con_1:
                    _words = _wordArrays.Biz_3Con1;
                    break;
                case enWordType.BizName3Con_2:
                    _words = _wordArrays.Biz_3Con2;
                    break;
                case enWordType.BizNameSyllable_1:
                    _words = _wordArrays.Biz_Syl1;
                    break;
                case enWordType.BizNameSyllable_2:
                    _words = _wordArrays.Biz_Syl2;
                    break;
                case enWordType.BizNameSuffix:
                    _words = _wordArrays.Biz_Suffix;
                    break;
                case enWordType.BizNamePrefix:
                    _words = _wordArrays.Biz_Prefix;
                    break;
                case enWordType.SubjectPronoun:
                    _words = _wordArrays.SubjectPronouns;
                    break;
                case enWordType.ObjectPronoun:
                    _words = _wordArrays.ObjectPronouns;
                    break;
                default:
                    _words = _wordArrays.Words; 
                    break;
            }
            _maxWordsArrayInx = _words.Length - 1;
        }

        //properties

        /// <summary>
        /// WordType Property.
        /// </summary>
        public enWordType WordType
        {
            get
            {
                return _wordType;
            }
            set
            {
                //reinitialize instance to specified word type
                InitInstance(value);
            }
        }


        //methods

        /// <summary>
        /// Method to load a set of custom words to the current word type represented by this instance.
        /// </summary>
        /// <param name="wordType">Type of word to generate with this instance (e.g. noun, verb, adjective, etc.).</param>
        /// <param name="filename">Full path to the file containg the word values.</param>
        /// <remarks>File needs to be in text format with one word per line in the file.</remarks>
        public void LoadWordsFromFile(enWordType wordType, string filename)
        {
            _wordType = wordType;
            _words = File.ReadAllLines(filename);
            _maxWordsArrayInx = _words.Length - 1;
        }

        /// <summary>
        /// Method to load a set of custom words to the current word type represented by this instance.
        /// </summary>
        /// <param name="wordType">Type of word to generate with this instance (e.g. noun, verb, adjective, etc.).</param>
        /// <param name="filename">Full path to the file containg the word values.</param>
        /// <param name="textEncoding">Supply a System.Text.Encoding enum value if .NET file routines have trouble reading your files. This will often be needed when incoming data has accents on individual letters.</param>
        /// <remarks>File needs to be in text format with one word per line in the file.</remarks>
        public void LoadWordsFromFile(enWordType wordType, string filename, System.Text.Encoding textEncoding)
        {
            _wordType = wordType;
            _words = File.ReadAllLines(filename, textEncoding);
            _maxWordsArrayInx = _words.Length - 1;
        }

        /// <summary>
        /// Routine to generate a random word.
        /// </summary>
        /// <returns>String containing the generated word.</returns>
        public string GetWord()
        {
            return _words[_rn.GenerateRandomInt(0, _maxWordsArrayInx)];
        }


    }//end class
}//end namespace
