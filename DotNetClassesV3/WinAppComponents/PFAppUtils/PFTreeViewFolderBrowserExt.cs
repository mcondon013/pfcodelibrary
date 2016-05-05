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
    public class PFTreeViewFolderBrowserExt
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private TreeView _tv = null;

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFTreeViewFolderBrowserExt(TreeView tv, List<string> rootPaths)
        {
            _tv = tv;
            _tv.Nodes.Clear();
            foreach (string rootPath in rootPaths)
            {
                DriveInfo di = new DriveInfo(rootPath);
                if (di.IsReady)
                {
                    TreeNode rootNode = _tv.Nodes.Add(rootPath);
                    FillChildNodes(rootNode);
                }
            }
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
                    if (newnode != null)
                    {
                        node.Nodes.Add(newnode);
                        newnode.Nodes.Add("*");
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to add following directory to nodes: ");
                        _msg.Append(dir.Name);
                        AppGlobals.AppMessages.DisplayWarningMessage(_msg.ToString());
                    }
                }

            }

            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("FillChildNodes failed for ");
                if (node != null)
                    _msg.Append(node.FullPath);
                else
                    _msg.Append("<null>");
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
