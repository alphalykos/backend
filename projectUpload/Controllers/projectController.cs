using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectUpload.Models;

namespace projectUpload.Controllers
{
    public class projectController : Controller
    {
        private ApplicationDbContext _dbContext;

        public projectController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Videos
        public ActionResult Index()
        {
            var projects = _dbContext.Projects.ToList();

            return View(projects);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Project project)
        {
            var projects = _dbContext.Projects.ToList();
            var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

                    if (project.Image == null || project.Image.ContentLength == 0)
                     {
                         ModelState.AddModelError("ImageUpload", "This field is required");
                     }
                    else if (!imageTypes.Contains(project.Image.ContentType))
                     {
                        ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                     }

                if (ModelState.IsValid)
                {
                var image = new Project
                {
                    title = project.Title,
                    description = project.Description,

                };
            
        if (project.Image != null && project.Image.ContentLength > 0)
                    {
                    var uploadDir = "~/uploads";
            var imagePath = Path.Combine(Server.MapPath(uploadDir), project.Image.FileName);
                        var imageUrl = Path.Combine(uploadDir, project.Image.FileName);
                        project.Image.SaveAs(imagePath);
                        image.ImageUrl = imageUrl;
                    }

                _dbContext.Add(image);
                _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(project);
            }
        }
    public ActionResult Edit(int id)
    {
        var image = _dbContext.Project.Find(id);
        if (image == null)
        {
            return new HttpNotFoundResult();
        }

        var model = new Project
        {
            Title = image.Title,
            Description = image.Description,
            
        }

    return View(model);
    }
    public ActionResult Update(Project project)
        {
            var projectInDb = _dbContext.Projects.SingleOrDefault(v => v.ID == project.ID);

            if (projectInDb == null)
                return HttpNotFound();

            projectInDb.Title = project.Title;
            projectInDb.Description = project.Description;
            projectInDb.Image = project.Image;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}