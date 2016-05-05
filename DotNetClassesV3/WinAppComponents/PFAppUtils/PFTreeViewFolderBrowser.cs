//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PFAppUtils
{
    /// <summary>
    /// Encapsulates logic to add and manage objects in a TreeView object. 
    /// </summary>
    public class PFTreeViewFolderBrowser
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private TreeView _tv = null;

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFTreeViewFolderBrowser(TreeView tv, string rootPath)
        {
            _tv = tv;
            _tv.Nodes.Clear();
            TreeNode rootNode = _tv.Nodes.Add(rootPath);
            FillChildNodes(rootNode);
        }

        //properties

        //methods
        /// <summary>
        /// Retrieves the child folders for the folder represented by the node parameter.
        /// </summary>
        /// <param name="node">Full path to folder represented by this node.</param>
        public void FillChildNodes(TreeNode node)
        {

            try
            {
                DirectoryInfo dirs = new DirectoryInfo(node.FullPath);

                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    TreeNode newnode = new TreeNode(dir.Name);
                    node.Nodes.Add(newnode);
                    newnode.Nodes.Add("*");
                }

            }

            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("FillChildNodes failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }

        }//end method

        /// <summary>
        /// Routine to handle tree view node expansion.
        /// </summary>
        /// <param name="sender">TreeView control.</param>
        /// <param name="e">Object containing the arguments for this event.</param>
        public void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

            if (e.Node.Nodes[0].Text == "*")
            {

                e.Node.Nodes.Clear();

                FillChildNodes(e.Node);

            }

        }
    
    
    }//end class

}//end namespace
