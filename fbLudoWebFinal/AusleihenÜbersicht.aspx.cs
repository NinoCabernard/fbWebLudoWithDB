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
    public partial class AusleihenÜbersicht : Page
    {
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
                    string[] titles = new string[] { "Spiel", "Ausgelehnt am", "Zurückbringen bis", "Verlängerungen", "Verlängern", "Zurückbringen" };
                    var hrow = new TableHeaderRow();
                    foreach (string title in titles)
                    {
                        var cell = new TableHeaderCell();
                        cell.Text = title;
                        hrow.Cells.Add(cell);
                    }
                    tblAusleihenAktiv.Rows.Add(hrow);
                    titles = new string[] { "Spiel", "Ausgelehnt am", "Zurückgebracht", "Verlängerungen" };
                    hrow = new TableHeaderRow();
                    foreach (string title in titles)
                    {
                        var cell = new TableHeaderCell();
                        cell.Text = title;
                        hrow.Cells.Add(cell);
                    }
                    tblAusleihenIaktiv.Rows.Add(hrow);
                    var dataFile = Server.MapPath("~/App_Data/ausleihen.txt");
                    string[] lines = File.ReadAllLines(dataFile);
                    if (lines.Count() > 0)
                    {
                        foreach (string line in lines)
                        {
                            if (line != "")
                            {
                                var cols = line.Split(';');
                                if (cols[1] == User.Identity.GetUserId())
                                {
                                    if (Convert.ToDateTime(cols[5]) > DateTime.Now)
                                    {
                                        var row = new TableRow();
                                        for (int cIndex = 0; cIndex < 9; cIndex++)
                                        {
                                            if (cIndex != 0 && cIndex != 1 && cIndex != 2)
                                            {
                                                var cell = new TableCell();
                                                if (cIndex < 4)
                                                {
                                                    cell.Text = cols[cIndex];
                                                }
                                                else if (cIndex == 4 || cIndex == 5)
                                                {
                                                    cell.Text = Convert.ToDateTime(cols[cIndex]).ToString("dd/MM/yyyy");
                                                }
                                                else if (cIndex == 6)
                                                {
                                                    cell.Text = cols[cIndex] + "/3";
                                                }
                                                else if (cIndex == 7)
                                                {
                                                    LinkButton button = new LinkButton();
                                                    button.Text = "Verlängern";
                                                    button.ID = "v" + cols[0];
                                                    button.Click += new System.EventHandler(this.longer);
                                                    if (cols[6] == "3")
                                                    {
                                                        button.Enabled = false;
                                                        button.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                                                        button.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                                                        button.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                                                        button.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                                                    }
                                                    cell.Controls.Add(button);
                                                }
                                                else if (cIndex == 8)
                                                {
                                                    LinkButton button = new LinkButton();
                                                    button.Text = "Zurückgeben";
                                                    button.ID = "z" + cols[0];
                                                    button.Click += new System.EventHandler(this.back);
                                                    cell.Controls.Add(button);
                                                }
                                                row.Cells.Add(cell);
                                            }
                                        }

                                        tblAusleihenAktiv.Rows.Add(row);
                                    }
                                    else
                                    {
                                        var row = new TableRow();
                                        for (int cIndex = 0; cIndex < 7; cIndex++)
                                        {
                                            if (cIndex != 0 && cIndex != 1 && cIndex != 2)
                                            {
                                                var cell = new TableCell();
                                                if (cIndex < 4)
                                                {
                                                    cell.Text = cols[cIndex];
                                                }
                                                else if (cIndex == 4 || cIndex == 5)
                                                {
                                                    cell.Text = Convert.ToDateTime(cols[cIndex]).ToString("dd/MM/yyyy");
                                                }
                                                else if (cIndex == 6)
                                                {
                                                    cell.Text = cols[cIndex] + "/3";
                                                }
                                                row.Cells.Add(cell);
                                            }
                                        }

                                        tblAusleihenIaktiv.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }

        private void longer(Object sender, EventArgs e)
        {
            var id = (sender as LinkButton).ID;
            var dataFile = Server.MapPath("~/App_Data/ausleihen.txt");
            string[] lines = File.ReadAllLines(dataFile);
            File.WriteAllText(dataFile, String.Empty);
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/App_Data/ausleihen.txt"), true))
            {
                foreach (string line in lines)
                {
                    var cols = line.Split(';');
                    if ("v" + cols[0] == id)
                    {
                        System.TimeSpan duration = new System.TimeSpan(7, 0, 0, 0);
                        DateTime newDeadline = Convert.ToDateTime(cols[5]);
                        newDeadline = newDeadline.Add(duration);
                        cols[5] = newDeadline.ToString("dd/MM/yyyy");
                        cols[6] = Int32.Parse(cols[6]) + 1 + "";
                        var newline = cols[0] + ";" + cols[1] + ";" + cols[2] + ";" + cols[3] + ";" + cols[4] + ";" + cols[5] + ";" + cols[6];
                        _testData.WriteLine(newline); // Write the file.var cols = line.Split(':');
                    }
                    else
                    {
                        _testData.WriteLine(line); // Write the file.var cols = line.Split(':');
                    }
                }

            }

            Response.Redirect(Request.RawUrl);
        }

        private void back(Object sender, EventArgs e)
        {
            var idSpiel = "0";
            var id = (sender as LinkButton).ID;
            var dataFile = Server.MapPath("~/App_Data/ausleihen.txt");
            string[] lines = File.ReadAllLines(dataFile);
            File.WriteAllText(dataFile, String.Empty);
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/App_Data/ausleihen.txt"), true))
            {
                foreach (string line in lines)
                {
                    var cols = line.Split(';');
                    if ("z" + cols[0] == id)
                    {
                        idSpiel = cols[2];
                        DateTime newDeadline = DateTime.Now;
                        cols[5] = newDeadline.ToString("dd/MM/yyyy");
                        var newline = cols[0] + ";" + cols[1] + ";" + cols[2] + ";" + cols[3] + ";" + cols[4] + ";" + cols[5] + ";" + cols[6];
                        _testData.WriteLine(newline); // Write the file.var cols = line.Split(':');
                    }
                    else
                    {
                        _testData.WriteLine(line); // Write the file.var cols = line.Split(':');
                    }
                }

            }


            dataFile = Server.MapPath("~/App_Data/spiele.txt");
            lines = File.ReadAllLines(dataFile);
            File.WriteAllText(dataFile, String.Empty);
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/App_Data/spiele.txt"), true))
            {
                foreach (string line in lines)
                {
                    var cols = line.Split(';');
                    if (cols[0] == idSpiel)
                    {
                        cols[2] = "1";
                        var newline = cols[0] + ";" + cols[1] + ";" + cols[2];
                        _testData.WriteLine(newline); // Write the file.var cols = line.Split(':');
                    }
                    else
                    {
                        _testData.WriteLine(line); // Write the file.var cols = line.Split(':');
                    }
                }

            }

            Response.Redirect(Request.RawUrl);
        }
    }
}