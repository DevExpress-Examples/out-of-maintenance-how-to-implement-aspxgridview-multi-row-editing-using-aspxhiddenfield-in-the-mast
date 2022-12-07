using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

public class EditableDataItem: ITemplate {
    public EditableDataItem() {
    }

    public void InstantiateIn(Control container) {
        GridViewDataItemTemplateContainer dataItemContainer = container as GridViewDataItemTemplateContainer;

        ASPxEdit cellEditor;
        if (dataItemContainer.Column is GridViewDataCheckColumn) {
            cellEditor = new ASPxCheckBox();
        }
        else {
            cellEditor = new ASPxTextBox();
        }

        cellEditor.ID = "edDataItem";
        dataItemContainer.Controls.Add(cellEditor);

        cellEditor.Init += new EventHandler(cellEditor_Init);
        
    }

    void cellEditor_Init(object sender, EventArgs e) {
        ASPxEdit editor = sender as ASPxEdit;
        GridViewDataItemTemplateContainer container = editor.NamingContainer as GridViewDataItemTemplateContainer;

        editor.Width = new Unit(100, UnitType.Percentage);

        ASPxHiddenField hfData = container.Grid.FindTitleTemplateControl("hfData") as ASPxHiddenField;

        string fieldKey = String.Format("{0}_{1}", container.KeyValue, container.Column.FieldName);
        editor.Value = (hfData != null && hfData.Contains(fieldKey)) ? hfData[fieldKey] : DataBinder.Eval(container.DataItem, container.Column.FieldName);

        editor.SetClientSideEventHandler("ValueChanged", String.Format("function(s, e) {{ {0}_hfData.Set('{1}', s.GetValue()); }}", container.Grid.UniqueID, fieldKey)); 
    }


}