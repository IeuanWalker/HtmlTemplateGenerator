﻿using System;
using System.Linq;
using System.Threading.Tasks;
using App.Database.Models;
using App.Database.Repositories.Project;
using App.Database.Repositories.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Project
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectRepository _projectTbl;
        private readonly ITemplateRepository _templateTbl;

        public DetailsModel(IProjectRepository projectTbl, ITemplateRepository templateTbl)
        {
            _projectTbl = projectTbl ?? throw new ArgumentNullException(nameof(projectTbl));
            _templateTbl = templateTbl ?? throw new ArgumentNullException(nameof(templateTbl));
        }

        public ProjectTbl Project { get; set; }
        public async Task OnGet(Guid id)
        {
            // TODO: Error handling
            Project = (await _projectTbl.Get(x => x.Id.Equals(id), null, nameof(ProjectTbl.Templates)).ConfigureAwait(false)).Single();
            Project.Templates = Project.Templates.OrderBy(x => x.Name).ToList();
            CreateTemplate = new TemplateTbl
            {
                ProjectId = id
            };
        }

        [BindProperty]
        public TemplateTbl CreateTemplate { get; set; }
        public async Task<IActionResult> OnPost()
        {
            // TODO: Error handling
            TemplateTbl result = await _templateTbl.Add(CreateTemplate);


            TempData["toastStatus"] = "success";
            TempData["toastMessage"] = "Template created";
            TempData["scrollToId"] = $"template-{result.Id}";

            return RedirectToPage("/Project/Details", new { id = CreateTemplate.ProjectId });
        }
    }
}