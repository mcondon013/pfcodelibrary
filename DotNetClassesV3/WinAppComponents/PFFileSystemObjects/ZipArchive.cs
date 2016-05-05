//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Enumerates the various compression levels supported by the zip library.
    /// </summary>
    public enum ZipCompressionLevel
    {
#pragma warning disable 1591
        NoCompression = Ionic.Zlib.CompressionLevel.None,
        BestSpeed = Ionic.Zlib.CompressionLevel.BestSpeed,
        BestCompression = Ionic.Zlib.CompressionLevel.BestCompression,
        DefaultCompression = Ionic.Zlib.CompressionLevel.Default
#pragma warning restore 1591
    }


    /// <summary>
    /// Class to manage adding, extracting and removing entries in a zip file.
    /// </summary>
    /// ///<remarks>This class requires that the Ionic.Zip.DLL lbrary (from DotNetZip on CodePlex) be installed along with the calling application.</remarks>
    public class ZipArchive
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _zipFilename = string.Empty;
        private string _zipPassword = string.Empty;
        private ZipCompressionLevel _zipCompressionLevel = ZipCompressionLevel.DefaultCompression;
        private bool _overwriteSilently = false;
        private bool _useUnicodeAsNecessary = false;
        private Zip64Option _useZip64WhanSaving = Zip64Option.Default;
        private ZipOption _alternativeEncodingUsage = ZipOption.Default;
        private Encoding _alternateEncoding = Encoding.Default;
        private bool _flattenFoldersOnExtract = false;
        private string _directoryPathInArchive = null;

        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">Path to the zip archive file.</param>
        public ZipArchive(string fileName)
        {
            _zipFilename = fileName;
        }

        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">Path to the zip archive file.</param>
        /// <param name="password">Sets the password to use when adding or retrieving entries from the zip file.</param>
        public ZipArchive(string fileName, string password)
        {
            _zipPassword = password;
        }

        //properties

        /// <summary>
        /// Path to the zip archive file.
        /// </summary>
        public string FileName
        {
            get
            {
                return _zipFilename;
            }
        }

        /// <summary>
        /// Returns the number of entries currently in the zip archive.
        /// </summary>
        public int Count
        {
            get
            {
                return GetNumEntriesInZipFile();
            }
        }

        /// <summary>
        /// Returns an array of strings containing the names of the entries in the zip archive.
        /// </summary>
        public string[] Entries
        {
            get
            {
                return GetListOfEntryNamesInZip();
            }
        }

        /// <summary>
        ///  Gets or sets a comment associated with the current zip file.
        /// </summary>
        public string Comment
        {
            get
            {
                return GetZipComment();
            }
            set
            {
                SetZipComment(value);
            }
        }

        /// <summary>
        ///  Gets or sets the compression level to use when adding entries to the zip archive.
        /// </summary>Higher compression settings create smaller files, but take longer to process.
        ///  The default setting ZipCompressionLevelExt.DefaultCompression) provides a good trade-off between compression and speed.
        /// <remarks>
        /// </remarks>
        public ZipCompressionLevel CompressionLevel
        {
            get
            {
                return _zipCompressionLevel;
            }
            set
            {
                _zipCompressionLevel = value;
            }
        }

        /// <summary>
        /// Determines whether or not calling program will be prompted to delete an existing file.
        /// If set to true, calling program will not be prompted and files will be silently overwritten.
        /// Default is false: i.e. prompt for delete.
        /// </summary>
        public bool OverwriteSilently
        {
            get
            {
                return _overwriteSilently;
            }
            set
            {
                _overwriteSilently = value;
            }
        }
        /// <summary>
        /// Indicates whether to encode entry filenames and entry comments using Unicode (UTF-8). 
        /// </summary>
        /// <remarks>
        /// ***Remarks were taken from the DOTNETZIP documentation on the web.***
        ///The PKWare zip specification provides for encoding file names and file comments in either the IBM437 code page, or in UTF-8. This flag selects the encoding according to that specification. 
        ///By default, this flag is false, and filenames and comments are encoded into the zip file in the IBM437 codepage. Setting this flag to true will specify that filenames and comments that cannot be encoded with IBM437 will be encoded with UTF-8. 
        ///Zip files created with strict adherence to the PKWare specification with respect to UTF-8 encoding can contain entries with filenames containing any combination of Unicode characters, 
        ///including the full range of characters from Chinese, Latin, Hebrew, Greek, Cyrillic, and many other alphabets. However, because at this time, the UTF-8 portion of the PKWare specification is not broadly supported by other zip libraries and utilities, such zip files may not be readable by your favorite zip tool or archiver. 
        ///In other words, interoperability will decrease if you set this flag to true. 
        ///In particular, Zip files created with strict adherence to the PKWare specification with respect to UTF-8 encoding will not work well with Explorer in Windows XP or Windows Vista, because Windows compressed folders, as far as I know, do not support UTF-8 in zip files. 
        ///Vista can read the zip files, but shows the filenames incorrectly. Unpacking from Windows Vista Explorer will result in filenames that have rubbish characters in place of the high-order UTF-8 bytes. 
        ///Also, zip files that use UTF-8 encoding will not work well with Java applications that use the java.util.zip classes, as of v5.0 of the Java runtime. 
        ///The Java runtime does not correctly implement the PKWare specification in this regard. 
        ///As a result, we have the unfortunate situation that "correct" behavior by the DotNetZip library with regard to Unicode encoding of filenames during zip creation will result in zip files that are readable by strictly compliant 
        ///and current tools (for example the most recent release of the commercial WinZip tool); but these zip files will not be readable by various other tools or libraries, including Windows Explorer. 
        ///The DotNetZip library can read and write zip files with UTF8-encoded entries, according to the PKware spec. If you use DotNetZip for both creating and reading the zip file, and you use UTF-8, 
        ///there will be no loss of information in the filenames. For example, using a self-extractor created by this library will allow you to unpack files correctly with no loss of information in the filenames. 
        ///If you do not set this flag, it will remain false. If this flag is false, the ZipOutputStream will encode all filenames and comments using the IBM437 codepage. 
        ///This can cause "loss of information" on some filenames, but the resulting zipfile will be more interoperable with other utilities. 
        ///As an example of the loss of information, diacritics can be lost. The o-tilde character will be down-coded to plain o. The c with a cedilla (Unicode 0xE7) used in Portugese will be downcoded to a c. Likewise, the O-stroke character (Unicode 248), used in Danish and Norwegian, will be down-coded to plain o. 
        ///Chinese characters cannot be represented in codepage IBM437; when using the default encoding, Chinese characters in filenames will be represented as ?. These are all examples of "information loss". 
        ///The loss of information associated to the use of the IBM437 encoding is inconvenient, and can also lead to runtime errors. For example, using IBM437, any sequence of 4 Chinese characters will be encoded as ????. 
        ///If your application creates a ZipOutputStream, does not set the encoding, then adds two files, each with names of four Chinese characters each, this will result in a duplicate filename exception. In the case where you add a single file with a name containing four Chinese characters, the zipfile will save properly, but extracting that file later, with any zip tool, will result in an error, 
        ///because the question mark is not legal for use within filenames on Windows. These are just a few examples of the problems associated to loss of information. 
        ///This flag is independent of the encoding of the content within the entries in the zip file. Think of the zip file as a container - it supports an encoding. 
        ///Within the container are other "containers" - the file entries themselves. The encoding within those entries is independent of the encoding of the zip archive container for those entries. 
        ///Rather than specify the encoding in a binary fashion using this flag, an application can specify an arbitrary encoding via the ProvisionalAlternateEncoding property. 
        ///Setting the encoding explicitly when creating zip archives will result in non-compliant zip files that, curiously, are fairly interoperable. 
        ///The challenge is, the PKWare specification does not provide for a way to specify that an entry in a zip archive uses a code page that is neither IBM437 nor UTF-8. 
        ///Therefore if you set the encoding explicitly when creating a zip archive, you must take care upon reading the zip archive to use the same code page. 
        ///If you get it wrong, the behavior is undefined and may result in incorrect filenames, exceptions, stomach upset, hair loss, and acne.         
        /// </remarks>
        public bool UseUnicodeAsNecessary
        {
            get
            {
                return _useUnicodeAsNecessary;
            }
            set
            {
                _useUnicodeAsNecessary = value;
            }
        }

        /// <summary>
        /// Specify whether to use ZIP64 extensions when saving a zip archive. 
        /// </summary>
        /// <remarks>
        ///When creating a zip file, the default value for the property is Never. AsNecessary is safest, in the sense that you will not get an Exception if a pre-ZIP64 limit is exceeded. 
        ///You may set the property at any time before calling Save(). 
        ///When reading a zip file via the Zipfile.Read() method, DotNetZip will properly read ZIP64-endowed zip archives, regardless of the value of this property. 
        ///DotNetZip will always read ZIP64 archives. This property governs only whether DotNetZip will write them. 
        ///Therefore, when updating archives, be careful about setting this property after reading an archive that may use ZIP64 extensions. 
        ///An interesting question is, if you have set this property to AsNecessary, and then successfully saved, does the resulting archive use ZIP64 extensions or not? To learn this, check the OutputUsedZip64 property, after calling Save(). 
        /// </remarks>
        public Zip64Option UseZip64WhanSaving
        {
            get
            {
                return _useZip64WhanSaving;
            }
            set
            {
                _useZip64WhanSaving = value;
            }
        }

        /// <summary>
        /// Describes if and when this instance should apply AlternateEncoding to encode the FileName and Comment, when saving. 
        /// </summary>
        public ZipOption AlternativeEncodingUsage
        {
            get
            {
                return _alternativeEncodingUsage;
            }
            set
            {
                _alternativeEncodingUsage = value;
            }
        }

        /// <summary>
        /// A Text Encoding to use when encoding the filenames and comments for all the ZipEntry items, during a ZipFile.Save() operation. 
        /// </summary>
        /// <remarks>
        /// Whether the encoding specified here is used during the save depends on AlternateEncodingUsage.
        /// </remarks>
        public Encoding AlternateEncoding
        {
            get
            {
                return _alternateEncoding;
            }
            set
            {
                _alternateEncoding = value;
            }
        }

        /// <summary>
        /// Indicates whether extracted files should keep their paths as stored in the zip archive.
        /// </summary>
        /// <remarks>
        ///This property affects Extraction. It is not used when creating zip archives. 
        ///With this property set to false, the default, extracting entries from a zip file will create files in the filesystem that have the full path associated to the entry within the zip file.
        ///With this property set to true, extracting entries from the zip file results in files with no path: the folders are "flattened." 
        ///An example: suppose the zip file contains entries /directory1/file1.txt and /directory2/file2.txt. 
        ///With FlattenFoldersOnExtract set to false, the files created will be \directory1\file1.txt and \directory2\file2.txt. 
        ///With the property set to true, the files created are file1.txt and file2.txt. 
        /// </remarks>
        public bool FlattenFoldersOnExtract
        {
            get
            {
                return _flattenFoldersOnExtract;
            }
            set
            {
                _flattenFoldersOnExtract = value;
            }
        }

        /// <summary>
        /// Specifies a directory path to use to override any path in the fileName being archived.
        /// </summary>
        /// <remarks>This path may, or may not, correspond to a real directory in the current filesystem. 
        /// If the files within the zip are later extracted, this is the path used for the extracted file. 
        /// Passing null (Nothing in VB) will use the path on the fileName, if any. 
        /// Passing the empty string ("") will insert the item at the root path within the archive.
        /// </remarks>
        public string DirectoryPathInArchive
        {
            get
            {
                return _directoryPathInArchive;
            }
            set
            {
                _directoryPathInArchive = value;
            }
        }



        //private helper methods for class follow

        private int GetNumEntriesInZipFile()
        {
            if (File.Exists(_zipFilename) == false)
            {
                return -1;
            }

            int cnt = -1;
            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                cnt = zip.Count;
            }
            return cnt;
        }

        private string[] GetListOfEntryNamesInZip()
        {
            if (File.Exists(_zipFilename) == false)
            {
                return null;
            }

            string[] entryNames = null;
            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                int entryInx = 0;
                int totalEntries = zip.Entries.Count;
                entryNames = new string[totalEntries];
                foreach (ZipEntry e in zip.Entries)
                {
                    entryNames[entryInx] = e.FileName;
                }
            }
            return entryNames;
        }

        private string GetZipComment()
        {
            if (File.Exists(_zipFilename) == false)
            {
                return string.Empty;
            }

            string zipComment = string.Empty;
            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                zipComment = zip.Comment;
            }
            return zipComment;
        }

        private void SetZipComment(string value)
        {
            if (File.Exists(_zipFilename) == false)
            {
                return;
            }

            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                zip.Comment = value;
                SaveZipFile(zip);
            }
        }

        private ZipCompressionLevel GetCompressionLevel()
        {
            if (File.Exists(_zipFilename) == false)
            {
                return ZipCompressionLevel.DefaultCompression;
            }

            ZipCompressionLevel compressionLevel = ZipCompressionLevel.DefaultCompression;
            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                compressionLevel = (ZipCompressionLevel)zip.CompressionLevel;
            }
            return compressionLevel;
        }

        private void SetCompressionLevel(ZipCompressionLevel value)
        {
            if (File.Exists(_zipFilename) == false)
            {
                return;
            }

            using (ZipFile zip = ZipFile.Read(_zipFilename))
            {
                zip.CompressionLevel = (Ionic.Zlib.CompressionLevel)value;
                SaveZipFile(zip);
            }
        }


        /// <summary>
        /// Saves entries to zip file. Overwrites zip file if it already exists.
        /// </summary>
        /// <param name="zip">ZipFile object that will do the zipping and saving.</param>
        /// <remarks>The ZipFile instance is written to storage, typically a zip file in a filesystem, only when the caller calls Save. 
        /// In the typical case, the Save operation writes the zip content to a temporary file, and then renames the temporary file to the desired name. 
        /// If necessary, this method will delete a pre-existing file before the rename. </remarks>
        private void SaveZipFile(ZipFile zip)
        {
            SetupZipFileObject(ref zip);
            if (String.IsNullOrEmpty(_zipPassword) == false)
                zip.Password = _zipPassword;
            zip.Save(_zipFilename);
        }

        private void SetupZipFileObject(ref ZipFile zip)
        {
            zip.CompressionLevel = (Ionic.Zlib.CompressionLevel)_zipCompressionLevel;
            zip.UseUnicodeAsNecessary = _useUnicodeAsNecessary;
            zip.UseZip64WhenSaving = _useZip64WhanSaving;
            zip.AlternateEncodingUsage = _alternativeEncodingUsage;
            zip.AlternateEncoding = _alternateEncoding;
        }

        /// <summary>
        /// Determines whether the collection contains an entry with a given name. 
        /// </summary>
        /// <param name="zip">ZipFile object that will be search for the entry with the specified name.</param>
        /// <param name="entryName">Name of the entry to look for.</param>
        /// <returns>True if the collection contains an entry with the given name, false otherwise.</returns>
        private bool Contains(ZipFile zip, string entryName)
        {
            bool ret = false;

            try
            {
                if (zip.ContainsEntry(entryName))
                    ret = true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return ret;
        }

        //public methods

        /// <summary>
        /// Determines whether the collection contains an entry with a given name. 
        /// </summary>
        /// <param name="zipFilename">Full path for the zip file.</param>
        /// <param name="entryName">Name of the entry to look for.</param>
        /// <returns>True if the collection contains an entry with the given name, false otherwise.</returns>
        public bool Contains(string zipFilename, string entryName)
        {
            bool ret = false;

            if (File.Exists(zipFilename) == false)
            {
                return false;
            }

            try
            {
                using (ZipFile zip = ZipFile.Read(_zipFilename))
                {
                    if (zip.ContainsEntry(entryName))
                        ret = true;
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return ret;
        }


        /// <summary>
        /// Compresses group of files using Zip format.
        /// </summary>
        /// <param name="sourceFiles">Array of file paths to be zipped.</param>
        /// <returns>Returns true if files successfully added to the zip archive.</returns>
        public bool AddFiles(string[] sourceFiles)
        {
            bool zipResult = false;
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFiles(sourceFiles, _directoryPathInArchive);
                    SaveZipFile(zip);
                }
                zipResult = true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            return zipResult;
        }

        /// <summary>
        /// Unzips all the entries in a zip archive.
        /// </summary>
        /// <param name="destinationFolder">Path to the folder that will contain the entracted entries.</param>
        /// <returns>Returns the number of entries extracted.</returns>
        public int ExtractAll(string destinationFolder)
        {
            int numEntriesExtracted = 0;
            ExtractExistingFileAction fileAction = ExtractExistingFileAction.Throw;
            try
            {
                if (Directory.Exists(destinationFolder) == false)
                    Directory.CreateDirectory(destinationFolder);

                using (ZipFile zip = ZipFile.Read(_zipFilename))
                {
                    zip.FlattenFoldersOnExtract = _flattenFoldersOnExtract;
                    if (_overwriteSilently)
                        fileAction = ExtractExistingFileAction.OverwriteSilently;
                    if (_zipPassword.TrimEnd().Length == 0)
                    {
                        zip.ExtractAll(destinationFolder, fileAction);
                    }
                    else
                    {
                        //zip is password protected
                        foreach (ZipEntry e in zip)
                        {
                            e.Password = _zipPassword;
                            e.Extract(destinationFolder, fileAction);
                        }
                    }
                }

                numEntriesExtracted = this.Count;

            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return numEntriesExtracted;
        }


    }//end class
}//end namespace
