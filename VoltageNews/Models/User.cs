using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using BCrypt.Net;
using System.Linq;
using HandyControl.Controls;
using System.Threading.Tasks;

namespace VoltageNews.Models;

public partial class User
{
    public int Id { get; set; }

    public string Nickname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string AvatarUri { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Editor? Editor { get; set; }

    public User(string nickname, string email, string password)
    {
            this.Nickname = nickname;
            this.Email = email;
            this.Password = password;
            this.Role = 0;
            this.AvatarUri = "";
    }

    public async Task<bool> createUser()
    {
        try
        {
            this.hashPass();
            using (VoltageDbContext db = new VoltageDbContext())
            {
                db.Users.Add(this);
                try
                {
                      await db.SaveChangesAsync();
                      UserStore.User = this;
                }
                catch(Exception ex)
                {

                }

            }
            return true;
        }
        catch
        {
            return false;
        }
       
    }

    public async static Task<int> checkIfExists(string email)
    {
        using(VoltageDbContext db = new VoltageDbContext())
        {
            User? result = await db.Users.FirstOrDefaultAsync(p => p.Email == email);
            if(result == null)
            {
                return 200;
            }
        }
        return 405;
        
    }

    public void hashPass()
    {
        this.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(this.Password);
        return;
    }

    public async static Task<int> authorize(string email, string password)
    {
        try
        {
            using (VoltageDbContext db = new VoltageDbContext())
            {
                User? result = await db.Users.FirstOrDefaultAsync(p => p.Email == email);
                if (result == null)
                {
                    return 404;
                }
                if(!BCrypt.Net.BCrypt.EnhancedVerify(password, result.Password))
                {
                    return 401;
                }
                UserStore.User = result;
                return 200;
            }
        }
        catch (Exception ex) {
            return 500; 
        }
    }
}
