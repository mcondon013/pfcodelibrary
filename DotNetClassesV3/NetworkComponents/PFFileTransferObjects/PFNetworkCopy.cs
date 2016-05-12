//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace PFFileTransferObjects
{
    /// <summary>
    /// Contains methods to copy, move and rename files on local area network servers.
    /// </summary>
    public class PFNetworkCopy
    {
        //private work variables
        private static  StringBuilder _msg = new StringBuilder();

        //private varialbles for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFNetworkCopy()
        {
            ;
        }



        //properties

        //methods

        /// <summary>
        /// Copies files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be copied.</param>
        /// <param name="destinationFile">Name of file at copy destination.</param>
        /// <remarks>Copy fails if destinationFile already exists.</remarks>
        public static void FileCopy(string sourceFile, string destinationFile)
        {
            FileCopy(sourceFile, destinationFile, false);
        }

        /// <summary>
        /// Copies files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be copied.</param>
        /// <param name="destinationFile">Name of file at copy destination.</param>
        /// <param name="overwriteDestination">If true and destination file exists, destination file will be deleted before sourceFile is copied.</param>
        public static void FileCopy(string sourceFile, string destinationFile, bool overwriteDestination)
        {
            if (File.Exists(sourceFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to copy file ");
                _msg.Append(sourceFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }
            if (sourceFile == destinationFile)
            {
                _msg.Length = 0;
                _msg.Append("Unable to copy file ");
                _msg.Append(sourceFile);
                _msg.Append(". Source and destination file paths are the same. ");
                throw new IOException(_msg.ToString());
            }
            if (File.Exists(destinationFile))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to copy file ");
                    _msg.Append(sourceFile);
                    _msg.Append(". Destination file ");
                    _msg.Append(destinationFile);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
            }

            File.Copy(sourceFile, destinationFile, overwriteDestination);
        }

        /// <summary>
        /// Moves files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be moved.</param>
        /// <param name="destinationFile">Name of file at move destination.</param>
        /// <remarks>Move fails if destination file already exists. Original file is deleted after a successful move.</remarks>
        public static void FileMove(string sourceFile, string destinationFile)
        {
            FileMove(sourceFile, destinationFile, false);
        }

        /// <summary>
        /// Moves files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be moved.</param>
        /// <param name="destinationFile">Name of file at move destination.</param>
        /// <param name="overwriteDestination">If true and destination file exists, destination file will be deleted before sourceFile is moved.</param>
        /// <remarks>Original file is deleted after the move.</remarks>
        public static void FileMove(string sourceFile, string destinationFile, bool overwriteDestination)
        {
            if (File.Exists(sourceFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to move file ");
                _msg.Append(sourceFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }
            if (sourceFile == destinationFile)
            {
                _msg.Length = 0;
                _msg.Append("Unable to move file ");
                _msg.Append(sourceFile);
                _msg.Append(". Source and destination file paths are the same. ");
                throw new IOException(_msg.ToString());
            }
            if (File.Exists(destinationFile))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to move file ");
                    _msg.Append(sourceFile);
                    _msg.Append(". Destination file name ");
                    _msg.Append(destinationFile);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
                else
                {
                    //overwriteDestination = true and destination file exists: so delete it
                    File.Delete(destinationFile);
                }
            }

            File.Move(sourceFile, destinationFile);
        }


        /// <summary>
        /// Renames file. Renamed file exists in same directory as original file name.
        /// </summary>
        /// <param name="originalFile">Full path to original file.</param>
        /// <param name="newFileName">New file name.</param>
        /// <remarks>Rename will fail if file with new name already exists.</remarks>
        public static void FileRename(string originalFile, string newFileName)
        {
            FileRename(originalFile, newFileName, false);
        }

        /// <summary>
        /// Renames file. Renamed file exists in same directory as original file name.
        /// </summary>
        /// <param name="originalFile">Full path to original file.</param>
        /// <param name="newFileName">New file name.</param>
        /// <param name="overwriteDestination">If true and a file with same name as original exists, the file with same name will be deleted </param>
        public static void FileRename(string originalFile, string newFileName, bool overwriteDestination)
        {
            if (File.Exists(originalFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to rename file ");
                _msg.Append(originalFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }

            string originalDirectoryPath = Path.GetDirectoryName(originalFile);
            string originalFileName = Path.GetFileName(originalFile);
            string renameFileName = Path.GetFileName(newFileName);
            string renameFilePath = Path.Combine(originalDirectoryPath, renameFileName);

            if (File.Exists(renameFilePath))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to rename file ");
                    _msg.Append(originalFile);
                    _msg.Append(". Destination file name ");
                    _msg.Append(renameFilePath);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
                else
                {
                    //overwriteDestination = true and destination file exists: so delete it
                    File.Delete(renameFilePath);
                }
            }

            File.Move(originalFileName, renameFilePath);

        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="fileToDelete">Full path for file to be deleted.</param>
        public static void FileDelete(string fileToDelete)
        {
            if (File.Exists(fileToDelete) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to delete file ");
                _msg.Append(fileToDelete);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }

            File.Delete(fileToDelete);

        }

        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value containing values.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String value.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>String containing name and value for all fields.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }



    }//end class
}//end namespace
