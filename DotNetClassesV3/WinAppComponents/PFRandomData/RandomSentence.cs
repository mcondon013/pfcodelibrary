//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Contains routines for generating sentences containing random words organized in correct syntax.
    /// </summary>
    public class RandomSentence
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        WordDataArrays _wda = new WordDataArrays();
        RandomNumber _rn = new RandomNumber();

        private string[] _sentenceSyntaxList = new string[] {""};
        private string[] _subordinateClauseList = new string[] {""};
        private string[] _nounPhraseSyntaxList = new string[] {""};
        private string[] _subjectSyntaxList = new string[] {""};
        private string[] _verbPhraseSyntaxList = new string[] {""};
        private string[] _objectSyntaxList = new string[] {""};
        private string[] _sentenceTerminatorList = new string[] {""};
        private string[] _thirdPersonSingularPronouns = new string[] { "" };
        private string[] _auxiliaryVerbs = new string[] { "" };
        private string[] _letterIsVowel = new string[] { "" };

        private RandomWord[] _randomWordGenerators = new RandomWord[Enum.GetNames(typeof(enWordType)).Length];

        
        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomSentence()
        {
            InitSyntaxLists();
            LoadRandomWordGenerators();
        }

        private void InitSyntaxLists()
        {
            _sentenceSyntaxList = _wda.SentenceSyntaxDefs;
            _subordinateClauseList = _wda.SubordinateClauseDefs;
            _nounPhraseSyntaxList = _wda.NounPhraseSyntaxDefs;
            _subjectSyntaxList = _wda.SubjectSyntaxDefs;
            _verbPhraseSyntaxList = _wda.VerbPhraseSyntaxDefs;
            _objectSyntaxList = _wda.ObjectSyntaxDefs;
            _sentenceTerminatorList = _wda.SentenceTerminatorDefs;
            _thirdPersonSingularPronouns = _wda.ThirdPersonSingularPronouns;
            _auxiliaryVerbs = _wda.AuxiliaryVerbs;
            _letterIsVowel = _wda.LetterIsVowel;
        }

        private void LoadRandomWordGenerators()
        {
            _randomWordGenerators[(int)enWordType.NotSpecified] = new RandomWord(enWordType.NotSpecified);
            _randomWordGenerators[(int)enWordType.Noun] = new RandomWord(enWordType.Noun);
            _randomWordGenerators[(int)enWordType.Verb] = new RandomWord(enWordType.Verb);

            _randomWordGenerators[(int)enWordType.Adjective] = new RandomWord(enWordType.Adjective);
            _randomWordGenerators[(int)enWordType.Adverb] = new RandomWord(enWordType.Adverb);
            _randomWordGenerators[(int)enWordType.Pronoun] = new RandomWord(enWordType.Pronoun);
            _randomWordGenerators[(int)enWordType.Determiner] = new RandomWord(enWordType.Determiner);
            _randomWordGenerators[(int)enWordType.DeterminerPronoun] = new RandomWord(enWordType.DeterminerPronoun);
            _randomWordGenerators[(int)enWordType.Preposition] = new RandomWord(enWordType.Preposition);
            _randomWordGenerators[(int)enWordType.Conjunction] = new RandomWord(enWordType.Conjunction);
            _randomWordGenerators[(int)enWordType.Interjection] = new RandomWord(enWordType.Interjection);
            _randomWordGenerators[(int)enWordType.Interrogative] = new RandomWord(enWordType.Interrogative);
            _randomWordGenerators[(int)enWordType.SubordinateConjunction] = new RandomWord(enWordType.SubordinateConjunction);
            _randomWordGenerators[(int)enWordType.CityName] = new RandomWord(enWordType.CityName);
            _randomWordGenerators[(int)enWordType.StateName] = new RandomWord(enWordType.StateName);
            _randomWordGenerators[(int)enWordType.FirstName] = new RandomWord(enWordType.FirstName);
            _randomWordGenerators[(int)enWordType.FirstNameMale] = new RandomWord(enWordType.FirstNameMale);
            _randomWordGenerators[(int)enWordType.FirstNameFemale] = new RandomWord(enWordType.FirstNameFemale);
            _randomWordGenerators[(int)enWordType.LastName] = new RandomWord(enWordType.LastName);
            _randomWordGenerators[(int)enWordType.BusinessName] = new RandomWord(enWordType.BusinessName);
            _randomWordGenerators[(int)enWordType.BizName3Con_1] = new RandomWord(enWordType.BizName3Con_1);
            _randomWordGenerators[(int)enWordType.BizName3Con_2] = new RandomWord(enWordType.BizName3Con_2);
            _randomWordGenerators[(int)enWordType.BizNameSyllable_1] = new RandomWord(enWordType.BizNameSyllable_1);
            _randomWordGenerators[(int)enWordType.BizNameSyllable_2] = new RandomWord(enWordType.BizNameSyllable_2);
            _randomWordGenerators[(int)enWordType.BizNameSuffix] = new RandomWord(enWordType.BizNameSuffix);
            _randomWordGenerators[(int)enWordType.BizNamePrefix] = new RandomWord(enWordType.BizNamePrefix);
            _randomWordGenerators[(int)enWordType.SubjectPronoun] = new RandomWord(enWordType.SubjectPronoun);
            _randomWordGenerators[(int)enWordType.ObjectPronoun] = new RandomWord(enWordType.ObjectPronoun);

        }


        //properties

        //methods
        /// <summary>
        /// Produces a sentence using random words arranged in a syntactical order.
        /// </summary>
        /// <returns>String containing random sentence.</returns>
        public string GenerateSentence()
        {
            StringBuilder sentence = new StringBuilder();

            sentence.Append(GenerateSentences((int)1));

            return sentence.ToString();
        }

        /// <summary>
        /// Routine to generate one or more sentences containing random words.
        /// </summary>
        /// <param name="numSentences">Number of sentences to generate.</param>
        /// <returns>String containing generated sentences.</returns>
        public string GenerateSentences(int numSentences)
        {
            StringBuilder sentence = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;
            string terminator = string.Empty;

            for (int s = 0; s < numSentences; s++)
            {
                rndMin = 0;
                rndMax = _sentenceSyntaxList.Length - 1;
                rndNum = _rn.GenerateRandomInt(rndMin, rndMax);

                string sentenceSyntax = _sentenceSyntaxList[rndNum];
                string newSentenceFragment = BuildSentenceString(sentenceSyntax, ref terminator);
                newSentenceFragment = newSentenceFragment.First().ToString().ToUpper() + String.Join("", newSentenceFragment.Skip(1));
                while (newSentenceFragment.EndsWith("  "))
                {
                    newSentenceFragment = newSentenceFragment.Replace("  ", " ");
                }
                sentence.Append(newSentenceFragment);
            }

            return sentence.ToString();
        }

        private string BuildSentenceString(string sentenceSyntaxDef, ref string prevSentenceTerminator)
        {
            StringBuilder sentence = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;
            bool isDeclarativeSentence = false;
            bool isRunOnSentence = false;
            bool conjunctionSpecified = false;
            string terminator = " ";
            string prevTerminator = string.Empty;
            bool useThirdPersonSingular = false;

            sentence.Length = 0;

            isDeclarativeSentence = true;
            conjunctionSpecified = false;
            string[] sentenceSyntax = sentenceSyntaxDef.Split(' ');
            useThirdPersonSingular = true;
            for (int i = 0; i < sentenceSyntax.Length; i++)
            {

                switch (sentenceSyntax[i])
                {
                    case "subject":
                        sentence.Append(BuildSubjectString(ref useThirdPersonSingular));
                        break;
                    case "verb":
                        sentence.Append(BuildVerbPhrase(useThirdPersonSingular));
                        break;
                    case "object":
                        sentence.Append(BuildObjectString());
                        break;
                    case "conjunction":
                        if (sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(" ");
                        sentence.Append(_randomWordGenerators[(int)enWordType.Conjunction].GetWord().Replace("_", " "));
                        sentence.Append(" ");
                        conjunctionSpecified = true;
                        break;
                    case "subordinateclause":
                        if (i > 0 && sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(" ");
                        sentence.Append(BuildSubordinateClause(ref useThirdPersonSingular));
                        if (i == 0 && sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(", ");
                        break;
                    case "interrogative":
                        sentence.Append(_randomWordGenerators[(int)enWordType.Interrogative].GetWord());
                        sentence.Append(" ");
                        isDeclarativeSentence = false;
                        break;
                    default:
                        sentence.Append("<<< sentence default >>>");
                        sentence.Append(" ");
                        break;
                }
            }


            if (conjunctionSpecified == false)
            {
                string temp = sentence.ToString().TrimEnd(' ');
                sentence.Length = 0;
                sentence.Append(temp);

                if (isDeclarativeSentence)
                {
                    rndMin = 0;
                    rndMax = _sentenceTerminatorList.Length - 1;
                    rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
                    terminator = _sentenceTerminatorList[rndNum];
                    //if (_testRunOnSentence)
                    //{
                    //    _testRunOnSentence = false;
                    //    terminator = ": ";
                    //}
                    if (terminator == "; " || terminator == ": ")
                        isRunOnSentence = true;
                    //terminator += " ";
                }
                else
                {
                    terminator = "? ";
                }
            }
            else
            {
                terminator = " ";
                rndMin = 0;
                rndMax = _sentenceSyntaxList.Length - 1;
                rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
                sentence.Append(BuildSentenceString(_sentenceSyntaxList[rndNum], ref terminator));
            }
            sentence.Append(terminator);

            if (isRunOnSentence)
            {
                rndMin = 0;
                rndMax = _sentenceSyntaxList.Length - 1;
                rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
                sentence.Append(BuildSentenceString(_sentenceSyntaxList[rndNum], ref terminator));
            }

            prevTerminator = terminator;

            return sentence.ToString();

        }

        private string BuildSubjectString(ref bool useThirdPersonSingular)
        {
            StringBuilder subject = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _subjectSyntaxList.Length - 1;
            rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
            //subject.Append("BuildSubjectString:");
            //subject.Append("\r\n\t");
            //subject.Append(_subjectSyntaxList[rndNum]);
            //subject.Append("\r\n\t");
            string subjectSyntax = _subjectSyntaxList[rndNum];

            useThirdPersonSingular = true;

            switch (subjectSyntax)
            {
                case "nounphrase":
                    subject.Append(BuildNounPhrase());
                    break;
                case "pronoun":
                    string pronoun = _randomWordGenerators[(int)enWordType.SubjectPronoun].GetWord();
                    subject.Append(pronoun);
                    if (PronounIsThirdPerson(pronoun) == false)
                        useThirdPersonSingular = false;
                    break;
                case "FirstName":
                    subject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    break;
                case "LastName":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "FirstName LastName":
                    subject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "LastName BusinessName":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "LastName BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BusinessName":
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNameSyllable1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Syllable1+BizNameSyllable2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Con1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Con1+BizNameCon2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                default:
                    subject.Append("<<< subject default >>>");
                    subject.Append(" ");
                    break;
            }

            string temp = subject.ToString();
            temp = temp.Replace("<CityName>", _randomWordGenerators[(int)enWordType.CityName].GetWord());
            temp = temp.Replace("<StateName>", _randomWordGenerators[(int)enWordType.StateName].GetWord());
            subject.Length = 0;
            subject.Append(temp);

            subject.Append(" ");

            return subject.ToString();
        }

        //routine is not used as of October 2014.
        private bool PronounIsThirdPerson(string pronoun)
        {
            bool ret = false;

            for (int i = 0; i < _thirdPersonSingularPronouns.Length; i++)
            {
                if (_thirdPersonSingularPronouns[i].ToLower() == pronoun.ToLower())
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private string BuildNounPhrase()
        {
            StringBuilder nounPhrase = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;
            string noun = string.Empty;
            string adjective = string.Empty;
            string determiner = string.Empty;
            string preposition = string.Empty;
            string determinerPronoun = string.Empty;

            rndMax = _nounPhraseSyntaxList.Length - 1;
            rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
            string nounPhraseSyntax = _nounPhraseSyntaxList[rndNum];

            noun = _randomWordGenerators[(int)enWordType.Noun].GetWord();
            adjective = _randomWordGenerators[(int)enWordType.Adjective].GetWord();
            determiner = _randomWordGenerators[(int)enWordType.Determiner].GetWord();
            //if (determiner == "an" && FirstLetterIsVowell(noun) == false)
            //    nounPhraseSyntax = "determiner noun";
            //if (determiner == "an" && FirstLetterIsVowell(adjective) == false)
            //    nounPhraseSyntax = "determiner adjective noun";
            bool nounFirstLetterIsVowell = FirstLetterIsVowell(noun);
            bool adjectiveFirstLetterIsVowell = FirstLetterIsVowell(adjective);
            preposition = _randomWordGenerators[(int)enWordType.Preposition].GetWord();
            determinerPronoun = _randomWordGenerators[(int)enWordType.DeterminerPronoun].GetWord();

            switch (nounPhraseSyntax)
            {
                case "noun":
                    nounPhrase.Append(noun);
                    break;
                case "preposition noun":
                    nounPhrase.Append(preposition);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "preposition determiner noun":
                    nounPhrase.Append(preposition);
                    nounPhrase.Append(" ");
                    if(determiner == "an" && nounFirstLetterIsVowell==false)
                        nounPhrase.Append("a");
                    else if (determiner == "a" && nounFirstLetterIsVowell)
                        nounPhrase.Append("an");
                    else
                        nounPhrase.Append(determiner);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "preposition determiner adjective noun":
                    nounPhrase.Append(preposition);
                    nounPhrase.Append(" ");
                    if(determiner == "an" && adjectiveFirstLetterIsVowell==false)
                        nounPhrase.Append("a");
                    else if (determiner == "a" && adjectiveFirstLetterIsVowell)
                        nounPhrase.Append("an");
                    else
                        nounPhrase.Append(determiner);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(adjective);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "determiner noun":
                    if(determiner == "an" && nounFirstLetterIsVowell==false)
                        nounPhrase.Append("a");
                    else if (determiner == "a" && nounFirstLetterIsVowell)
                        nounPhrase.Append("an");
                    else
                        nounPhrase.Append(determiner);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "determiner adjective noun":
                    if(determiner == "an" && adjectiveFirstLetterIsVowell==false)
                        nounPhrase.Append("a");
                    else if (determiner == "a" && adjectiveFirstLetterIsVowell)
                        nounPhrase.Append("an");
                    else
                        nounPhrase.Append(determiner);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(adjective);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "preposition determinerpronoun noun":
                    nounPhrase.Append(preposition);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(determinerPronoun);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "preposition determinerpronoun adjective noun":
                    nounPhrase.Append(preposition);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(determinerPronoun);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(adjective);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "determinerpronoun noun":
                    nounPhrase.Append(determinerPronoun);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "determinerpronoun adjective noun":
                    nounPhrase.Append(determinerPronoun);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(adjective);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                case "adjective noun":
                    nounPhrase.Append(adjective);
                    nounPhrase.Append(" ");
                    nounPhrase.Append(noun);
                    break;
                default:
                    nounPhrase.Append("<<< nounphrase default >>>");
                    nounPhrase.Append(" ");
                    break;
            }

            //nounPhrase.Append(" ");

            return nounPhrase.ToString();
        }

        private bool FirstLetterIsVowell(string word)
        {
            bool ret = false;
            if (String.IsNullOrEmpty(word))
            {
                return false;
            }

            string firstLetter = word.Substring(0, 1);

            for (int i = 0; i < _letterIsVowel.Length; i++)
            {
                if (_letterIsVowel[i] == firstLetter)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private string BuildVerbPhrase(bool useThirdPersonSingular)
        {
            StringBuilder verb = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _verbPhraseSyntaxList.Length - 1;
            rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
            string verbPhraseSyntax = _verbPhraseSyntaxList[rndNum];
            string randomVerb = _randomWordGenerators[(int)enWordType.Verb].GetWord();
            string modifiedVerb = randomVerb;
            if (WordIsAuxiliaryVerb(randomVerb) == true)
            {
                modifiedVerb = FixAuxiliaryVerb(randomVerb);
            }
            else if (useThirdPersonSingular && WordIsAuxiliaryVerb(randomVerb) == false)
            {
                modifiedVerb = FixThirdPersonVerb(randomVerb);
            }
            else
                modifiedVerb = randomVerb;

            switch (verbPhraseSyntax)
            {
                case "verb":
                    verb.Append(modifiedVerb);
                    break;
                case "verb adverb":
                    verb.Append(modifiedVerb);
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verb":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append(modifiedVerb);
                    break;
                case "verbpast":
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    break;
                case "verbpast adverb":
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbpast":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    break;
                case "verbfuture":
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will", "aim"));
                    break;
                case "verbfuture adverb":
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will", "aim"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbfuture":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will", "aim"));
                    break;
                case "verbconditional":
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    break;
                case "verbconditional adverb":
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbconditional":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    break;
                default:
                    verb.Append("<<< verb default >>>");
                    verb.Append(" ");
                    break;
            }

            verb.Append(" ");

            return verb.ToString();
        }

        private bool WordIsAuxiliaryVerb(string word)
        {
            bool ret = false;

            for (int i = 0; i < _auxiliaryVerbs.Length; i++)
            {
                if (_auxiliaryVerbs[i] == word)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private string FixAuxiliaryVerb(string verbToFix)
        {
            string ret = verbToFix;

            switch (verbToFix)
            {
                case "ought":
                    ret = verbToFix + " to " + GetNonAuxiliaryVerb(verbToFix);
                    break;
                default:
                    ret = verbToFix + " " + GetNonAuxiliaryVerb(verbToFix); ;
                    break;
            }


            return ret;
        }

        private string GetNonAuxiliaryVerb(string auxiliaryVerb)
        {
            string ret = string.Empty;
            string newVerb = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                newVerb = _randomWordGenerators[(int)enWordType.Verb].GetWord();
                if (WordIsAuxiliaryVerb(newVerb) == false)
                {
                    ret = newVerb;
                    break;
                }
            }

            if (ret == string.Empty)
            {
                ret = "translate into esperanto";
            }

            return ret;
        }

        private string FixThirdPersonVerb(string originalVerb)
        {
            string newVerb = originalVerb;

            switch (originalVerb)
            {
                case "be":
                    newVerb = "is";
                    break;
                case "have":
                    newVerb = "has";
                    break;
                case "do":
                    newVerb = "does";
                    break;
                case "can":
                case "could":
                case "would":
                case "should":
                case "shall":
                case "must":
                case "might":
                case "may":
                case "ought":
                    newVerb = originalVerb;
                    break;
                default:
                    if (originalVerb.EndsWith("s") || originalVerb.EndsWith("h") || originalVerb.EndsWith("x"))
                        newVerb = originalVerb + "es";
                    else if (originalVerb.EndsWith("y"))
                    {
                        if (originalVerb.EndsWith("ay")
                            || originalVerb.EndsWith("ey")
                            || originalVerb.EndsWith("iy")
                            || originalVerb.EndsWith("oy")
                            || originalVerb.EndsWith("uy"))
                            newVerb = originalVerb + "s";
                        else
                        {
                            newVerb = originalVerb.TrimEnd('y') + "ies";
                        }
                    }
                    else
                        newVerb = originalVerb + "s";
                    break;
            }

            return newVerb;
        }

        private string BuildObjectString()
        {
            StringBuilder sObject = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _objectSyntaxList.Length - 1;
            rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
            string objectSyntax = _objectSyntaxList[rndNum];

            switch (objectSyntax)
            {
                case "adjective":
                    sObject.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    break;
                case "nounphrase":
                    sObject.Append(BuildNounPhrase());
                    break;
                case "pronoun":
                    sObject.Append(_randomWordGenerators[(int)enWordType.ObjectPronoun].GetWord());
                    break;
                case "FirstName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    break;
                case "LastName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "FullName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "LastName BusinessName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "LastName BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BusinessName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNameSyllable1 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Syllable1+BizNameSyllable2 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                default:
                    sObject.Append("<<< object default >>>");
                    sObject.Append(" ");
                    break;
            }

            string temp = sObject.ToString();
            temp = temp.Replace("<CityName>", _randomWordGenerators[(int)enWordType.CityName].GetWord());
            temp = temp.Replace("<StateName>", _randomWordGenerators[(int)enWordType.StateName].GetWord());
            sObject.Length = 0;
            sObject.Append(temp);



            return sObject.ToString();
        }

        private string BuildSubordinateClause(ref bool useThirdPersonSingular)
        {
            StringBuilder subordinateClause = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            subordinateClause.Append(_randomWordGenerators[(int)enWordType.SubordinateConjunction].GetWord().Replace("_", " "));
            subordinateClause.Append(" ");

            rndMax = _subordinateClauseList.Length - 1;
            rndNum = _rn.GenerateRandomInt(rndMin, rndMax);
            string subordinateClauseSyntax = _subordinateClauseList[rndNum];
            useThirdPersonSingular = true;

            string[] subordinateClauseElements = subordinateClauseSyntax.Split(' ');
            for (int i = 0; i < subordinateClauseElements.Length; i++)
            {
                switch (subordinateClauseElements[i])
                {
                    case "subject":
                        subordinateClause.Append(BuildSubjectString(ref useThirdPersonSingular));
                        break;
                    case "verb":
                        subordinateClause.Append(BuildVerbPhrase(useThirdPersonSingular));
                        break;
                    case "object":
                        subordinateClause.Append(BuildObjectString());
                        break;
                    default:
                        subordinateClause.Append("<<< subordinateClause default >>>");
                        subordinateClause.Append(" ");
                        break;
                }
            }

            return subordinateClause.ToString();
        }


    }//end class
}//end namespace
