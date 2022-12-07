Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI
Imports DevExpress.Web
Imports System.Web.UI.WebControls

Public Class EditableDataItem
	Implements ITemplate
	Public Sub New()
	End Sub

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim dataItemContainer As GridViewDataItemTemplateContainer = TryCast(container, GridViewDataItemTemplateContainer)

		Dim cellEditor As ASPxEdit
		If TypeOf dataItemContainer.Column Is GridViewDataCheckColumn Then
			cellEditor = New ASPxCheckBox()
		Else
			cellEditor = New ASPxTextBox()
		End If

		cellEditor.ID = "edDataItem"
		dataItemContainer.Controls.Add(cellEditor)

		AddHandler cellEditor.Init, AddressOf cellEditor_Init

	End Sub

	Private Sub cellEditor_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim editor As ASPxEdit = TryCast(sender, ASPxEdit)
		Dim container As GridViewDataItemTemplateContainer = TryCast(editor.NamingContainer, GridViewDataItemTemplateContainer)

		editor.Width = New Unit(100, UnitType.Percentage)

		Dim hfData As ASPxHiddenField = TryCast(container.Grid.FindTitleTemplateControl("hfData"), ASPxHiddenField)

		Dim fieldKey As String = String.Format("{0}_{1}", container.KeyValue, container.Column.FieldName)
		If (hfData IsNot Nothing AndAlso hfData.Contains(fieldKey)) Then
			editor.Value = hfData(fieldKey)
		Else
			editor.Value = DataBinder.Eval(container.DataItem, container.Column.FieldName)
		End If

		editor.SetClientSideEventHandler("ValueChanged", String.Format("function(s, e) {{ {0}_hfData.Set('{1}', s.GetValue()); }}", container.Grid.UniqueID, fieldKey))
	End Sub


End Class