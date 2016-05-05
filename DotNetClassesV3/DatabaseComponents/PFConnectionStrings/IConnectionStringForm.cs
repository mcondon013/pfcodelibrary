using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable 1591

namespace PFConnectionStrings
{
    interface IConnectionStringForm
    {

        PFDataAccessObjects.DatabasePlatform DbPlatform { get; set; }
        string ConnectionName { get; set; }
        string ConnectionString { get; set; }
        ConnectionStringPrompt CSP { get; set; }
        PFConnectionObjects.enConnectionAccessStatus ConnectionAccessStatus { get; set; }

        System.Windows.Forms.DialogResult ShowDialog();
    }
}

#pragma warning restore 1591
