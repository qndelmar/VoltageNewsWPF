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
    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();

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
                      db.LoginHistories.Add(new LoginHistory { LoginDate = DateTime.Now, UserId = this.Id });
                      await db.SaveChangesAsync();
                      UserStore.User = this;
                }
                catch(Exception)
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
                db.LoginHistories.Add(new LoginHistory { LoginDate = DateTime.Now, UserId = result.Id });
                await db.SaveChangesAsync();
                UserStore.User = result;
                return 200;
            }
        }
        catch (Exception) {
            return 500; 
        }
    }

    public static bool editUserPermission(int id, string kindOfPermission)
    {
        try
        {
            using(VoltageDbContext dbCtx = new())
            {
                User? user = dbCtx.Users.FirstOrDefault(r => r.Id == id);
                if(user == null)
                {
                    throw new Exception("This user is not exists");
                }
                switch (kindOfPermission)
                {
                    case "Пользователь":
                        user.Role = 0;
                        break;
                    case "Редактор":
                        user.Role = 1;
                        break;
                    case "Администратор":
                        user.Role = 2;
                        break;
                    default:
                        throw new Exception("Incorrect role");
                }
                dbCtx.SaveChanges();
                return true;
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
