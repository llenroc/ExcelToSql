using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ExcelToSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel 活頁簿 (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls|文字檔 (Tab 字元分隔) (*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtbSourceFile.Text = ofd.FileName;
                else
                    txtbSourceFile.Text = string.Empty;
            }
        }

        /// <summary>
        /// 取得 Excel 文件中指定工作表的內容
        /// </summary>
        /// <param name="FileFullPath">檔案路徑</param>
        /// <param name="SheetName">工作表名稱</param>
        /// <returns>DataTable 工作表內容</returns>
        private DataTable GetExcelSheetData(string FileFullPath, string SheetName)
        {
            //string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"; //此連接只能操作Excel2007之前(.xls)文件
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'"; //此連接可以操作.xls與.xlsx文件
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                DataSet ds = new DataSet();
                //   ("select * from [Sheet1$]", conn);
                OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", SheetName), conn);
                odda.Fill(ds, SheetName);
                return ds.Tables[0];
            }
        }


        /// <summary>
        /// 取得 Excel 文件中所有工作表名
        /// </summary>
        /// <param name="excelFile">Excel 檔案路徑</param>
        /// <returns>string[] 工作表名稱集合</returns>
        private String[] GetExcelSheetNames(string excelFile)
        {
            System.Data.DataTable dt = null;
            try
            {
                //string connString = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFile + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"; //此連接只能操作Excel2007之前(.xls)文件
                string connString = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelFile + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'"; //此連接可以操作.xls與.xlsx文件

                // 建立連結
                using (OleDbConnection objConn = new OleDbConnection(connString))
                {
                    objConn.Open();

                    // 取得 Excel 資料結構
                    dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                        return null;

                    // 取得 Excel 資料表中的工作表
                    var excelSheets = dt.AsEnumerable().Where(r => r["TABLE_NAME"].ToString().Contains("$")).Select(s => s["TABLE_NAME"].ToString()).ToArray();
                    return excelSheets;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            if (dt != null)
                dt.Dispose();
        }

        private struct RegExcel
        {
            public string Path;
            public string DefaultValue;
            public bool Exist;

            public RegExcel(string path, string value, bool exist)
            {
                this.Path = path;
                this.DefaultValue = value;
                this.Exist = exist;
            }
        };

        private void btnImportData_Click(object sender, EventArgs e)
        {
            if (txtbSourceFile.Text != "" && txtProjectName.Text != "")
            {
                string[][] stringArr = null;
                RegExcel[] RegExcelPath = {new RegExcel(@"SOFTWARE\Microsoft\Jet\3.5\Engines\Excel" ,"",false)
                                                , new RegExcel(@"SOFTWARE\Microsoft\Jet\4.0\Engines\Excel","",false)
                                                , new RegExcel(@"SOFTWARE\Wow6432Node\Microsoft\Jet\4.0\Engines\Excel","",false)
                                                , new RegExcel(@"SOFTWARE\Wow6432Node\Microsoft\Office\14.0\Access Connectivity Engine\Engines\Excel","",false)
                                                , new RegExcel(@"SOFTWARE\Wow6432Node\Microsoft\Office\12.0\Access Connectivity Engine\Engines\Excel","",false)
                                                , new RegExcel(@"SOFTWARE\Microsoft\Office\14.0\Access Connectivity Engine\Engines\Excel","",false) };
                string[] SheetName = GetExcelSheetNames(txtbSourceFile.Text);
                switch (Path.GetExtension(txtbSourceFile.Text))
                {
                    case ".xls":
                    case ".xlsx":
                        if (cbIsRowType.Checked)
                        {
                            for (int i = 0; i < RegExcelPath.Length; i++)
                            {
                                using (RegistryKey myKey = Registry.LocalMachine.OpenSubKey(RegExcelPath[i].Path, true))
                                {
                                    if (myKey != null)
                                    //檢查子機碼是否存在，檢查資料夾是否存在。
                                    {
                                        //若目錄存在，則取出 key=cnstr 的值。
                                        RegExcelPath[i].DefaultValue = myKey.GetValue("TypeGuessRows").ToString();
                                        RegExcelPath[i].Exist = true;
                                        myKey.SetValue("TypeGuessRows", "0", RegistryValueKind.DWord);
                                    }
                                }
                            }
                        }
                        DataTable dataTable = GetExcelSheetData(txtbSourceFile.Text, SheetName[0]);
                        stringArr = dataTable.AsEnumerable().Select(r => r.ItemArray.Select(ra => ra.ToString()).ToArray()).ToArray();
                        dataTable.Dispose();
                        break;
                    case ".txt":
                        using (StreamReader sr = new StreamReader(txtbSourceFile.Text, Encoding.Default))
                        {
                            sr.ReadLine();//去掉標題列
                            stringArr = (sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None)).ToArray().Select(r => r.Trim().Split('\t')).ToArray();
                        }
                        break;
                }
                if (stringArr.Length <= 0)
                {
                    MessageBox.Show("匯入資料不符合格式，請重新選擇檔案進行匯入！");
                    return;
                }
                // Excel 資料欄位至少大於2欄
                if (stringArr[0].Length < 2)
                {
                    MessageBox.Show("匯入資料不符合格式，請重新選擇檔案進行匯入！");
                    return;
                }

                string strSQL = "";
                //匯入資料庫前，將資料表清空
                //string strSQL = "Truncate Table [Northwind].[dbo].[Categories];";
                for (int i = 0; i < stringArr.Length; i++)
                {
                    //自行設定參數
                    string Project,Category, Priority,Item,Describe,Todo,Owner,Status,StartDate,EndDate,PredictTime_hr;
                    string[] temp = stringArr[i];
                    Project = txtProjectName.Text;
                    Category = temp[1].Trim();
                    Priority = temp[2].Trim();
                    Item = temp[3].Trim();
                    Describe = temp[4].Trim();
                    Todo = temp[5].Trim();
                    Owner = temp[6].Trim();
                    Status = temp[7].Trim();
                    StartDate = temp[8].Trim();
                    EndDate = temp[9].Trim();
                    PredictTime_hr = temp[10].Trim();
                    strSQL += @"INSERT INTO [WBS].[dbo].[WBS]
                                ([Project]
                                ,[Category]
                                ,[Priority]
                                ,[Item]
                                ,[Describe]
                                ,[Todo]
                                ,[Owner]
                                ,[Status]
                                ,[StartDate]
                                ,[EndDate]
                                ,[PredictTime_hr])
                                VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
                    strSQL = string.Format(strSQL, Project, Category, Priority, Item, Describe, Todo, Owner, Status, StartDate, EndDate, PredictTime_hr);
                }

                using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=WBS;Integrated Security=True"))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(strSQL, conn);
                    cmd.Transaction = trans;
                    int sqlcount = 0;
                    try
                    {
                        sqlcount = cmd.ExecuteNonQuery();
                        trans.Commit();
                        if (sqlcount == stringArr.Length)
                            MessageBox.Show("匯入資料成功");
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        MessageBox.Show("匯入資料中 " + (stringArr.Length - sqlcount) + " 筆資料失敗");
                        throw;
                    }
                }

                //回復登錄檔的設定
                if (cbIsRowType.Checked)
                {
                    var ReDefaultReg = RegExcelPath.AsEnumerable().Where(r => r.Exist == true);
                    foreach (var p in ReDefaultReg)
                    {
                        using (RegistryKey myKey = Registry.LocalMachine.OpenSubKey(p.Path, true))
                        {
                            myKey.SetValue("TypeGuessRows", p.DefaultValue, RegistryValueKind.DWord);
                        }
                    }
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
