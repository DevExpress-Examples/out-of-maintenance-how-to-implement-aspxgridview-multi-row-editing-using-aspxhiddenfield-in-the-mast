Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxHiddenField
Imports DevExpress.Web.ASPxEditors

Public Class TitleWithHiddenField
	Implements ITemplate
	Public Sub New()
	End Sub

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim titleContainer As GridViewTitleTemplateContainer = TryCast(container, GridViewTitleTemplateContainer)

		Dim hiddenField As New ASPxHiddenField()
		hiddenField.ID = "hfData"
		hiddenField.ClientInstanceName = String.Format("{0}_hfData", titleContainer.Grid.UniqueID)
		titleContainer.Controls.Add(hiddenField)

		Dim btUpdate As New ASPxButton()
		btUpdate.ID = "btUpdate"
		btUpdate.Text = "Apply changes"
		btUpdate.AutoPostBack = False
		btUpdate.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.PerformCallback(); }}", titleContainer.Grid.ClientInstanceName)
		titleContainer.Controls.Add(btUpdate)
	End Sub
End Class