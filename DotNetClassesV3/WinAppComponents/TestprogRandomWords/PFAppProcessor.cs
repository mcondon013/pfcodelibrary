using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFProcessObjects;
using PFMessageLogs;
using PFRandomData;

namespace TestprogRandomWords
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;


        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }


        //application routines

        public void RandomWordsTest(PFRandomData.enWordType _wordType, int numWords)
        {
            RandomWord wd = new RandomWord();

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomWordsTest started ...");
                _messageLog.WriteLine(_msg.ToString());

                OutputWords(wd, _wordType, numWords);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... RandomWordsTest finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }

        private void OutputWords(RandomWord wd, enWordType wordType, int numWordsToGenerate)
        {
            wd.WordType = wordType;

            for (int i = 0; i < 10; i++)
            {
                _msg.Length = 0;
                _msg.Append("WordType ");
                _msg.Append(wd.WordType.ToString());
                _msg.Append(": ");
                _msg.Append(wd.GetWord());
                _messageLog.WriteLine(_msg.ToString());

            }
        }



        public void RandomSentencesTest(int minNumSentences, int maxNumSentences)
        {
            RandomNumber rn = new RandomNumber();
            RandomSentence rs = new RandomSentence();
            int numSentences = 0;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomSentencesTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                numSentences = rn.GenerateRandomInt(minNumSentences, maxNumSentences);

                for (int i = 0; i < numSentences; i++)
                {
                    _msg.Length = 0;
                    _msg.Append((i+1).ToString());
                    _msg.Append(": ");
                    _msg.Append(rs.GenerateSentence());
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("\r\nAll:\r\n");
                _msg.Append(rs.GenerateSentences(numSentences));
                Program._messageLog.WriteLine(_msg.ToString());


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomSentencesTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public  void RandomParagraphsTest(int minNumParagraphs, int maxNumParagraphs, int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph)
        {
            RandomNumber rn = new RandomNumber();
            RandomDocument rd = new RandomDocument();
            int numParagraphs = 0;
            int numSentences = 0;
            string paragraph = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomParagraphsTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _str.Length = 0;

                numParagraphs = rn.GenerateRandomInt(minNumParagraphs, maxNumParagraphs);

                for (int p = 0; p < numParagraphs; p++)
                {
                    numSentences = rn.GenerateRandomInt(minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                    paragraph = rd.GenerateParagraph(numSentences);
                    _str.Append(paragraph);
                    _str.Append(Environment.NewLine);
                    _str.Append(Environment.NewLine);
                }

                Program._messageLog.WriteLine(_str.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomParagraphsTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void RandomDocumentTest(int minNumParagraphs, int maxNumParagraphs, int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph, string documentSubject)
        {
            RandomNumber rn = new RandomNumber();
            RandomDocument rd = new RandomDocument();
            int numParagraphs = 0;
            string doc = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDocumentTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _str.Length = 0;

                numParagraphs = rn.GenerateRandomInt(minNumParagraphs, maxNumParagraphs);

                doc = rd.GenerateDocument(numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph, documentSubject);
                _str.Append(doc);
                Program._messageLog.WriteLine(_str.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDocumentTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomChaptersTest(int numChapters, int minNumParagraphsPerChapter, int maxNumParagraphsPerChapter, 
                                                        int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph,
                                       string pChapterHeadings)
        {
            RandomNumber rn = new RandomNumber();
            RandomDocument rd = new RandomDocument();
            int numParagraphs = 0;
            string chap = string.Empty;
            string[] chapterHeadings = null;
            int chapHeadingInx = -1;
            int chapHeadingMaxInx = -1;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomChaptersTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (pChapterHeadings.Trim().Length > 0)
                {
                    chapterHeadings = pChapterHeadings.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    chapHeadingMaxInx = chapterHeadings.Length - 1;
                }

                chapHeadingInx = -1;
                for (int ch = 0; ch < numChapters; ch++)
                {
                    numParagraphs = rn.GenerateRandomInt(minNumParagraphsPerChapter, maxNumParagraphsPerChapter);
                    if (chapterHeadings == null)
                    {
                        _str.Length = 0;
                        _str.Append("Chapter ");
                        _str.Append((ch + 1).ToString());
                        _str.Append(Environment.NewLine);
                        _str.Append(Environment.NewLine);
                        chap = rd.GenerateChapter(numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                        _str.Append(chap);
                        chap = _str.ToString();
                    }
                    else
                    {
                        chapHeadingInx++;
                        if (chapHeadingInx > chapHeadingMaxInx)
                        {
                            //More chapters are being generated than there are defined chapter headings
                            _str.Length = 0;
                            _str.Append("Chapter ");
                            _str.Append((ch + 1).ToString());
                            chap = rd.GenerateChapter(_str.ToString(), numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                        }
                        else
                        {
                            chap = rd.GenerateChapter(chapterHeadings[chapHeadingInx], numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                        }
                    }
                    Program._messageLog.WriteLine(chap);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomChaptersTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomBookTest(string bookTitle, int numChapters, string pChapterHeadings,
                                   int minNumParagraphsPerChapter, int maxNumParagraphsPerChapter, 
                                   int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph)
        {
            RandomNumber rn = new RandomNumber();
            RandomDocument rd = new RandomDocument();
            string theEndText = "Finis ... The End";
            string book = string.Empty;
            string[] chapterHeadings = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomBookTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (pChapterHeadings.Trim().Length > 0)
                {
                    chapterHeadings = pChapterHeadings.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                }

                if (chapterHeadings == null)
                {
                    book = rd.GenerateBook(bookTitle, numChapters, minNumParagraphsPerChapter, maxNumParagraphsPerChapter, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                }
                else
                {
                    book = rd.GenerateBook(bookTitle, theEndText, numChapters, chapterHeadings,
                                           minNumParagraphsPerChapter, maxNumParagraphsPerChapter, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
                }

                Program._messageLog.WriteLine(book);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomBookTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        
        

    }//end class
}//end namespace
