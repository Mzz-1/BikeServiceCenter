
using BikeServiceCenter.Data.Model;
using BikeServiceCenter.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace BikeServiceCenter.Data
{
    public class ItemRecordService
    {
        // Gets the data of the items requested by staff and stores it in a json file
        private static void SaveAll(List<ItemRecord> item)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemsRecordFilePath = Utils.GetItemsRecordFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(item);
            File.WriteAllText(itemsRecordFilePath, json);
        }

        // Gets the data of the items requested by staff from the json file and returns them in a list
        public static List<ItemRecord> GetAll()
        {
            string itemsRecordFilePath = Utils.GetItemsRecordFilePath();
            if (!File.Exists(itemsRecordFilePath))
            {
                return new List<ItemRecord>();
            }

            var json = File.ReadAllText(itemsRecordFilePath);

            return JsonSerializer.Deserialize<List<ItemRecord>>(json);
        }

        // Method to store the data of an item requested by the user 
        public static List<ItemRecord> CreateRequest(string username,Guid id, string itemName, int quantity)
        {

            List<ItemRecord> itemRecord = GetAll();
            itemRecord.Add(new ItemRecord
            {
                ItemName = itemName,
                ItemId = id,
                Quantity = quantity,
                TakenBy = username,

            });
            SaveAll(itemRecord);
            return itemRecord;
        }

        // Method to store the data of the item approved by the admin
        public static List<ItemRecord> ApproveRequest(string username, Guid id, string itemName, int quantity)
        {
            List<ItemRecord> itemRecord = GetAll();
            ItemRecord itemToUpdate = itemRecord.FirstOrDefault(x => x.Id == id);

                ItemService.UpdateQuantity(itemToUpdate.ItemId, itemName, quantity);
                ItemService.AddDateTakenOut(itemToUpdate.ItemId);
 
            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.ApprovedBy = username;

            itemToUpdate.TakenOutAt = DateTime.Now;
            itemToUpdate.IsApproved = true;
            SaveAll(itemRecord);
            return itemRecord;
        }

        // Method to store the data of the item declined by the admin
        public static List<ItemRecord> DeclineRequest(string username, Guid id, string itemName, int quantity)
        {
            List<ItemRecord> itemRecord = GetAll();
            ItemRecord itemToUpdate = itemRecord.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.IsDeclined = true;
            SaveAll(itemRecord);
            return itemRecord;
        }

       
    }
}
