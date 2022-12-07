Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports System.Data.OleDb

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub dsProducts_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim dataSource As SqlDataSource = TryCast(sender, SqlDataSource)
		Dim container As GridViewDetailRowTemplateContainer = TryCast(dataSource.NamingContainer, GridViewDetailRowTemplateContainer)

		dataSource.SelectParameters("CategoryID").DefaultValue = container.KeyValue.ToString()
	End Sub
	Protected Sub grid_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)

		gridView.ClientInstanceName = gridView.UniqueID

		gridView.Templates.TitlePanel = New TitleWithHiddenField()

		For Each col As GridViewDataColumn In gridView.Columns
			If col.Visible AndAlso (Not col.ReadOnly) Then
				col.DataItemTemplate = New EditableDataItem()
			End If
		Next col

		gridView.ClientSideEvents.BeginCallback = "function(s, e) { s.cpIsCustomCallback = (e.command == 'CUSTOMCALLBACK'); }"
		gridView.ClientSideEvents.EndCallback = String.Format("function(s, e) {{ if (s.cpIsCustomCallback) {0}_hfData.Clear(); }}", gridView.UniqueID)
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
		Dim hfData As ASPxHiddenField = TryCast(gridView.FindTitleTemplateControl("hfData"), ASPxHiddenField)

		Dim connection As OleDbConnection = GetConnection()
		If connection Is Nothing Then
			Return
		End If

		For i As Integer = 0 To gridView.VisibleRowCount - 1
			Dim isRowUpdated As Boolean = False
			Dim values(gridView.Columns.Count - 1) As Object
			For Each column As GridViewDataColumn In gridView.Columns

				Dim fieldKey As String = String.Format("{0}_{1}", gridView.GetRowValues(i, gridView.KeyFieldName), column.FieldName)
				If hfData.Contains(fieldKey) Then
					values(column.Index) = hfData(fieldKey)
					isRowUpdated = True
				Else
					values(column.Index) = gridView.GetRowValues(i, column.FieldName)
				End If
			Next column
			If isRowUpdated Then
				UpdateGrid(connection, gridView, values)
			End If
		Next i
		connection.Close()
		gridView.DataBind()
	End Sub

	Private Function GetConnection() As OleDbConnection
		Try
			Dim connection As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\nwind.mdb;Persist Security Info=True")
			connection.Open()
			Return connection
		Catch
		End Try
		Return Nothing
	End Function

	Private Sub UpdateGrid(ByVal connection As OleDbConnection, ByVal gridView As ASPxGridView, ByVal values() As Object)
		Dim update As New OleDbCommand()
		update.Connection = connection

		Select Case gridView.ID
			Case "gvCategories"
				update.CommandText = String.Format("UPDATE [Categories] SET [CategoryName] = '{1}', [Description] = '{2}' WHERE [CategoryID] = {0}", values(0), values(1), values(2))
			Case "gvProducts"
				update.CommandText = String.Format("UPDATE [Products] SET [ProductName] = '{1}', [UnitPrice] = {2}, [Discontinued] = {3} WHERE [ProductID] = {0}", values(0), values(1), values(2), values(3))
		End Select
		'update.ExecuteNonQuery(); // <-- Uncomment this line to allow updating
	End Sub
End Class