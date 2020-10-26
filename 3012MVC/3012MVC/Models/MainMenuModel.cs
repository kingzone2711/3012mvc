using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3012MVC.Models
{
	public class MainMenuModel
	{
		Shopbanhang db = new Shopbanhang();
        public List<item> GetMenuList()
        {
            List<item> mnlist = new List<item>();
            var loaiSPlst = db.LSANPHAMs.OrderBy(a => a.MALOAI).Where(a => !a.MALOAI.Equals("NOTTT")).ToList();
            foreach (var item in loaiSPlst)
            {
                item mnitem = new item();
                mnitem.lsp = item;
                mnitem.hsx = this.GetHangSXLst(item.MALOAI);
                mnlist.Add(mnitem);
            }
            return mnlist;
        }

        private List<HSANXUAT> GetHangSXLst(string maloai)
        {
            List<HSANXUAT> hsxlist = (from p in db.SPHAMs where p.LOAISP == maloai select p.HSANXUAT).Distinct().ToList();
            return hsxlist;
        }
    }

    public class item 
		{
		public	LSANPHAM lsp { get; set; }
		public	List<HSANXUAT> hsx { get; set; }
		}
}
