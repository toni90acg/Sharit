using Microsoft.WindowsAzure.MobileServices;
using Sharit.Configs;
using Sharit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sharit.Services
{
    public class SharitMobileService
    {
        private IMobileServiceTable<SharitItem> _sharitItemTable;

        private static SharitMobileService _instance;
        public static SharitMobileService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SharitMobileService();
                }

                return _instance;
            }
        }

        private MobileServiceClient _client;
        public MobileServiceClient Client
        {
            get { return _client; }
        }

        public SharitMobileService()
        {
            if (_client != null)
                return;

            _client = new MobileServiceClient(GlobalSettings.AzureUrl);
            _sharitItemTable = _client.GetTable<SharitItem>();
        }

        public Task<IEnumerable<SharitItem>> ReadSharitItemsAsync()
        {
            return _sharitItemTable.ReadAsync();
        }

        public async Task AddOrUpdateSharitItemAsync(SharitItem sharitItem)
        {
            if (string.IsNullOrEmpty(sharitItem.Id))
            {
                await _sharitItemTable.InsertAsync(sharitItem);
            }
            else
            {
                await _sharitItemTable.UpdateAsync(sharitItem);
            }
        }

        public async Task DeleteSharitItemAsync(SharitItem sharitItem)
        {
            await _sharitItemTable.DeleteAsync(sharitItem);
        }
    }
}
