using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSystem.Web.Areas.Admin.Controllers
{
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.BindingModels;
    using Models.EntityModels;
    using Models.ViewModels.TeachersManagement;
    using Services;

    public class TeachersManagementController : Controller
    {
        // GET: Admin/TeacherManagement
        public ActionResult Index()
        {
            TeachersService service = new TeachersService();
            var teachers = service.GetAllTeachers();
            IEnumerable<TeacherManagementVM> teacherVMs = teachers
                .ToList()
                .Select(t => new TeacherManagementVM()
                {
                    Id = t.Id,
                    FirstName = t.Profile.FirstName,
                    LastName = t.Profile.LastName,
                    ClassLead = t.Class,
                    Subjects = t.Subjects
                });

            return View(teacherVMs);
        }

        public ActionResult Add()
        {
            ClassesService classesService = new ClassesService();
            SubjectsService subjectsService = new SubjectsService();
            AddTeacherVM vm = new AddTeacherVM()
            {
                Subjects = new MultiSelectList(subjectsService.GetAllSubjects().Select(s => new { SubjectId = s.Id, SubjectName = s.Name }), "SubjectId", "SubjectName"),
                TaughtClasses = new MultiSelectList(classesService.GetAllClasses().Select(c => new { ClassId = c.Id, ClassName = c.Name}), "ClassId", "ClassName")
            };

            return this.View(vm);
        }

        [HttpPost]
        public ActionResult Add(AddTeacherBM bind)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser profile = new ApplicationUser()
                {
                    FirstName = bind.FirstName,
                    MiddleName = bind.MiddleName,
                    LastName = bind.LastName,
                    Pin = bind.PIN,
                    BirthPlace = bind.BirthPlace,
                    Address = bind.Address,
                    BirthDate = bind.BirthDate,
                    IsPublic = bind.IsPublic,
                    ThumbnailUrl = bind.ThumbnailUrl,
                    UserName = bind.PIN,
                };
                
                TeachersService teacherService = new TeachersService();
                ClassesService classesService = new ClassesService();
                SubjectsService subjectsService = new SubjectsService();
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.GetContext));
                userManager.Create(profile, bind.Password);
                Teacher teacher = new Teacher();
                if (bind.TaughtClasses != null)
                {
                    var selectedClassesIds = bind.TaughtClasses;
                    ICollection<Class> classes = classesService.GetAllClassesByIds(selectedClassesIds).ToList();
                    teacher.TaughtClasses = classes;
                }

                if (bind.Subjects != null)
                {
                    var selectedSubjectIds = bind.Subjects;
                    ICollection<Subject> subjects = subjectsService.GetAllSubjectsByIds(selectedSubjectIds).ToList();
                    teacher.Subjects = subjects;
                }

                teacherService.AddTeacher(teacher, profile);

                return this.RedirectToAction("Index");
            }

            return this.View();
        }

        public ActionResult Edit(int id)
        {
            TeachersService teacherService = new TeachersService();
            SubjectsService subjectsService = new SubjectsService();
            ClassesService classesService = new ClassesService();
            Teacher teacher = teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return this.RedirectToAction("Index");
            }

            EditTeacherVM vm = new EditTeacherVM()
            {
                FirstName = teacher.Profile.FirstName,
                MiddleName = teacher.Profile.MiddleName,
                LastName = teacher.Profile.LastName,
                Address = teacher.Profile.Address,
                BirthDate = teacher.Profile.BirthDate,
                BirthPlace = teacher.Profile.BirthPlace,
                IsPublic = teacher.Profile.IsPublic,
                ThumbnailUrl = teacher.Profile.ThumbnailUrl,
                Subjects = new MultiSelectList(subjectsService.GetAllSubjects()
                .Select(s => new { SubjectId = s.Id, SubjectName = s.Name }), 
                "SubjectId", 
                "SubjectName",
                teacher.Subjects.Select(s => new { SubjectId = s.Id, SubjectName = s.Name })),
                TaughtClasses = new MultiSelectList(classesService.GetAllClasses()
                .Select(c => new { ClassId = c.Id, ClassName = c.Name }), 
                "ClassId", 
                "ClassName",
                teacher.TaughtClasses.Select(c => new { ClassId = c.Id, ClassName = c.Name }))
            };

            return this.View(vm);
        }
    }
}