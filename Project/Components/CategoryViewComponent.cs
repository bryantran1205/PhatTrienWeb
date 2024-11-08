using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.AppData;
using Project.Models;

namespace Project.Components
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly AppDBContext _db;
        public CategoryViewComponent(AppDBContext db)
        {
            this._db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> cateList= await this._db.Categories.ToListAsync();
            cateList.Insert(0, new Category { Id = 0, Name = "All" });
            return View(cateList);
        }
    }
}
