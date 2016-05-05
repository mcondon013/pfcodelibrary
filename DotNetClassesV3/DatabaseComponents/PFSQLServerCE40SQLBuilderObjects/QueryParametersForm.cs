#pragma warning disable 1591
using System;
using System.Data.Common;
using System.Windows.Forms;

namespace PFSQLServerCE40SQLBuilderObjects
{
    public partial class QueryParametersForm : Form
    {
        private readonly DbCommand _command;

        public QueryParametersForm(DbCommand command)
        {
            _command = command;

            InitializeComponent();

            for (int i = 0; i < _command.Parameters.Count; i++)
            {
                DbParameter p = _command.Parameters[i];

                grid.Rows.Add();
                grid.Rows[i].Cells[0].Value = p.ParameterName;
                grid.Rows[i].Cells[1].Value = p.DbType;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _command.Parameters.Count; i++)
            {
                _command.Parameters[i].Value = grid.Rows[i].Cells[2].Value;
            }
        }
    }
}
#pragma warning restore 1591
