using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BikeServiceCenter.Data.Model;

namespace BikeServiceCenter.Data
{

    public static class ItemService
    {
        // Gets the data of the available items in the inventory and stores it in a json file
        private static void SaveAll( List<Item> item)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemsFilePath = Utils.GetItemsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(item);
            File.WriteAllText(itemsFilePath, json);
        }

        // Gets the data of Items available in the inventory from the json file and returns them in a list
        public static List<Item> GetAll()
        {
            string itemsFilePath = Utils.GetItemsFilePath();    // Gets the path of the json file where the data of available items are stored
            // Checks if the json file exists 
            if (!File.Exists(itemsFilePath))
            {
                return new List<Item>();    // Returns a new list if the file dosent exist
            }

            var json = File.ReadAllText(itemsFilePath);     // Stores the list data 

            return JsonSerializer.Deserialize<List<Item>>(json);    // Returns the data after converting into a list
        }

        // Method to add a new item to the inventory
        public static List<Item> Create(Guid itemId, string itemName, int quantity)
        {

            List<Item> item = GetAll();
            
            item.Add(new Item
            {
                ItemName = itemName,
                Quantity = quantity,
                DateAdded = DateTime.Now,
        
            });
            SaveAll( item);
            return item;
        }

        // Method to delete an existing item from the inventory
        public static List<Item> Delete(Guid itemid, Guid id)
        {
            List<Item> items = GetAll();
            Item item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            items.Remove(item);
            SaveAll(items);
            return items;
        }

        // Method to update Item name and quantity of an existing item in the inventory
        public static List<Item> Update(Guid id, string itemName, int quantity)
        {
            List<Item> items = GetAll();
            Item itemToUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.ItemName = itemName;
            itemToUpdate.Quantity = quantity;
 
            SaveAll(items);
            return items;
        }

        // Method to update the item quantity from the stock after the admin approves the request
        public static List<Item> UpdateQuantity(Guid id, string itemName, int quantity)
        {
            List<Item> items = GetAll();
            Item itemToUpdate = items.FirstOrDefault(x => x.Id == id);
            if (quantity < itemToUpdate.Quantity)
            {
                itemToUpdate.ItemName = itemName;
                itemToUpdate.Quantity = itemToUpdate.Quantity - quantity;
            }
            else {
                throw new Exception("Insufficient quantity in stock.");
            }
            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }
            SaveAll(items);
            return items;
        }

        // Method to update the date of an item to the latest taken out date after the admin approves the request
        public static List<Item> AddDateTakenOut(Guid id)
        {
            List<Item> items = GetAll();
            Item itemToUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.LastTakenOut = DateTime.Now;

            SaveAll(items);
            return items;
        }
    }
}


