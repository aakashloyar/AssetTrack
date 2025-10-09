using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace AssetApp.Services
{
    public class Session
    {
        private readonly ProtectedSessionStorage _protectedSessionStorage;

        public Session(ProtectedSessionStorage protectedSessionStorage)
        {
            _protectedSessionStorage = protectedSessionStorage;
        }

        public async Task SetUserAsync(string username)
        {
            await _protectedSessionStorage.SetAsync("Username", username);
        }

        public async Task<string> GetUserAsync()
        {
            var result = await _protectedSessionStorage.GetAsync<string>("Username");
            return result.Value;
        }

        public async Task RemoveUserAsync()
        {
            await _protectedSessionStorage.DeleteAsync("Username");
        }
    }
}
