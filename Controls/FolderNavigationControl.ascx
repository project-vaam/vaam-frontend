<%@ Control Language="C#" AutoEventWireup="true"  %>
<telerik:RadTreeView runat="server" Skin="MetroTouch" ID="rtvFolders" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true" ShowLineImages="false">
    <ContextMenus>
        <telerik:RadTreeViewContextMenu>
            <Items>
                <telerik:RadMenuItem Text="Add New Folder"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Rename"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Delete"></telerik:RadMenuItem>
            </Items>
        </telerik:RadTreeViewContextMenu>
    </ContextMenus>
</telerik:RadTreeView>

<div id="newFolderContainer">
    <telerik:RadTextBox runat="server" ID="rtbFolder" />
</div>
<div class="btn-new-node">Add New</div>
