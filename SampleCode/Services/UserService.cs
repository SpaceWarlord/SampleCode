using Microsoft.EntityFrameworkCore;
using Models;
using SampleCode.DTO;
using SampleCode.Interfaces;
using SampleCode.Other;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services
{
    public class UserService : BaseService, IPageService<UserDTO>
    {
        public UserService(SampleDbContext db)
        {
            _db = db;
        }
        public async Task<ObservableCollection<UserDTO>> GetAll()
        {
            return new ObservableCollection<UserDTO>(await _db.Users.Select(c => new UserDTO(c.Id, c.Username)).ToListAsync());
        }

        public async Task<bool> Update(UserDTO user)
        {
            var found = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (found is null) return false;
            found.Username = user.Username;
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<int> Add(UserDTO user)
        {
            var found = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (found is not null)
            {
                return 0;
            }
            else
            {
                Debug.WriteLine("User doesnt yet exist");
                Debug.WriteLine("id is " + user.Id);
                Debug.WriteLine("name is " + user.Username);
                UserModel u = new UserModel()
                {
                    Id = user.Id,
                    Username = user.Username,

                };
                using (var db = new SampleDbContext())
                {
                    db.Users.Add(u);
                    Debug.WriteLine("Added the user");
                    //if more then 0 something was added to database
                    //https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.savechangesasync?view=efcore-9.0
                    try
                    {
                        await db.SaveChangesAsync();
                        return u.Id;
                    }
                    catch (DbUpdateException ex)
                    {
                        Debug.WriteLine($"Unique constraint {ex.ToString()} violated. Duplicate value for {ex.InnerException.ToString()}");

                        var failedEntries = ex.Entries;
                        foreach (var entry in failedEntries)
                        {
                            var entityName = entry.Metadata.Name;
                            var properties = entry.Properties.Where(p => p.IsModified && !p.IsTemporary);
                            foreach (var property in properties)
                            {
                                var propertyName = property.Metadata.Name;
                                Debug.WriteLine($"Failed to update field: {propertyName} in entity: {entityName}");
                            }
                        }
                        return 0;
                    }
                }
            }
        }

        public Task<int> AddUpdate(UserDTO viewModel)
        {
            throw new NotImplementedException();
        }
    }
}