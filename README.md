# Entities-6-Databases-Teamwork
A very basic desktop client for employees in the Elephant book store to be able to edit the products offered by the company. 

Every user can import data from the corresponding file types.

The database schema looks like this:<br>
![Db Schema](./Images/DbSchemaImage.png)


The main window shows all product types with their corresponding categories.

![Main Window](./Images/MainWindow.png)

The menu gives the ability to import data:

![Import Data](./Images/ImportData.png)

Also categories and items can be added without being written into file:

![Add category](./Images/AddCategory.png) 
![Add item](./Images/AddItem.png)

You can delete a category, which will cascade delete all items that belong to that category:

![Delete category](./Images/DeleteCategory.png)

You can edit item data or delete the item itself (which will not delete it from the database) using the UI:

![Items list](./Images/ItemsListed.png)
![Edit item data](./Images/EditItemData.png)

Also you can create report for a category of your choice:
![Create report](./Images/CreateReport.png)

Which will result in a table with the items in the category presented in pdf format:
![Report](./Images/Report.png)