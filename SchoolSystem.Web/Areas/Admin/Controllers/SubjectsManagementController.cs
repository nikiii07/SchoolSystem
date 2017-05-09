using System.Linq;
using System.Web.Mvc;

namespace SchoolSystem.Web.Areas.Admin.Controllers
{
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.SubjectsManagement;
    using Services;

    public class SubjectsManagementController : Controller
    {
        // GET: Admin/SubjectsManagement
        public ActionResult Index()
        {
            SubjectsService service = new SubjectsService();
            var allSubjects = service.GetAllSubjects();
            var vms = allSubjects.Select(s => new SubjectsManagementVM()
            {
                Id = s.Id,
                Description = s.Description,
                Difficulty = s.Difficulty,
                Name = s.Name,
                ThumbnailUrl = s.ThumbnailUrl
            });

            return View(vms);
        }

        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddSubjectBM bind)
        {
            if (ModelState.IsValid)
            {
                SubjectsService service = new SubjectsService();

                Subject subject = new Subject()
                {
                    Name = bind.Name,
                    Description = bind.Description,
                    Difficulty = bind.Difficulty,
                    ThumbnailUrl = bind.ThumbnailUrl
                };

                service.AddSubject(subject);
                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SubjectsService service = new SubjectsService();
            Subject subject = service.FindSubjectById(id);
            if (subject == null)
            {
                return this.RedirectToAction("Index");
            }

            EditSubjectVM vm = new EditSubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                Difficulty = subject.Difficulty,
                ThumbnailUrl = subject.ThumbnailUrl
            };

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSubjectBM bind)
        {
            if (ModelState.IsValid)
            {
                SubjectsService service = new SubjectsService();
                Subject subject = service.FindSubjectById(bind.Id);
                if (subject == null)
                {
                    return this.RedirectToAction("Index");
                }

                subject.Description = bind.Description;
                subject.Name = bind.Name;
                subject.Difficulty = bind.Difficulty;
                subject.ThumbnailUrl = bind.ThumbnailUrl;

                service.UpdateSubject(subject);

                return this.RedirectToAction("Index");
            }

            return this.View();
        }
    }
}