using Microsoft.EntityFrameworkCore;
using Models;
using Models.Other;
using SampleCode.Interfaces;
using Syncfusion.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCode.Services;

public class UserService(SampleDbContext db) : IPageService<UserModel>
{
    private SampleDbContext _db = db;
    
    public async Task<IEnumerable<UserModel>> GetAll()
    {        
        IQueryable<UserModel> users = db.Users;        
        return await users.ToListAsync();
    }

    public async Task<bool> Update(UserModel user)
    {
        var found = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (found is null) return false;
        found.Username = user.Username;
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<int> Add(UserModel user)
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

    public Task<int> AddUpdate(UserModel viewModel)
    {
        throw new NotImplementedException();
    }
}