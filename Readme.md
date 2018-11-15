<!-- default file list -->
*Files to look at*:

* [EditableDataItem.cs](./CS/WebSite/App_Code/EditableDataItem.cs) (VB: [EditableDataItem.vb](./VB/WebSite/App_Code/EditableDataItem.vb))
* [TitleWithHiddenField.cs](./CS/WebSite/App_Code/TitleWithHiddenField.cs) (VB: [TitleWithHiddenField.vb](./VB/WebSite/App_Code/TitleWithHiddenField.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to implement ASPxGridView Multi-Row editing using ASPxHiddenField in the Master-Detail scenario


<p>This example demonstrates how you can implement the Multi-Row editing for ASPxGridViews in the Master-Detail scenario. The <a href="http://documentation.devexpress.com/#AspNet/CustomDocument5767"><u>ASPxHiddenField</u></a> control is used to store edited values and to send data to the server.</p><p>The advantages of this approach:</p><p>  - the edited values will not be missing after the grid's callbacks (sorting, filtering, paging).</p><p>  - in the datasource, only changed rows will be updated</p><br />
<p>The main idea of this approach is to put the ASPxHiddenField in the Title template, so it will be updated on all grid's callbacks, and its data will be accessible on GridView's events. Each DataItem editor has its own client-side <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxEditorsScriptsASPxClientEdit_ValueChangedtopic"><u>ASPxClientEdit.ValueChanged</u></a> event handler to save its data to the hidden field with a corresponding key based on the column's field name and row key value. When the "Apply Changes" button is pressed, a callback is sent to the server by calling the client-side <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_PerformCallbacktopic"><u>ASPxClientGridView.PerformCallback</u></a> method. In the server-side <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_CustomCallbacktopic"><u>ASPxGridView.CustomCallback</u></a> event handler, data is loaded from the hidden field and saved to the datasource.</p><p><strong>See also:<br />
</strong><a href="https://www.devexpress.com/Support/Center/p/K18282">The general technique of using the Init/Load event handler</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E293">How to create a DataItem template for a grid column at runtime</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E248">A simple example of master-detail grids with editing capabilities</a></p><p><a href="https://www.devexpress.com/Support/Center/p/E324">How to implement the multi-row editing feature in the ASPxGridView</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E2333">How to perform ASPxGridView instant updating using different editors in the DataItem template</a></p>

<br/>


