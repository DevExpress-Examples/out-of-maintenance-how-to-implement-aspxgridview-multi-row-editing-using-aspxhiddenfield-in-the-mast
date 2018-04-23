using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHiddenField;
using System.Data.OleDb;

public partial class _Default : System.Web.UI.Page {
    protected void dsProducts_Init(object sender, EventArgs e) {
        SqlDataSource dataSource = sender as SqlDataSource;
        GridViewDetailRowTemplateContainer container = dataSource.NamingContainer as GridViewDetailRowTemplateContainer;

        dataSource.SelectParameters["CategoryID"].DefaultValue = container.KeyValue.ToString();
    }
    protected void grid_Init(object sender, EventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;

        gridView.ClientInstanceName = gridView.UniqueID;

        gridView.Templates.TitlePanel = new TitleWithHiddenField();

        foreach (GridViewDataColumn col in gridView.Columns) {
            if (col.Visible && !col.ReadOnly) {
                col.DataItemTemplate = new EditableDataItem();
            }
        }

        gridView.ClientSideEvents.BeginCallback = "function(s, e) { s.cpIsCustomCallback = (e.command == 'CUSTOMCALLBACK'); }";
        gridView.ClientSideEvents.EndCallback = String.Format("function(s, e) {{ if (s.cpIsCustomCallback) {0}_hfData.Clear(); }}", gridView.UniqueID);
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;
        ASPxHiddenField hfData = gridView.FindTitleTemplateControl("hfData") as ASPxHiddenField;

        OleDbConnection connection = GetConnection();
        if (connection == null)
            return;

        for (int i = 0; i < gridView.VisibleRowCount; i++) {
            bool isRowUpdated = false;
            object[] values = new object[gridView.Columns.Count];
            foreach (GridViewDataColumn column in gridView.Columns) {

                string fieldKey = String.Format("{0}_{1}", gridView.GetRowValues(i, gridView.KeyFieldName), column.FieldName);
                if (hfData.Contains(fieldKey)) {
                    values[column.Index] = hfData[fieldKey];
                    isRowUpdated = true;
                }
                else {
                    values[column.Index] = gridView.GetRowValues(i, column.FieldName);
                }
            }
            if (isRowUpdated) {
                UpdateGrid(connection, gridView, values);
            }
        }
        connection.Close();
        gridView.DataBind();
    }

    OleDbConnection GetConnection() {
        try {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\nwind.mdb;Persist Security Info=True");
            connection.Open();
            return connection;
        }
        catch {
        }
        return null;
    }

    void UpdateGrid(OleDbConnection connection, ASPxGridView gridView, object[] values) {
        OleDbCommand update = new OleDbCommand();
        update.Connection = connection;

        switch (gridView.ID) {
            case "gvCategories":
                update.CommandText = String.Format("UPDATE [Categories] SET [CategoryName] = '{1}', [Description] = '{2}' WHERE [CategoryID] = {0}", values[0], values[1], values[2]);
                break;
            case "gvProducts":
                update.CommandText = String.Format("UPDATE [Products] SET [ProductName] = '{1}', [UnitPrice] = {2}, [Discontinued] = {3} WHERE [ProductID] = {0}", values[0], values[1], values[2], values[3]);
                break;
        }
        //update.ExecuteNonQuery(); // <-- Uncomment this line to allow updating
    }
}