using _3012MVC.Common;
using _3012MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace _3012MVC.Controllers
{
	public class userdao
	{
		Shopbanhang db =new Shopbanhang();
		
		public long insert(USSER entity)
		{
			db.USSERs.Add(entity);
			db.SaveChanges();
			return entity.ID;
		}
		public USSER getbyid(string username)
		{
			return db.USSERs.SingleOrDefault(x => x.USERNAME == username);
		}
		public USSER getid(int id)
		{
			return db.USSERs.SingleOrDefault(x => x.ID ==id);
		}
		public List<string>getcredential(string userName)
		{
			var user = db.USSERs.Single(x => x.USERNAME == userName);
			var data = (from a in db.CREDENTIALs
						join b in db.USERGROUPs on a.USERGROUP equals b.ID
						join c in db.ROLEs on a.ROLEID equals c.ID
						where b.ID == user.GROUPID
						select new
						{
							RoleID = a.ROLEID,
							UsergroupID = a.USERGROUP
						}).AsEnumerable().Select(x => new CREDENTIAL() { ROLEID = x.RoleID, USERGROUP = x.UsergroupID });
				return data.Select(x=>x.ROLEID).ToList();
		}
		public int Login(string username,string password,bool isloginadmin=false)
		{
			var result = db.USSERs.SingleOrDefault(x => x.USERNAME == username);
			if(result==null)
			{
				return 0;
			}
			else
			{
				if(isloginadmin==true)
				{
					if (result.GROUPID == Commonuser.ADMIN || result.GROUPID == Commonuser.MOD)
					{
						if (result.SATUS == false)
						{
							return -1;
						}
						else
						{
							if (result.PASS == password)
								return 1;
							else return -2;
						}
					}
					else
					{
						return -3;
					}
					
				}
				else
				{
					if (result.SATUS == false)
					{
						return -1;
					}
					else
					{
						if (result.PASS == password)
							return 1;
						else return -2;
					}
				}
				
			}
		}
		public bool Delete(int id)
		{
			
			try
			{
				var user = db.USSERs.Find(id);
				db.USSERs.Remove(user);
				db.SaveChanges();
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
		public bool changestatus(int id)
		{
			var user = db.USSERs.Find(id);
			user.SATUS = !user.SATUS;
			return true;
		}
	}
	

}