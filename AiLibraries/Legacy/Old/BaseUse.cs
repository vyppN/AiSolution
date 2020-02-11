using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AiLibraries.Legacy.Old
{
    public abstract class BaseUse
    {
        protected bool Ceof;
        protected Dictionary<int, string> DictionaryId;
        protected DataTable Dt;
        protected int Numrow;
        protected int PointerMode;
        protected string Primarykey;
        protected DataRow Row;
        protected int Totalrow;

        protected BaseUse(DataTable send_dataset)
        {
            Dt = send_dataset.Copy();
            Numrow = 0;
            if (Dt.Rows.Count > 0)
                Row = Dt.Rows[Numrow];
            else
                Row = null;
        }

        protected BaseUse(DataTable send_dataset, int mode)
        {
            PointerMode = mode;
            Dt = mode != 0 ? send_dataset : send_dataset.Copy();
            Numrow = 0;
            if (Dt.Rows.Count > 0)
                Row = Dt.Rows[Numrow];
            else
                Row = null;
        }

        public DataTable GetDataTable
        {
            get { return Dt.Copy(); }
        }

        public object Gettablename
        {
            get { return Dt.TableName; }
        }

        public object Getnumrow
        {
            get { return Numrow; }
        }

        public object Gettotalrow
        {
            get { return Dt.Rows.Count; }
        }

        public object Getcolumncount
        {
            get { return Dt.Columns.Count; }
        }

        public bool eof
        {
            get
            {
                if (Dt.Rows.Count > 0 && !Operators.ConditionalCompareObjectEqual(Numrow, Gettotalrow, false))
                    return false;
                return true;
            }
        }

        public object get_getcolumnname(int i)
        {
            return Dt.Columns[i].ColumnName;
        }

        public Type get_getcolumntype(int i)
        {
            return Dt.Columns[i].DataType;
        }

        public Type get_getcolumntype(string i)
        {
            return Dt.Columns[i].DataType;
        }

        public string getcolumnname_list()
        {
            DataTable dataTable2 = Dt;
            string str = "";
            str = dataTable2.Columns.Cast<DataColumn>().Aggregate(str,
                                                                  (current, dataColumn2) =>
                                                                  current + "," + dataColumn2.ColumnName);
            return str.Substring(1);
        }

        public AutoCompleteStringCollection getautocomplete(params string[] nameoffield_string)
        {
            var stringCollection = new AutoCompleteStringCollection();
            var use = new use(Dt);
            string[] strArray = nameoffield_string;
            int index = 0;
            while (index < strArray.Length)
            {
                string nameoffield = strArray[index];
                while (!use.eof)
                {
                    stringCollection.Add(use.get_getdata(nameoffield).ToString());
                    use.skip();
                }
                checked
                {
                    ++index;
                }
            }
            return stringCollection;
        }

        public string getcolumntype_list()
        {
            DataTable dataTable2 = Dt;
            string str = "";
            foreach (DataColumn dataColumn2 in dataTable2.Columns)
                str = str + "," + dataColumn2.DataType;
            return str.Substring(1);
        }

        public DataTable copy()
        {
            return Dt.Copy();
        }

        public virtual void top()
        {
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false))
                return;
            Numrow = 0;
        }

        public virtual void bot()
        {
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false))
                return;
            Numrow = Conversions.ToInteger(Operators.SubtractObject(Gettotalrow, 1));
        }

        public DataTable copy_for(string commandfilter)
        {
            Dt.DefaultView.RowFilter = commandfilter;
            DataTable dataTable2 = Dt.DefaultView.ToTable();
            Dt.DefaultView.RowFilter = "";
            return dataTable2;
        }

        public virtual void skip()
        {
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false) ||
                !Operators.ConditionalCompareObjectLessEqual(checked(Numrow + 1), Gettotalrow, false))
                return;
            Numrow = checked(Numrow + 1);
        }

        public object cr()
        {
            return "Ai Kung";
        }

        public virtual void skip_back()
        {
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false) ||
                !Operators.ConditionalCompareObjectLessEqual(checked(Numrow - 1), Gettotalrow, false))
                return;
            Numrow = checked(Numrow - 1);
        }

        public virtual void skip(int skiprecord)
        {
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false) ||
                !Conversions.ToBoolean(Operators.AndObject((checked(Numrow + skiprecord) >= 0),
                                                           Operators.CompareObjectLessEqual(
                                                               checked(Numrow + skiprecord),
                                                               Operators.SubtractObject(Gettotalrow, 1), false))))
                return;
            Numrow = checked(Numrow + skiprecord);
        }

        public virtual void brow()
        {
            var form = new Form();
            form.Width = Screen.PrimaryScreen.Bounds.Width;
            form.Height = Screen.PrimaryScreen.Bounds.Height;
            form.Text = Operators.CompareString(Dt.TableName, null, false) != 0
                            ? "brow : " + Dt.TableName
                            : "brow table no name";
            var dataGridView = new DataGridView();
            dataGridView.Width = 574;
            dataGridView.Height = 374;
            int index1 = 0;
            while (Operators.ConditionalCompareObjectLess(index1, Getcolumncount, false))
            {
                var dataGridViewColumn = new DataGridViewColumn();
                dataGridViewColumn.Name = Dt.Columns[index1].ColumnName;
                dataGridViewColumn.HeaderText = Dt.Columns[index1].ColumnName;
                dataGridView.Columns.Add(dataGridViewColumn.Name, dataGridViewColumn.Name);
                checked
                {
                    ++index1;
                }
            }
            int index2 = 0;
            while (Operators.ConditionalCompareObjectLess(index2, Gettotalrow, false))
            {
                dataGridView.Rows.Add();
                int index3 = 0;
                while (Operators.ConditionalCompareObjectLess(index3, Getcolumncount, false))
                {
                    DataGridViewRow dataGridViewRow = dataGridView.Rows[index2];
                    DataRow dataRow = Dt.Rows[index2];
                    dataGridViewRow.Cells[index3].Value =
                        Operators.CompareString(dataRow[index3].GetType().ToString(), "System.Byte[]", false) != 0
                            ? RuntimeHelpers.GetObjectValue(dataRow[index3])
                            : "[Binaryfield]";
                    if (Operators.ConditionalCompareObjectEqual(Getnumrow, index2, false))
                    {
                        dataGridViewRow.Cells[index3].Style.BackColor = Color.Yellow;
                        dataGridViewRow.Cells[index3].Selected = true;
                    }
                    checked
                    {
                        ++index3;
                    }
                }
                checked
                {
                    ++index2;
                }
            }
            form.Controls.Add(dataGridView);
            try
            {
                dataGridView.CurrentCell = dataGridView[0, Conversions.ToInteger(Getnumrow)];
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
            }
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.Dock = DockStyle.Fill;
        }

        public virtual void orderby(string columnname_order)
        {
            top();
            if (!Operators.ConditionalCompareObjectGreater(Gettotalrow, 0, false))
                return;
            Dt.DefaultView.Sort = columnname_order;
            Dt = Dt.DefaultView.ToTable();
        }

        public virtual void exclude_record()
        {
            if (Dt.Rows.Count <= 0)
                return;
            if (Numrow == -1)
                Dt.Rows[0].Delete();
            else
                Dt.Rows[Numrow].Delete();
            Numrow = checked(Numrow - 1);
            Dt.AcceptChanges();
        }

        public virtual bool find(string columnname, object value_search)
        {
            int skiprecord = Conversions.ToInteger(Getnumrow);
            bool flag = false;
            top();
            while (!eof)
            {
                if (Operators.ConditionalCompareObjectEqual(get_getdata(columnname), value_search, false))
                {
                    flag = true;
                    break;
                }
                skip();
            }
            if (!flag)
                skip(skiprecord);
            return flag;
        }

        public virtual object get_data(string nameoffield)
        {
            return get_getdata(nameoffield);
        }

        public virtual void set_data(string nameoffield, object value)
        {
            set_getdata(nameoffield, RuntimeHelpers.GetObjectValue(value));
        }

        public virtual object get_field(string nameoffield)
        {
            return get_getdata(nameoffield);
        }

        public virtual void set_field(string nameoffield, object value)
        {
            set_getdata(nameoffield, RuntimeHelpers.GetObjectValue(value));
        }

        public virtual object get_getdata(string nameoffield)
        {
            if (!eof)
            {
                if (Numrow == -1)
                    Numrow = 0;
                Row = Dt.Rows[Numrow];
                return Row[nameoffield];
            }
            if (!Operators.ConditionalCompareObjectNotEqual(Gettotalrow, 0, false))
                return null;
            bot();
            Row = Dt.Rows[Numrow];
            return Row[nameoffield];
        }

        public virtual void set_getdata(string nameoffield, object value)
        {
            if (eof)
                return;
            Row = Dt.Rows[Numrow];
            Row[nameoffield] = RuntimeHelpers.GetObjectValue(value);
        }

        public virtual object get_data_notnull(string nameoffield)
        {
            return get_getdata_notnull(nameoffield);
        }

        public virtual void set_data_notnull(string nameoffield, object value)
        {
            set_getdata_notnull(nameoffield, RuntimeHelpers.GetObjectValue(value));
        }

        public virtual object get_field_notnull(string nameoffield)
        {
            return get_getdata_notnull(nameoffield);
        }

        public virtual void set_field_notnull(string nameoffield, object value)
        {
            set_getdata_notnull(nameoffield, RuntimeHelpers.GetObjectValue(value));
        }

        public virtual object get_getdata_notnull(string nameoffield)
        {
            if (!eof)
            {
                if (Numrow == -1)
                    Numrow = 0;
                Row = Dt.Rows[Numrow];
                if (Operators.CompareString(Row[nameoffield].GetType().ToString(), "System.DBNull", false) != 0)
                    return Row[nameoffield];
                if (Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int16", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int32", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int64", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt16", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt32", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt64", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Double", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Byte", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.SByte", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.SByte", false) == 0 |
                    Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Single", false) == 0)
                    return 0;
                return "";
            }
            if (!Operators.ConditionalCompareObjectNotEqual(Gettotalrow, 0, false))
                return "";
            bot();
            Row = Dt.Rows[Numrow];
            if (Operators.CompareString(Row[nameoffield].GetType().ToString(), "System.DBNull", false) != 0)
                return Row[nameoffield];
            if (Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int16", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int32", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Int64", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt16", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt32", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.UInt64", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Double", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Byte", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.SByte", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.SByte", false) == 0 |
                Operators.CompareString(get_getcolumntype(nameoffield).ToString(), "System.Single", false) == 0)
                return 0;
            return "";
        }

        public virtual void set_getdata_notnull(string nameoffield, object value)
        {
            if (eof)
                return;
            Row = Dt.Rows[Numrow];
            Row[nameoffield] = !Information.IsNothing(RuntimeHelpers.GetObjectValue(value))
                                   ? RuntimeHelpers.GetObjectValue(value)
                                   : (RuntimeHelpers.GetObjectValue(value).IsNumber() ? (object) 0 : "");
        }

        public virtual object get_datatype(string nameoffield)
        {
            return get_getdatatype(nameoffield);
        }

        public virtual object get_fieldtype(string nameoffield)
        {
            return get_getdatatype(nameoffield);
        }

        public string get_getdatatype(string nameoffield)
        {
            return Dt.Columns[nameoffield].DataType.ToString().Trim();
        }
    }
}