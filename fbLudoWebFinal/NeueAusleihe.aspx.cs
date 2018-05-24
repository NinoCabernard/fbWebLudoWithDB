using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fbLudoWebFinal
{
    public partial class NeueAusleihe : Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser != null)
            {
                if (currentUser.FirstLogin == false)
                {
                    Response.Redirect("/Account/Manage?m=AddData");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        DataTable dataTable = ConvertToDataTable("~/App_Data/spiele.txt", 2);
                        DropDownList1.DataSource = dataTable;
                        DropDownList1.DataTextField = "Column2";
                        DropDownList1.DataValueField = "Column1";
                        DropDownList1.AutoPostBack = false;
                        DropDownList1.DataBind();

                        var message = Request.QueryString["m"];
                        if (message != null)
                        {
                            // Abfragezeichenfolge aus der Aktion entfernen
                            Form.Action = ResolveUrl("~/Account/Manage");

                            SuccessMessage =
                                message == "SaveDataSuccess" ? "Die persönlichen Daten wurden erfolgreich gespeichert."
                                : String.Empty;
                            successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }

        public void ausleihen(Object sender, EventArgs e)
        {
            var dataFile = Server.MapPath("~/App_Data/ausleihen.txt");
            string[] lines = File.ReadAllLines(dataFile);
            var id = lines.Count();
            var userid = Context.User.Identity.GetUserId();
            DateTime currentTime = DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(7,0,0,0);
            DateTime deadline = currentTime.Add(duration);
            var counter = 0;
            var txt = id.ToString() + ";" + userid + ";" + DropDownList1.SelectedValue + ";" + DropDownList1.SelectedItem + ";" + currentTime + ";" + deadline + ";" + counter ;
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/App_Data/ausleihen.txt"), true))
            {
                _testData.WriteLine(txt); // Write the file.
            }
            dataFile = Server.MapPath("~/App_Data/spiele.txt");
            lines = File.ReadAllLines(dataFile);
            File.WriteAllText(dataFile, String.Empty);
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/App_Data/spiele.txt"), true))
            {
                foreach (string line in lines)
                {
                    var cols = line.Split(';');
                    if (cols[0] == DropDownList1.SelectedValue) {
                        cols[2] = "0";
                        var newline = cols[0] + ";" + cols[1] + ";" + cols[2];
                        _testData.WriteLine(newline); // Write the file.var cols = line.Split(':');
                    }
                    else
                    {
                        _testData.WriteLine(line); // Write the file.var cols = line.Split(':');
                    }
                }
                
            }
            Server.Transfer("AusleihenÜbersicht.aspx", true);
        }

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));

            var dataFile = Server.MapPath(filePath);
            string[] lines = File.ReadAllLines(dataFile);

            foreach (string line in lines)
            {
                var cols = line.Split(';');
                if (cols[2] == "1")
                {
                    DataRow dr = tbl.NewRow();
                    for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                    {
                        dr[cIndex] = cols[cIndex];
                    }

                    tbl.Rows.Add(dr);
                }
            }

            return tbl;
        }
    }
}