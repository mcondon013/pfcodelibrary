        public void ImportDataFromDataTable(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select * from ");
            sql.Append(dt.TableName);
            sql.Append(";");
            SqlCommand cmd = new SqlCommand(sql.ToString(), _conn);
            cmd.CommandType = CommandType.Text;
            DataTable dbTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.SelectCommand = cmd;
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.InsertCommand = builder.GetInsertCommand();
            da.FillSchema(dt, SchemaType.Source);
            da.Fill(dbTable);

            //check for invalid column names
            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                if (dt.Columns[colInx].ColumnName.ToLower() == "value")
                {
                    _msg.Length = 0;
                    _msg.Append("Column name ");
                    _msg.Append(dt.Columns[colInx].ColumnName);
                    _msg.Append(" is invalid. Please choose another name for the column.");
                    throw new DataException(_msg.ToString());
                }
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow inrow = dt.Rows[i];
                DataRow outrow = dbTable.NewRow();
                outrow.ItemArray = inrow.ItemArray;
                dbTable.Rows.Add(outrow);
            }

            da.Update(dbTable);
            dbTable.AcceptChanges();


        }
        

