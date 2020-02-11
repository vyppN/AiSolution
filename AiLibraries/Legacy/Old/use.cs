using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace AiLibraries.Legacy.Old
{
    public class use : BaseUse
    {
        public use(DataTable send_dataset)
            : base(send_dataset)
        {
        }

        public use(DataTable send_dataset, int mode_pointer)
            : base(send_dataset, mode_pointer)
        {
        }

        public void delcolumn(string columnname)
        {
            Dt.Columns.Remove(columnname);
        }

        public bool find_next(string columnname, object value_search)
        {
            int skiprecord = Conversions.ToInteger(Getnumrow);
            bool flag = false;
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

        public void altertable(string columnname, string columntype)
        {
            Dt.Columns.Add(new DataColumn(columnname, Type.GetType(columntype)));
        }

        public void group(string column_name)
        {
            top();
            var hashtable = new Hashtable();
            while (!eof)
            {
                if (hashtable.Contains(RuntimeHelpers.GetObjectValue(get_getdata(column_name))))
                {
                    delete();
                    skip();
                }
                else
                {
                    hashtable.Add(RuntimeHelpers.GetObjectValue(get_getdata(column_name)),
                                  RuntimeHelpers.GetObjectValue(get_getdata(column_name)));
                    skip();
                }
            }
            top();
        }

        public void append_blank()
        {
            if (!Operators.ConditionalCompareObjectNotEqual(Getcolumncount, 0, false))
                return;
            if (eof)
            {
                Dt.Rows.Add(new object[0]);
                bot();
            }
            else
            {
                Dt.Rows.InsertAt(Dt.NewRow(), checked(Numrow + 1));
                Numrow = checked(Numrow + 1);
            }
        }

        public void append_blank_before()
        {
            if (!Operators.ConditionalCompareObjectNotEqual(Getcolumncount, 0, false))
                return;
            if (eof)
            {
                Dt.Rows.Add(new object[0]);
                bot();
            }
            else
            {
                Dt.Rows.InsertAt(Dt.NewRow(), Numrow);
            }
        }

        public void appendfrom(use use)
        {
            use.top();
            while (!use.eof)
            {
                append_blank();
                int i = 0;
                while (Operators.ConditionalCompareObjectNotEqual(i, Getcolumncount, false))
                {
                    try
                    {
                        set_getdata(Conversions.ToString(get_getcolumnname(i)),
                                    RuntimeHelpers.GetObjectValue(
                                        use.get_getdata(Conversions.ToString(get_getcolumnname(i)))));
                    }
                    catch (Exception ex)
                    {
                        ProjectData.SetProjectError(ex);
                        ProjectData.ClearProjectError();
                    }
                    checked
                    {
                        ++i;
                    }
                }
                use.skip();
            }
            bot();
        }

        public void AcceptChanges()
        {
            Dt.AcceptChanges();
        }

        public void delete()
        {
            if (Dt.Rows.Count <= 0)
                return;
            if (Numrow == -1)
                Dt.Rows[0].Delete();
            else
                Dt.Rows[Numrow].Delete();
            Numrow = checked(Numrow - 1);
            if (PointerMode != 1)
                Dt.AcceptChanges();
        }

        public void deleteall()
        {
            top();
            while (!eof)
            {
                Dt.Rows[Numrow].Delete();
                skip();
            }
            if (PointerMode == 1)
                return;
            Dt.AcceptChanges();
        }

        public void filter_for(string commandfilter)
        {
            Dt.DefaultView.RowFilter = commandfilter;
            Dt = Dt.DefaultView.ToTable();
            Numrow = 0;
            if (Dt.Rows.Count > 0)
                Row = Dt.Rows[Numrow];
            else
                Row = null;
        }

        public virtual use copystructure()
        {
            var use = new use(Dt);
            use.deleteall();
            return use;
        }
    }
}