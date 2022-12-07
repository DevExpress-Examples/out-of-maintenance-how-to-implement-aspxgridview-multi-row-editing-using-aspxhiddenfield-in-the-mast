using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;

public class TitleWithHiddenField : ITemplate {
    public TitleWithHiddenField() {
    }

    public void InstantiateIn(Control container) {
        GridViewTitleTemplateContainer titleContainer = container as GridViewTitleTemplateContainer;
                
        ASPxHiddenField hiddenField = new ASPxHiddenField();
        hiddenField.ID = "hfData";
        hiddenField.ClientInstanceName = String.Format("{0}_hfData", titleContainer.Grid.UniqueID);
        titleContainer.Controls.Add(hiddenField);

        ASPxButton btUpdate = new ASPxButton();
        btUpdate.ID = "btUpdate";
        btUpdate.Text = "Apply changes";
        btUpdate.AutoPostBack = false;
        btUpdate.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.PerformCallback(); }}", titleContainer.Grid.ClientInstanceName);
        titleContainer.Controls.Add(btUpdate);
    }
}